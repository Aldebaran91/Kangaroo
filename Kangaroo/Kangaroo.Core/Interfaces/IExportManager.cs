using System;
using System.Collections.Generic;
using System.Text;

namespace Kangaroo.Core.Interfaces
{
    public interface IExportManager<T, U>
    {
        IExport<U> DataExport { get; set; }
        IConverter<T, U> DataConverter { get; set; }
    }
}
