using Kangaroo.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kangaroo.Core
{
    public abstract class Exporter<T, U> : IKangarooExportManager<T, U>, IKangarooExportWorker<T>
    {
        /// <summary>
        /// Property to set criteria for filtering data to be exported.
        /// </summary>
        public Predicate<T> Filter { get; set; } = null;

        public IKangarooConverter<T, U> Converter { get; set; }

        public IKangarooExportWorker<U> Worker { get; set; }
                
        public void Export(IEnumerable<T> input)
        {
            try
            {
                IEnumerable<T> list = (Filter != null)
                    ? input.Where(x => Filter(x))
                    : input;

                if (Converter == null)
                    Worker.Export(list.Select(x => (U)Convert.ChangeType(x, typeof(U))));
                else
                    Worker.Export(list.Select(x => Converter.Convert(x)));
            }
            catch (NullReferenceException exp)
            {
                throw new NoExportFoundException("");
            }
            catch (Exception exp)
            {
                throw new Exception("");
            }
        }
    }
}