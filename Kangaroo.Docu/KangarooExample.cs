using Kangaroo.Core;
using Kangaroo.Docu.Implementations;
using System;

namespace Kangaroo.Docu
{
	public class KangarooExample
	{
		public void CreateKangaroo()
		{
			#region Example1

			// First create an instance of KangarooStore
			KangarooStore<Exception> kangaroo = new KangarooStore<Exception>();

			// Create an instance of the custom KangarooExporter implementation, in this case an exporter for exceptions
			KangarooExporter<Exception, string> exporter = new KangarooExceptionExporter();

			// Set the Converter property of the exporter to an instance of a custom converter implementation, in this case converting from Exception to string
			exporter.Converter = new KangarooConvertExcpetionToString();

			// Set the Filter property of the exporter
			exporter.Filter = (x) =>
			{
				return x is NullReferenceException;
			};

			// Set the Worker property of the exporter to an instance of a custom export worker, in this case for writing genetrated string output to the console
			exporter.Worker = new KangarooExportWorkerStringToConsole();

			// Add the custom exporter to the export handler (list of exporters to be managed by the library)
			kangaroo.ExportHandler.Add(exporter);

			#endregion Example1
		}
	}
}