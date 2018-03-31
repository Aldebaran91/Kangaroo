using Kangaroo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kangaroo
{
    /// <summary>
    /// Primary class of the Kangaroo library.
    /// </summary>
    /// <example>
    /// <code source="..\..\SWF\Kangaroo.Docu\KangarooExample.cs" region="KangarooExample1" title="How to use Kangaroo library"/>
    /// </example>
    public sealed class Kangaroo
	{
		/// <summary>
		/// Enumerable property with a collection of data objects to be exported.
		/// </summary>
		public IEnumerable<object> LoggingData { get; set; }

		/// <summary>
		/// Enumerable property with a collection of specific/custom export handlers to be used.
		/// </summary>
		public IEnumerable<IExportManager<object, object>> ExportHandler { get; set; }

		/// <summary>
		/// Property for export settings as defined by specific/custom implementation.
		/// </summary>
		public IExportSettings ExportSettings { get; set; }

		/// <summary>
		/// Method for adding data to the collection of data objects to be exported. 
		/// </summary>
		/// <param name="data">The data which should be exported later.</param>
		public void AddData(object data)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Method for adding data asynchronously to the collection of data objects to be exported. 
        /// </summary>
        /// <param name="data">The data which should be exported later.</param>
		/// <returns>Returns a task.</returns>
        public Task AddDataAsync(object data)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Overloaded method for addig data to the collection of data objects to be exported, and also passing the category for the data to be assigned to.
        /// </summary>
        /// <param name="data">The data which should be exported later.</param>
        /// <param name="category">Provides the ability to categories the data.</param>
        public void AddData(object data, string category)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Overloaded method for addig data asynchronously to the collection of data objects to be exported, and also passing the category for the data to be assigned to.
        /// </summary>
        /// <param name="data">The data which should be exported later.</param>
        /// <param name="category">Provides the ability to categories the data.</param>
		/// <returns>Returns a task.</returns>
        public Task AddDataAsync(object data, string category)
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
