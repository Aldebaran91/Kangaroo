using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kangaroo.Core.Interfaces
{
	/// <summary>
	/// Defines the generic methods to be implemented for converting data from a data type (T) to another (U). Provides a basis for custom converter implemenations.
	/// </summary>
	/// <typeparam name="T">T is the generic type for the input.</typeparam>
	/// <typeparam name="U">U is the generic type for the export.</typeparam>
	public interface IConverter<T, U>
	{
		/// <summary>
		/// Generic method to convert data from a data type (T) to another (U). Conversion is perfomed in preparation for exporting data.
		/// </summary>
		/// <param name="data">Data is the object which should be converted to the generic type U</param>
		/// <returns>Return a new type converted from input data.</returns>
		U Convert(T data);

        /// <summary>
        /// Generic method to convert data, which takes an input of a data type (T) and returns a Task with a result of another type (U). Conversion is perfomed in preparation for exporting data asynchronously.
        /// </summary>
        /// <param name="data">Data is the object which should be converted to the generic type U</param>
        /// <returns>Return a new type converted from input data.</returns>
        Task<U> ConvertAsync(T data);
	}
}
