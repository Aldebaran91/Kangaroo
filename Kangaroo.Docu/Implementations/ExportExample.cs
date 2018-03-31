using Kangaroo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kangaroo.Docu.Implementations
{
    #region Example1
    public class ExportExample : IExport<string>
    {
        public Predicate<string> Filter
        {
            get
            {
                return (string input) => { return input.Length > 5; };
            }
            set
            {
            }
        }

        public void ExportData(IEnumerable<string> data)
        {
        }

        public Task ExportDataAsync(IEnumerable<string> data)
        {
            return Task.Run(() =>
            {
            });
        }
    }
    #endregion
}