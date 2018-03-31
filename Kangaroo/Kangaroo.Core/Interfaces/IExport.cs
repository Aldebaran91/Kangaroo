using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kangaroo.Core.Interfaces
{
    /// <summary>
    /// Defines property and generic methods to be implemented for exporting data. Provides a basis for custom exporter implemenations.
    /// </summary>
    /// <typeparam name="U">U is the generic type of the export data.</typeparam>
    public interface IExport<U>
    {
        /// <summary>
        /// Property to set criteria for filtering data to be exported.
        /// </summary>
        Predicate<U> Filter { get; set; }

        /// <summary>
        /// Generic method to export an enumerable collection of data (of type U).
        /// </summary>
        /// <param name="data">Data which should be exported.</param>
        void ExportData(IEnumerable<U> data);

        /// <summary>
        /// Generic method to export an enumerable collection of data (of type U). Ansynchronous export is realized by returning a value of type Task.
        /// </summary>
        /// <param name="data">Data which should be exported.</param>
        /// <returns>Returns a task.</returns>
        Task ExportDataAsync(IEnumerable<U> data);
    }
}