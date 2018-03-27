using System;
using System.Collections.Generic;
using System.Text;

namespace Kangaroo.Core.Interfaces
{
	/// <summary>
	/// Defines properties to be implemented for converting and exporting data.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="U"></typeparam>
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
