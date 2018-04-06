using Kangaroo.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kangaroo.Core
{
    public abstract class Exporter<T, U> : IExportManager<T, U>, IExportWorker<T>
    {
        /// <summary>
        /// Property to set criteria for filtering data to be exported.
        /// </summary>
        public Predicate<T> Filter { get; set; } = null;

        public IConverter<T, U> Converter { get; set; }

        public IExportWorker<U> Worker { get; set; }

        public void Export(IEnumerable<T> input)
        {
            try
            {
                IEnumerable<T> list = (Filter != null)
                    ? input.Where(x => Filter(x))
                    : input;

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