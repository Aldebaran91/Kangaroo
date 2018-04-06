using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kangaroo.Core
{
    /// <summary>
    /// Defines property and generic methods to be implemented for exporting data. Provides a basis for custom exporter implemenations.
    /// </summary>
    /// <typeparam name="T">U is the generic type of the export data.</typeparam>
    public abstract class Export<T>
    {
        /// <summary>
        /// Generic method to export an enumerable collection of data (of type U).
        /// </summary>
        /// <param name="data">Data which should be exported.</param>
        public void ExportData(IEnumerable<T> data)
        {

        }
    }
}