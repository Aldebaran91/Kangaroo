using System;

namespace Kangaroo.Core.Exceptions
{
	/// <summary>
	/// Defines library specific exception implementations related to converting and exporting data.
	/// </summary>
	public class NoExportFoundException : Exception
	{
		/// <summary>
		/// Helper method to raise a library specific exception.
		/// </summary>
		internal NoExportFoundException()
		{
		}

		/// <summary>
		/// Helper method to raise a library specific exception with a custom error message.
		/// </summary>
		/// <param name="message">String paramter for the custom error message.</param>
		internal NoExportFoundException(string message) : base(message)
		{
		}

		/// <summary>
		/// Helper method to raise a library specific exception with a custom error message.
		/// </summary>
		/// <param name="message">String paramter for the custom error message.</param>
		/// <param name="innerException">Parameter of type Exception to reference the inner exception causing this exception.</param>
		internal NoExportFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}