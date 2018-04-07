using Kangaroo.Core;
using System;

namespace Kangaroo.Docu.Implementations
{
	#region Example1

	// Create a custom converter implementation from type Exception to type string
	public class KangarooExceptionExporter : KangarooExporter<Exception, string>
	{
		// Create Converter property for the custom Converter implementation
		public IKangarooConverter<Exception, string> Converter { get; set; }

		// Create Worker property for the custom Worker implemenation
		public IKangarooExportWorker<string> Worker { get; set; }
	}

	#endregion Example1
}
