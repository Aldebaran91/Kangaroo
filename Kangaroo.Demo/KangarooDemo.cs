using Kangaroo.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kangaroo.Demo
{
    internal class KangarooDemo
    {
        private static void Main(string[] args)
        {
            StartTimebasedExport();
            Console.WriteLine();

            StartAutomaticMax5ItemsExport();
            Console.WriteLine();

            StartManuelExport();
        }

        public static void StartTimebasedExport()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(">>>> Kangaroo Demo: Automatic export afte 1 sec");
            Console.ResetColor();

            // Create kangaroostore instance
            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>(new KangarooSettings(TimeSpan.FromSeconds(1), null));

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

            for (int i = 0; i < 7; i++)
            {
                try
                {
                    throw new ArgumentException($"This is an exception message! {i}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($">> Add {i + 1}. exception!");
                    kangaroo.AddData(ex);
                }
            }

            Task.Delay(1100).Wait();

            Console.WriteLine(">> END DEMO");
        }

        public static void StartAutomaticMax5ItemsExport()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(">>>> Kangaroo Demo: Automatic export MAX 5 items");
            Console.ResetColor();

            // Create kangaroostore instance
            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>(new KangarooSettings(5, null));

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

            for (int i = 0; i < 7; i++)
            {
                try
                {
                    throw new ArgumentException($"This is an exception message! {i}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($">> Add {i + 1}. exception!");
                    kangaroo.AddData(ex);
                }
            }

            Console.WriteLine(">> END DEMO");
        }

        public static void StartManuelExport()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(">>>> Kangaroo Demo: Manuel export");
            Console.ResetColor();

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

            try
            {
                throw new ArgumentException("This is an exception message!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($">> Add an exception!");
                kangaroo.AddData(ex);
            }

            Console.WriteLine(">> Start export!");
            kangaroo.StartExport();
            Console.WriteLine(">> END DEMO");
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