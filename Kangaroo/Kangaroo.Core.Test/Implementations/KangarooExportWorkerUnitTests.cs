using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

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
            var test = new string[] { "a", "b", "c" };
            obj.Export(test);
            Assert.AreEqual<string>("abc", sw.ToString());
            sw.Close();
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