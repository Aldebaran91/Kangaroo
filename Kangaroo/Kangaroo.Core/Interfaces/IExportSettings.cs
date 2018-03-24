using System;
using System.Collections.Generic;
using System.Text;

namespace Kangaroo.Core.Interfaces
{
    public interface IExportSettings
    {
        bool ContinousExport { get; set; }
        TimeSpan LapsOfTimeExport { get; set; }
        long MaxValuesStored { get; set; }
        IEnumerable<string> Categories { get; set; }
    }
}
