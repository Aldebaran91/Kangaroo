using System;
using System.Collections.Generic;
using System.Text;

namespace Kangaroo.Core
{
    public interface IExportWorker<T>
    {
        void Export(IEnumerable<T> input);
    }
}
