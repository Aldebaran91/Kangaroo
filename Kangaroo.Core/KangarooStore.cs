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
        private readonly object dataLock = new object();

        private struct KangarooData
        {
            public T Data;
            public Enum Category;

            public KangarooData(T data, Enum category)
            {
                this.Data = data;
                this.Category = category;
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

        /// <summary>
        /// Contains a delegate which should be used if data will be added.
        /// </summary>
        private Action<T, Enum> ExportCommand;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Constructor taking custom exporter settings.
        /// </summary>
        /// <param name="settings">Parateter for custom exporter settings.</param>
        public KangarooStore(KangarooSettings settings = null)
        {
            if (settings == null)
            {
                this.settings = new KangarooSettings();
            }
            else
            {
                this.settings = settings;
            }

            if (this.settings.Inverval != TimeSpan.Zero)
            {
                StartTimebasedExport();
            }
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Enumerable property with a collection of data objects to be exported.
        /// </summary>
        private ConcurrentQueue<KangarooData> data { get; } = new ConcurrentQueue<KangarooData>();

        /// <summary>
        /// Enumerable property with a collection of specific/custom export handlers to be used.
        /// </summary>
        private ConcurrentQueue<KangarooExportHandler> ExportHandler { get; set; } = new ConcurrentQueue<KangarooExportHandler>();

        /// <summary>
        /// Property for export settings as defined by specific/custom implementation.
        /// </summary>
        public KangarooSettings Settings { get => settings; set => settings = value; }

        #endregion Properties

        /// <summary>
        /// Overloaded method for addig data to the collection of data objects to be exported, and also passing the category for the data to be assigned to.
        /// </summary>
        /// <param name="data">The data which should be exported later.</param>
        /// <param name="category">Provides the ability to categories the data.</param>
        public void AddData(T data, Enum category = null)
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
                        lock (dataLock)
                        {
                            this.data.Enqueue(new KangarooData(data, category));
                            count = this.data.Count;
                            if (settings.MaxStoredObjects > 0 && count >= settings.MaxStoredObjects)
                            {
                                StartManualExport();
                            }
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

        /// <summary>
        /// Method for staring the export.
        /// </summary>
        public void StartManualExport()
        {
            KangarooData[] dataSnapshot = null;
            lock (dataLock)
            {
                dataSnapshot = new KangarooData[data.Count];
                data.CopyTo(dataSnapshot, 0);
                this.data.Clear();
            }

            Dictionary<Enum, List<T>> dataToExport = new Dictionary<Enum, List<T>>();
            List<T> dataToExport_uncategorized = new List<T>();

            for (int i = 0; i < dataSnapshot.Length; i++)
            {
                if (dataSnapshot[i].Category == null)
                {
                    dataToExport_uncategorized.Add(dataSnapshot[i].Data);
                }
                else
                {
                    if (dataToExport.ContainsKey(dataSnapshot[i].Category) == false)
                    {
                        dataToExport.Add(dataSnapshot[i].Category, new List<T>());
                    }

                    dataToExport[dataSnapshot[i].Category].Add(dataSnapshot[i].Data);
                }
            }

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
            lock (dataLock)
            {
                if (intervalExporter == null && settings.Inverval.TotalMilliseconds > 0)
                {
                    var token = cancellationTokenSource.Token;
                    intervalExporter = Task.Run(() =>
                    {
                        while (!token.IsCancellationRequested)
                        {
                            Task.Delay(settings.Inverval).Wait();
                            StartManualExport();
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Method for stopping the time triggered export. 
        /// </summary>
        public void StopTimebasedExport()
        {
            lock (dataLock)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource = new CancellationTokenSource();
                intervalExporter = null;
            }
        }

        /// <summary>
        /// Method for clearing the registered export hanlders, items belonging to a category can be deleted selectively by passing a category paramteter
        /// </summary>
        /// <param name="category">Category of export handlers to be selected for deletion.</param>
        public void ClearExporter(Enum category = null)
        {
            if (category == null)
            {
                this.ExportHandler.Clear();
            }
            else
            {
                var list = this.ExportHandler.Where(x => !x.Category.Equals(category)).ToList();
                this.ExportHandler.Clear();
                this.ExportHandler = new ConcurrentQueue<KangarooExportHandler>(list);
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