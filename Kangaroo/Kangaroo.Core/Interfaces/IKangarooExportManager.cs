using System;
using System.Collections.Generic;
using System.Text;

namespace Kangaroo.Core
{
    public interface IKangarooExportManager<T, U>
    {
        IKangarooConverter<T, U> Converter { get; set; }
        IKangarooExportWorker<U> Worker { get; set; }
    }
}
