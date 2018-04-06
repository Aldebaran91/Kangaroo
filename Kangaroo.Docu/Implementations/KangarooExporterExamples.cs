using Kangaroo.Core;
using System;

namespace Kangaroo.Docu.Implementations
{
    public class KangarooExceptionExporter : KangarooExporter<Exception, string>
    {
        public IKangarooConverter<Exception, string> Converter { get; set; }

        public IKangarooExportWorker<string> Worker { get; set; }
    }
}
