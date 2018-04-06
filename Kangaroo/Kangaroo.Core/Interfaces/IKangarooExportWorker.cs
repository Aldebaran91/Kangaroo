using System;
using System.Collections.Generic;
using System.Text;

namespace Kangaroo.Core
{
    public interface IKangarooExportWorker<T>
    {
        void Export(IEnumerable<T> input);
    }
}
