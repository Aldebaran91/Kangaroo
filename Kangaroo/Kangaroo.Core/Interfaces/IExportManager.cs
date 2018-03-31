using System;
using System.Collections.Generic;
using System.Text;

namespace Kangaroo.Core.Interfaces
{
    /// <summary>
    /// Defines properties to be implemented for converting and exporting data. If the type T is not the same as U the data will be converted.
    /// </summary>
    /// <typeparam name="T">T is the generic type for the input.</typeparam>
    /// <typeparam name="U">U is the generic type for the export.</typeparam>
    public interface IExportManager<T, U>
	{
		/// <summary>
		/// Property for specific/custom exporter to be used.
		/// </summary>
		IExport<U> DataExport { get; set; }

		/// <summary>
		/// Property for specific/custom converter to be used.
		/// </summary>
		IConverter<T, U> DataConverter { get; set; }
	}
}
