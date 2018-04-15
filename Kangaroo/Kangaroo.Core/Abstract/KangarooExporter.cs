using Kangaroo.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kangaroo.Core
{
	/// <summary>
	/// Abstract class to provide basis for custom exporter implementations.
	/// </summary>
	/// <typeparam name="T">T is the generic type for the input</typeparam>
	/// <typeparam name="U">U is the generic type for the export</typeparam>
	/// <example>
	/// <code
	/// source="..\Kangaroo.Docu\Implementations\KangarooExporterExamples.cs"
	/// region="Example1"
	/// title="How to create a custom exporter implementation"
	/// language="csharp"/>
	/// </example>
	public abstract class KangarooExporter<T, U> : IKangarooExportManager<T, U>, IKangarooExportWorker<T>
	{
		/// <summary>
		/// Property to set criteria for filtering data to be exported.
		/// </summary>
		public Predicate<T> Filter { get; }

		/// <summary>
		/// Property to specify data conversion in prepreration for the export.
		/// </summary>
		public IKangarooConverter<T, U> Converter { get; }

		/// <summary>
		/// Property to specify and access worker object for the export.
		/// </summary>
	    public IKangarooExportWorker<U> Worker { get; }

        public KangarooExporter(IKangarooExportWorker<U> worker, IKangarooConverter<T, U> converter, Predicate<T> filter = null)
        {
            this.Worker = worker;
            this.Converter = converter;
            this.Filter = filter;
        }

		/// <summary>
		/// Method for exporting the converted and filtered data utilizing the export worker.
		/// </summary>
		/// <param name="input">Enumerable collection of data as input for the export.</param>
		public void Export(IEnumerable<T> input)
		{
			try
			{
				IEnumerable<T> list = (Filter != null)
					? input.Where(x => Filter(x))
					: input;

                if (Converter == null)
                {
                    Worker.Export(list.Select(x => (U)Convert.ChangeType(x, typeof(U))));
                }
                else
                {
                    Worker.Export(list.Select(x => Converter.Convert(x)));
                }
			}
			catch (NullReferenceException)
			{
				throw new NoExportFoundException("No IKangarooExportWorker was added to KangarooExporter");
			}
			catch (Exception exp)
			{
				throw;
			}
		}
	}
}