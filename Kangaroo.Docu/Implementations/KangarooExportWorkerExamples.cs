using System.Collections.Generic;
using Kangaroo.Core;

namespace Kangaroo.Docu.Implementations
{
    #region Example1
	
	// Create a custom export worker implementation corresponding to the output type
    public class KangarooExportWorkerStringToConsole : IKangarooExportWorker<string>
    {
		// Implement the Export method for the enumerable collection of the specific output type, in this case string
        public void Export(IEnumerable<string> input)
        {
			// Define the action to be taken when exporting the data, in this case writing the string items to the console
            foreach (var item in input)
                System.Console.WriteLine($"Kangaroo>> {item}");
        }
    }

    #endregion Example1
}