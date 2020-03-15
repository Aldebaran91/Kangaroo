using Kangaroo.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kangaroo
{
    /// <summary>
    /// Primary class of the Kangaroo library.
    /// </summary>
    /// <typeparam name="T">The generic type parameter.</typeparam>
    public sealed class KangarooStore<T>
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private Task intervalExporter;

        private struct KangarooData
        {
            public T Data;
            public Enum Category;
            internal DateTime DateTimeOfCollect;

            public KangarooData(T data, Enum category)
            {
                this.Data = data;
                this.Category = category;
                this.DateTimeOfCollect = DateTime.UtcNow;
            }
        }

        private struct KangarooExportHandler
        {
            public IKangarooExportWorker<T> ExportWorker;
            public Enum Category;

            public KangarooExportHandler(IKangarooExportWorker<T> exportWorker, Enum category)
            {
                this.ExportWorker = exportWorker;
                this.Category = category;
            }
        }

        #region Fields

        /// <summary>
        /// Member for export settings.
        /// </summary>
        private KangarooSettings settings;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Constructor taking standard exporter settings.
        /// </summary>
        public KangarooStore() : this(new KangarooSettings())
        {
        }

        /// <summary>
        /// Constructor taking custom exporter settings.
        /// </summary>
        /// <param name="settings">Parateter for custom exporter settings.</param>
        public KangarooStore(KangarooSettings settings)
        {
            this.settings = settings;
            
            if (this.settings.Inverval != TimeSpan.Zero)
            {
                StartTimebasedExport();
            }
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Queue property with a collection of data objects to be exported.
        /// </summary>
        private ConcurrentQueue<KangarooData> data { get; } = new ConcurrentQueue<KangarooData>();

        /// <summary>
        /// Queue property with a collection of specific/custom export handlers to be used.
        /// </summary>
        private ConcurrentQueue<KangarooExportHandler> ExportHandler { get; set; } = new ConcurrentQueue<KangarooExportHandler>();

        /// <summary>
        /// Property for export settings as defined by specific/custom implementation.
        /// </summary>
        public KangarooSettings Settings { get => settings; set => settings = value; }

        #endregion Properties

        /// <summary>
        ///  Method for addig data to the collection of data objects to be exported.
        /// </summary>
        /// <param name="data"></param>
        public void AddData(T data)
        {
            AddData(data, null);
        }

        /// <summary>
        /// Method for addig data to the collection of data objects to be exported, and also passing the category for the data to be assigned to.
        /// </summary>
        /// <param name="data">The data which should be exported later.</param>
        /// <param name="category">Provides the ability to categories the data.</param>
        public void AddData(T data, Enum category)
        {
            switch (settings.MaxStoredObjects)
            {
                case 0:
                    {
                        this.data.Enqueue(new KangarooData(data, category));
                        break;
                    }
                case 1:
                    {
                        DirectExport(data, category);
                        break;
                    }
                default:
                    {
                        int count = 0;
                        this.data.Enqueue(new KangarooData(data, category));
                        count = this.data.Count;
                        if (settings.MaxStoredObjects > 0 && count >= settings.MaxStoredObjects)
                        {
                            StartManualExport();
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Direct export without adding data to the queue
        /// </summary>
        /// <param name="data">Data which should be exported</param>
        /// <param name="category">Category for data</param>
        private void DirectExport(T data, Enum category = null)
        {
            T[] exData = new T[] { data };

            List<Exception> exceptions = new List<Exception>();
            var enumerator = ExportHandler.GetEnumerator();
            while (enumerator.MoveNext())
            {
                try
                {
                    if (enumerator.Current.Category == null && category == null)
                    {
                        enumerator.Current.ExportWorker.Export(exData);
                    }
                    else if (category != null && category.Equals(enumerator.Current.Category))
                    {
                        enumerator.Current.ExportWorker.Export(exData);
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions.ToArray());
            }
        }

        public void StartManualExport()
        {
            StartExportTimeBased(DateTime.UtcNow);
        }

        /// <summary>
        /// Method for staring the export.
        /// </summary>
        internal void StartExporSizeBased()
        {
            Dictionary<Enum, List<T>> dataToExport = new Dictionary<Enum, List<T>>();
            List<T> dataToExport_uncategorized = new List<T>();

            while (dataToExport.Count < settings.MaxStoredObjects && data.Count > 0)
            {
                if (data.TryDequeue(out var dataSnapshot))
                {
                    if (dataSnapshot.Category == null)
                    {
                        dataToExport_uncategorized.Add(dataSnapshot.Data);
                    }
                    else
                    {
                        if (dataToExport.ContainsKey(dataSnapshot.Category) == false)
                        {
                            dataToExport.Add(dataSnapshot.Category, new List<T>());
                        }

                        dataToExport[dataSnapshot.Category].Add(dataSnapshot.Data);
                    }
                }
            }

            DataToExport(dataToExport, dataToExport_uncategorized);
        }

        internal void StartExportTimeBased(DateTime until)
        {
            Dictionary<Enum, List<T>> dataToExport = new Dictionary<Enum, List<T>>();
            List<T> dataToExport_uncategorized = new List<T>();

            while (data.Count > 0)
            {
                if (data.TryDequeue(out var dataSnapshot))
                {
                    if (dataSnapshot.DateTimeOfCollect > until)
                        continue;

                    if (dataSnapshot.Category == null)
                    {
                        dataToExport_uncategorized.Add(dataSnapshot.Data);
                    }
                    else
                    {
                        if (dataToExport.ContainsKey(dataSnapshot.Category) == false)
                        {
                            dataToExport.Add(dataSnapshot.Category, new List<T>());
                        }

                        dataToExport[dataSnapshot.Category].Add(dataSnapshot.Data);
                    }
                }
            }

            DataToExport(dataToExport, dataToExport_uncategorized);
        }

        /// <summary>
        /// Method to export categorized data to the export handlers.
        /// </summary>
        /// <param name="dataToExport"></param>
        /// <param name="dataToExport_uncategorized"></param>
        private void DataToExport(Dictionary<Enum, List<T>> dataToExport, List<T> dataToExport_uncategorized)
        {
            List<Exception> exceptions = new List<Exception>();
            var enumerator = ExportHandler.GetEnumerator();
            while (enumerator.MoveNext())
            {
                try
                {
                    if (enumerator.Current.Category == null)
                    {
                        enumerator.Current.ExportWorker.Export(dataToExport_uncategorized.ToArray());
                    }
                    else if (dataToExport.ContainsKey(enumerator.Current.Category))
                    {
                        enumerator.Current.ExportWorker.Export(dataToExport[enumerator.Current.Category].ToArray());
                    }
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions.ToArray());
            }
        }

        /// <summary>
        /// Method for asynchronously starting the export. Ansynchronous export is realized by returning a value of type Task.
        /// </summary>
        /// <returns>Returns a task.</returns>
        public Task StartManualExportAsync()
        {
            return Task.Run(() => StartManualExport());
        }

        /// <summary>
        /// Method for starting the export to be triggered at defined intervals.
        /// </summary>
        public void StartTimebasedExport()
        {
            if (intervalExporter == null && settings.Inverval.TotalMilliseconds > 0)
            {
                var token = cancellationTokenSource.Token;
                intervalExporter = Task.Run(() =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        Task.Delay(settings.Inverval).Wait();
                        StartExportTimeBased(DateTime.UtcNow);
                    }
                });
            }
        }

        /// <summary>
        /// Method for stopping the time triggered export. 
        /// </summary>
        public void StopTimebasedExport()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            intervalExporter = null;
        }

        /// <summary>
        /// Clear all registered export handlers.
        /// </summary>
        public void ClearExporter()
        {
            ClearExporter(null);
        }

        /// <summary>
        /// Method for clearing the registered export hanlders, items belonging to a category can be deleted selectively by passing a category paramteter
        /// </summary>
        /// <param name="category">Category of export handlers to be selected for deletion.</param>
        public void ClearExporter(params Enum[] category)
        {
            if (category == null || category.Length == 0)
            {
                this.ExportHandler.Clear();
            }
            else
            {
                foreach (var cat in category)
                {
                    var list = this.ExportHandler.Where(x => !x.Category.Equals(cat)).ToList();
                    this.ExportHandler.Clear();
                    this.ExportHandler = new ConcurrentQueue<KangarooExportHandler>(list);
                }
            }
        }

        /// <summary>
        /// Method for registering an export handler.
        /// </summary>
        /// <param name="exportWorker">Export worker to be assigned to the exporter.</param>
        /// <param name="category">Caterty to define which items should be handled by the exporter.</param>
        public void AddExporter(IKangarooExportWorker<T> exportWorker, Enum category = null)
        {
            this.ExportHandler.Enqueue(new KangarooExportHandler(exportWorker, category));
        }
    }
}