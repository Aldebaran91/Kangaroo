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

            // PLEASE CHANGE!!!
            // First create an instance of Kangaroo
            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>();

            // PLEASE CHANGE!!!
            // Create KangarooExporter
            KangarooExporter<Exception, string> exporter = new KangarooExceptionExporter();
            exporter.Converter = new KangarooConvertExcpetionToString();
            exporter.Filter = (x) =>
            {
                return x is NullReferenceException;
            };
            exporter.Worker = new KangarooExportWorkerStringToConsole();

            // PLEASE CHANGE!!!
            // Add new exporter to exporter list
            kangaroo.ExportHandler.Add(exporter);

            #endregion Example1
        }
    }
}