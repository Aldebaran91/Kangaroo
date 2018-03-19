using Kangaroo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kangaroo.Core.Abstract
{
    public abstract class Kangaroo
    {
        public abstract IEnumerable<object> LoggingData { get; set; }
        public abstract IEnumerable<IExportManager<object, object>> ExportHandler { get; set; }
        public abstract IExportSettings ExportSettings { get; set; }

        public abstract void AddData(object data);
        public abstract Task AddDataAsync(object data);
        public abstract void AddData(object data, string category);
        public abstract Task AddDataAsync(object data, string category);
        public abstract void StartExport();
        public abstract Task StartExportAsync();
    }
}
