using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kangaroo.Core.Test.Implementations
{
	[TestClass]
	public class KangarooExportWorkerUnitTests
	{
		[TestMethod]
		public void KangarooExportWorkerTestMethod1()
		{
			StringWriter sw = new StringWriter();
			Console.SetOut(sw);
			var obj = new KangarooExportWorkerStringToConsole();
			List<string> test = new List<string>{ "a", "b", "c" };
			obj.Export(test);
			Assert.AreEqual<string>("abc", sw.ToString());
			sw.Close();

		}

		public class KangarooExportWorkerStringToConsole : IKangarooExportWorker<string>
		{
			public void Export(IEnumerable<string> input)
			{
				foreach (var item in input)
					System.Console.Write(item);
			}
		}
	}
}
