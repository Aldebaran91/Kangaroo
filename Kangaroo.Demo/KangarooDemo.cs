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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">> Kangaroo Demo!");

            // Create kangaroostore instance
            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>();

            // Create kangaroo export handler
            KangarooExporter<Exception, string> exporter = new KangarooExceptionExporter();

            // Add converter
            exporter.Converter = new KangarooConvertExcpetionToString();

            // Add filter
            exporter.Filter = (x) =>
            {
                return x is ArgumentException;
            };

            // Add exporter
            exporter.Worker = new KangarooExportWorkerStringToConsole();

            // Add exporter to kangaroo store
            kangaroo.AddExporter(exporter);

            Console.ResetColor();
            Console.WriteLine(">> Throw ArgumentException!");
            try
            {
                throw new ArgumentException("This is a exception message!");
            }
            catch (Exception ex)
            {
                kangaroo.AddData(ex);
            }

            Console.WriteLine(">> Start export of kangaroo manualy.\n");
            kangaroo.StartExport();
        }


        public class KangarooConvertExcpetionToString : IKangarooConverter<Exception, string>
        {
            public string Convert(Exception data)
            {
                return data.Message;
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
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"Kangaroo>> ");
                    Console.ResetColor();
                    Console.WriteLine(item);
                }
            }
        }


    }
}
