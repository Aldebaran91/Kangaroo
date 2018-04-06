using Kangaroo.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kangaroo.Core
{
    /// <summary>
    /// Defines properties to be implemented for converting and exporting data. If the type T is not the same as U the data will be converted.
    /// </summary>
    /// <typeparam name="T">T is the generic type for the input.</typeparam>
    /// <typeparam name="U">U is the generic type for the export.</typeparam>
    public abstract class ExportManager<T>
    {
        /// <summary>
        /// Property to set criteria for filtering data to be exported.
        /// </summary>
        public Predicate<T> Filter { get; set; } = null;

        public Export<T> Exporter { get; set; }
        
        internal void Export(IList<T> input)
        {
            IEnumerable<T> list = (Filter != null) 
                ? input.Where(x => Filter(x))
                : input;

            ExportData(list);
        }

        public virtual void ExportData(IEnumerable<T> input)
        {
            try
            {
                Exporter.ExportData(input);
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