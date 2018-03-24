using Kangaroo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kangaroo
{
    public sealed class Kangaroo
    {
        public IEnumerable<object> LoggingData { get; set; }
        public IEnumerable<IExportManager<object, object>> ExportHandler { get; set; }
        public IExportSettings ExportSettings { get; set; }

        public void AddData(object data)
        {
            throw new NotImplementedException();
        }

        public Task AddDataAsync(object data)
        {
            throw new NotImplementedException();
        }

        public void AddData(object data, string category)
        {
            throw new NotImplementedException();
        }

        public Task AddDataAsync(object data, string category)
        {
            throw new NotImplementedException();
        }

        public void StartExport()
        {
            throw new NotImplementedException();
        }

        public Task StartExportAsync()
        {
            throw new NotImplementedException();
        }
    }
}
