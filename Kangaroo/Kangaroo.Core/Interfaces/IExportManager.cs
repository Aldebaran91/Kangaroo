using System;
using System.Collections.Generic;
using System.Text;

namespace Kangaroo.Core
{
    public interface IExportManager<T, U>
    {
        IConverter<T, U> Converter { get; set; }
        IExportWorker<U> Worker { get; set; }
    }
}
