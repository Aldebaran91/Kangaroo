using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kangaroo.Core.Interfaces
{
    public interface IExport<U>
    {
        Predicate<U> Filter { get; set; }

        void ExportData(IEnumerable<U> data);
        Task ExportDataAsync(IEnumerable<U> data);
    }
}
