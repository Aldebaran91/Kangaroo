﻿using Kangaroo.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kangaroo
{
	/// <summary>
	/// Primary class of the Kangaroo library.
	/// </summary>
	/// <typeparam name="T">The generic type parameter.</typeparam>
	/// <example>
	/// <code
	/// source="..\Kangaroo.Docu\KangarooExample.cs"
	/// region="Example1"
	/// title="How to use Kangaroo library"/>
	/// </example>
	public sealed class KangarooStore<T>
	{
        private readonly object dataLock = new object();

        private struct KangarooData
        {
            public T Data;
            public KangarooDataCategory Category;

            public KangarooData(T data, KangarooDataCategory category)
            {
                this.Data = data;
                this.Category = category;
            }
        }

        private struct KangarooExportHandler
        {
            public IKangarooExportWorker<T> ExportWorker;
            public KangarooDataCategory Category;

            public KangarooExportHandler(IKangarooExportWorker<T> exportWorker, KangarooDataCategory category)
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
		/// Default constructor.
		/// </summary>
		public KangarooStore()
		{
		}

		/// <summary>
		/// Constructor taking custom exporter settings.
		/// </summary>
		/// <param name="settings">Parateter for custom exporter settings.</param>
		public KangarooStore(KangarooSettings settings)
		{
			this.settings = settings;
		}

		#endregion Constructor

		#region Properties

		/// <summary>
		/// Enumerable property with a collection of data objects to be exported.
		/// </summary>
		private IList<KangarooData> data { get; } = new List<KangarooData>();

        /// <summary>
        /// Enumerable property with a collection of specific/custom export handlers to be used.
        /// </summary>
        private IList<KangarooExportHandler> ExportHandler { get; set; } = new List<KangarooExportHandler>();

		/// <summary>
		/// Property for export settings as defined by specific/custom implementation.
		/// </summary>
		public KangarooSettings Settings { get => settings; set => settings = value; }

		#endregion Properties

		/// <summary>
		/// Method for adding data to the collection of data objects to be exported.
		/// </summary>
		/// <param name="data">The data which should be exported later.</param>
		public void AddData(T data)
		{
            this.data.Add(new KangarooData(data, null));
        }

		/// <summary>
		/// Overloaded method for addig data to the collection of data objects to be exported, and also passing the category for the data to be assigned to.
		/// </summary>
		/// <param name="data">The data which should be exported later.</param>
		/// <param name="category">Provides the ability to categories the data.</param>
		public void AddData(T data, KangarooDataCategory category)
		{
			this.data.Add(new KangarooData(data, category));
		}

		/// <summary>
		/// Method for staring the export.
		/// </summary>
		public void StartExport()
		{
            KangarooData[] dataSnapshot = null;
            lock (dataLock)
            {
                dataSnapshot = new KangarooData[data.Count];
                data.CopyTo(dataSnapshot, 0);
                this.data.Clear();
            }
            
            Dictionary<string, List<T>> dataToExport = new Dictionary<string, List<T>>();
            List<T> dataToExport_uncategorized = new List<T>();

            for (int i = 0; i < dataSnapshot.Length; i++)
            {
                if (dataSnapshot[i].Category == null)
                {
                    dataToExport_uncategorized.Add(dataSnapshot[i].Data);
                }
                else
                {
                    if (dataToExport.ContainsKey(dataSnapshot[i].Category.Identifier) == false)
                    {
                        dataToExport.Add(dataSnapshot[i].Category.Identifier, new List<T>());
                    }

                    dataToExport[dataSnapshot[i].Category.Identifier].Add(dataSnapshot[i].Data);
                }
            }
            
            List<Exception> exceptions = new List<Exception>();
            for (int i = 0; i < ExportHandler.Count; i++)
            {
                try
                {
                    if (ExportHandler[i].Category == null)
                    {
                        ExportHandler[i].ExportWorker.Export(dataToExport_uncategorized);
                    }
                    else
                    {
                        if (dataToExport.ContainsKey(ExportHandler[i].Category.Identifier))
                        {
                            ExportHandler[i].ExportWorker.Export(dataToExport[ExportHandler[i].Category.Identifier]);
                        }
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
		public Task StartExportAsync()
		{
            return Task.Run(() => StartExport());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void ClearExporter(KangarooDataCategory category = null)
        {
            if (category == null)
            {
                this.ExportHandler.Clear();
            }
            else
            {
                for (int i = ExportHandler.Count - 1; i >= 0; i--)
                {
                    if (this.ExportHandler[i].Category.Identifier == category.Identifier)
                    {
                        this.ExportHandler.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exportWorker"></param>
        /// <param name="category"></param>
        public void AddExporter(IKangarooExportWorker<T> exportWorker, KangarooDataCategory category = null)
        {
            this.ExportHandler.Add(new KangarooExportHandler(exportWorker, category));
        }
	}
}