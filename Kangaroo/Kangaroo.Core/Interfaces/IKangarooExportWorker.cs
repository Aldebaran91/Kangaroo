using System.Collections.Generic;

namespace Kangaroo.Core
{
    public interface IKangarooExportWorker<T>
    {
        void Export(IEnumerable<T> input);
    }
}