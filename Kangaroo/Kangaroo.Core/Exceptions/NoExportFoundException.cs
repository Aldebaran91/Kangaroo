using System;

namespace Kangaroo.Core.Exceptions
{
    public class NoExportFoundException : Exception
    {
        internal NoExportFoundException()
        {
        }

        internal NoExportFoundException(string message) : base(message)
        {
        }

        internal NoExportFoundException(string message, Exception innerException) : base (message, innerException)
        {
        }
    }
}
