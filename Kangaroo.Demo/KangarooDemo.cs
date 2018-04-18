using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kangaroo;
using Kangaroo.Core;


namespace Kangaroo.Demo
{
	class KangarooDemo
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting Kangaroo Demo!");

			KangarooStore<Exception> kangaroo = new KangarooStore<Exception>();

			KangarooExporter<Exception, string> exporter = new KangarooExceptionExporter();

			exporter.Converter = new KangarooConvertExcpetionToString();

			exporter.Filter = (x) =>
			{
				return x is NullReferenceException;
			};

			exporter.Worker = new KangarooExportWorkerStringToConsole();

			//kangaroo.ExportHandler.Add(exporter);
			//TODO discuss
			kangaroo.AddExporter(exporter);
			Console.WriteLine("Throwing test exception!");
			try
			{
				throw new System.ArgumentException("Test exception message.");
			}
			catch (Exception ex)
			{
				kangaroo.AddData(ex);
			}
			finally
			{
				Console.WriteLine("Stopping Kangaroo Demo!");
			}

		}


		public class KangarooConvertExcpetionToString : IKangarooConverter<Exception, string>
		{
			public string Convert(Exception data)
			{
				return data.ToString();
			}
		}

		public class KangarooExceptionExporter : KangarooExporter<Exception, string>
		{
			public IKangarooConverter<Exception, string> Converter { get; set; }

			public IKangarooExportWorker<string> Worker { get; set; }
		}

		public class KangarooExportWorkerStringToConsole : IKangarooExportWorker<string>
		{
			public void Export(IEnumerable<string> input)
			{
				foreach (var item in input)
					System.Console.WriteLine($"Kangaroo>> {item}");
			}
		}


	}
}
