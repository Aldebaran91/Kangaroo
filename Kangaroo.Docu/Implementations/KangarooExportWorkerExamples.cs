using System.Collections.Generic;
using Kangaroo.Core;

namespace Kangaroo.Docu.Implementations
{
    #region Example1

    public class KangarooExportWorkerStringToConsole : IKangarooExportWorker<string>
    {
        public void Export(IEnumerable<string> input)
        {
            foreach (var item in input)
                System.Console.WriteLine($"Kangaroo>> {item}");
        }
    }

    #endregion Example1
}