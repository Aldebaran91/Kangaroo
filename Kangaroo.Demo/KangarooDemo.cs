using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kangaroo.Core;


namespace Kangaroo.Demo
{
    class KangarooDemo
    {
        static void Main(string[] args)
        {
            StartTimebasedExport();
        }

        public static void StartTimebasedExport()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">> Kangaroo Demo!");

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

            Console.ResetColor();
            Console.WriteLine(">> Throw ArgumentException!");
            for (int i = 0; i < 7; i++)
            {
                try
                {
                    throw new ArgumentException($"This is a exception message! {i}");
                }
                catch (Exception ex)
                {
                    kangaroo.AddData(ex);
                }
            }

            Task.Delay(1100).Wait();

            Console.WriteLine(">> END DEMO");
        }

        public static void StartAutomaticMax5ItemsExport()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">> Kangaroo Demo!");

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

            Console.ResetColor();
            Console.WriteLine(">> Throw ArgumentException!");
            for (int i = 0; i < 7; i++)
            {
                try
                {
                    throw new ArgumentException($"This is a exception message! {i}");
                }
                catch (Exception ex)
                {
                    kangaroo.AddData(ex);
                }
            }

            Console.WriteLine(">> END DEMO");
        }

        public static void StartManuelExport()
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
