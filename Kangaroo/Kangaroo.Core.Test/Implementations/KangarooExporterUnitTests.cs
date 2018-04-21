using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kangaroo.Core.Test.Implementations
{
	[TestClass]
	public class KangarooExporterUnitTests
	{
		[TestMethod]
		public void KangarooExporterTestMethod1()
		{
			var obj = new KangarooExceptionExporter();
			obj.Worker = new KangarooExportWorkerStringToConsole();
			obj.Converter = new KangarooConvertExcpetionToString();

			Exception[] test = new Exception[2];
			test[0] = new Exception();
			test[1] = new Exception();
			obj.Export(test);
		}

		public class KangarooExceptionExporter : KangarooExporter<Exception, string>
		{
			// Create Converter property for the custom Converter implementation
			public override IKangarooConverter<Exception, string> Converter { get; set; }

			// Create Worker property for the custom Worker implemenation
			public override IKangarooExportWorker<string> Worker { get; set; }
		}

		public class KangarooConvertExcpetionToString : IKangarooConverter<Exception, string>
		{
			public string Convert(Exception data)
			{
				return data.ToString();
			}
		}

		public class KangarooExportWorkerStringToConsole : IKangarooExportWorker<string>
		{
			public void Export(string[] input)
			{
				foreach (var item in input)
				{
					Console.Write(item);
				}
			}
		}

	}
}
