using Kangaroo.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kangaroo.Tests.Implementations.IntegrationTests
{
    [TestClass]
    public class KangarooTimeBasedExportTests
    {
        private Mock<KangarooExporter<Exception, string>> KangarooExporterExceptionString;
        private Mock<IKangarooExportWorker<string>> KangrooExportWorker;
        
        [TestInitialize]
        public void Init()
        {
            KangrooExportWorker = new Mock<IKangarooExportWorker<string>>();

            KangarooExporterExceptionString = new Mock<KangarooExporter<Exception, string>>();
            KangarooExporterExceptionString.SetupGet(x => x.Converter).Returns(new KangarooConvertExcpetionToString());
            KangarooExporterExceptionString.SetupGet(x => x.Filter).Returns((x) =>
            {
                return x is ArgumentException;
            });
            KangarooExporterExceptionString.SetupGet(x => x.Worker).Returns(KangrooExportWorker.Object);
        }

        [TestMethod]
        public void TimeBasedExport_5Exported_Ok()
        {

            // Create kangaroostore instance
            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>(new KangarooSettings(TimeSpan.FromSeconds(1)));

            // Create kangaroo export handler
            KangarooExporter<Exception, string> exporter = KangarooExporterExceptionString.Object;

            // Add exporter to kangaroo store
            kangaroo.AddExporter(exporter);

            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($">> Add {i + 1}. exception!");
                kangaroo.AddData(new ArgumentException($"This is an exception message! {i}"));
            }

            Task.Delay(1100).Wait();

            Console.WriteLine(">> END DEMO");

            kangaroo.StopTimebasedExport();

            KangrooExportWorker.Verify(x => x.Export(It.IsAny<string[]>()), Times.Once());
        }

        public class KangarooConvertExcpetionToString : IKangarooConverter<Exception, string>
        {
            public string Convert(Exception data)
            {
                return data.Message;
            }
        }
    }
}
