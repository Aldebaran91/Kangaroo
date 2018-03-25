using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kangaroo.Core.Interfaces
{
	/// <summary>
	/// Defines the generic methods to be implemented for converting data from a data type (T) to another (U). Provides a basis for custom converter implemenations.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="U"></typeparam>
	public interface IConverter<T, U>
    {
		/// <summary>
		/// Generic method to convert data from a data type (T) to another (U). Conversion is perfomed in preparation for exporting data.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		U Convert(T data);
		/// <summary>
		/// Generic method to convert data, which takes an input of a data type (T) and returns a Task with a result of another type (U). Conversion is perfomed in preparation for exporting data asynchronously.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		Task<U> ConvertAsync(T data);
    }
}
