using Kangaroo.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kangaroo
{
    /// <summary>
    /// Primary class of the Kangaroo library.
    /// </summary>
    /// <example>
    /// <code
    /// source="..\Kangaroo.Docu\KangarooExample.cs"
    /// region="Example1"
    /// title="How to use Kangaroo library"/>
    /// </example>
    public sealed class KangarooStore<T>
    {
        #region Fields

        private static KangarooStore<T> instance;
        private KangarooSettings settings;

        #endregion Fields

        #region Constructor

        public KangarooStore()
        {
        }

        public KangarooStore(KangarooSettings settings)
        {
            this.settings = settings;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Enumerable property with a collection of data objects to be exported.
        /// </summary>
        private IList<T> data { get; } = new List<T>();

        /// <summary>
        /// Enumerable property with a collection of specific/custom export handlers to be used.
        /// </summary>
        public IList<IKangarooExportWorker<T>> ExportHandler { get; set; }

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Overloaded method for addig data to the collection of data objects to be exported, and also passing the category for the data to be assigned to.
        /// </summary>
        /// <param name="data">The data which should be exported later.</param>
        /// <param name="category">Provides the ability to categories the data.</param>
        public void AddData(T data, string category)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method for staring the export.
        /// </summary>
        public void StartExport()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method for asynchronously starting the export. Ansynchronous export is realized by returning a value of type Task.
        /// </summary>
        /// <returns>Returns a task.</returns>
        public Task StartExportAsync()
        {
            throw new NotImplementedException();
        }
    }
}