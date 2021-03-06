﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kangaroo.Core.Test.Implementations.IntegrationTests
{
    public enum MyEnum
    {
        Debug,
        Warning,
        Fatal
    }

    [TestClass]
    public class KangarooStoreTests
    {
        [TestMethod]
        public void KangarooStoreTestMethodAddExporter()
        {
            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>();
            KangarooExporter<Exception, string> exporter = new KangarooExceptionExporter();
            exporter.Converter = new KangarooConvertExcpetionToString();
            exporter.Filter = (x) =>
            {
                return x is NullReferenceException;
            };
            exporter.Worker = new KangarooExportWorkerStringToConsole();
            kangaroo.AddExporter(exporter);
            kangaroo.AddExporter(exporter, MyEnum.Debug);
        }

        [TestMethod]
        public void KangarooStoreTestMethodAddData()
        {
            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>();
            KangarooExporter<Exception, string> exporter = new KangarooExceptionExporter();
            exporter.Converter = new KangarooConvertExcpetionToString();
            exporter.Filter = (x) =>
            {
                return x is NullReferenceException;
            };
            exporter.Worker = new KangarooExportWorkerStringToConsole();
            kangaroo.AddExporter(exporter);
            kangaroo.AddExporter(exporter, MyEnum.Debug);

            // Log the exception without specifying a category
            kangaroo.AddData(new Exception());
            // Log the exception with a specific category
            kangaroo.AddData(new Exception(), MyEnum.Debug);
        }

        [TestMethod]
        public void KangarooStoreTestMethodClearExporter()
        {

            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>();
            KangarooExporter<Exception, string> exporter = new KangarooExceptionExporter();
            exporter.Converter = new KangarooConvertExcpetionToString();
            exporter.Filter = (x) =>
            {
                return x is NullReferenceException;
            };
            exporter.Worker = new KangarooExportWorkerStringToConsole();
            kangaroo.AddExporter(exporter);
            kangaroo.AddExporter(exporter, MyEnum.Debug);

            // Log the exception without specifying a category
            kangaroo.AddData(new Exception());
            // Log the exception with a specific category
            kangaroo.AddData(new Exception(), MyEnum.Debug);

            kangaroo.ClearExporter();
        }

        [TestMethod]
        public void KangarooStoreTestMethodStartManualExport()
        {

            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>();
            KangarooExporter<Exception, string> exporter = new KangarooExceptionExporter();
            exporter.Converter = new KangarooConvertExcpetionToString();
            exporter.Filter = (x) =>
            {
                return x is NullReferenceException;
            };
            exporter.Worker = new KangarooExportWorkerStringToConsole();
            kangaroo.AddExporter(exporter);
            kangaroo.AddExporter(exporter, MyEnum.Debug);

            // Log the exception without specifying a category
            kangaroo.AddData(new Exception());
            // Log the exception with a specific category
            kangaroo.AddData(new Exception(), MyEnum.Debug);

            kangaroo.StartManualExport();
        }

        [TestMethod]
        public void KangarooStoreTestMethodStartManualExportAsync()
        {

            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>();
            KangarooExporter<Exception, string> exporter = new KangarooExceptionExporter();
            exporter.Converter = new KangarooConvertExcpetionToString();
            exporter.Filter = (x) =>
            {
                return x is NullReferenceException;
            };
            exporter.Worker = new KangarooExportWorkerStringToConsole();
            kangaroo.AddExporter(exporter);
            kangaroo.AddExporter(exporter, MyEnum.Debug);

            // Log the exception without specifying a category
            kangaroo.AddData(new Exception());
            // Log the exception with a specific category
            kangaroo.AddData(new Exception(), MyEnum.Debug);

            kangaroo.StartManualExportAsync();
        }

        [TestMethod]
        public void KangarooStoreTestMethodStartTimebasedExport()
        {

            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>();
            KangarooExporter<Exception, string> exporter = new KangarooExceptionExporter();
            exporter.Converter = new KangarooConvertExcpetionToString();
            exporter.Filter = (x) =>
            {
                return x is NullReferenceException;
            };
            exporter.Worker = new KangarooExportWorkerStringToConsole();
            kangaroo.AddExporter(exporter);
            kangaroo.AddExporter(exporter, MyEnum.Debug);

            // Log the exception without specifying a category
            kangaroo.AddData(new Exception());
            // Log the exception with a specific category
            kangaroo.AddData(new Exception(), MyEnum.Debug);

            kangaroo.StartTimebasedExport();
        }

        [TestMethod]
        public void KangarooStoreTestMethodStopTimebasedExport()
        {

            KangarooStore<Exception> kangaroo = new KangarooStore<Exception>();
            KangarooExporter<Exception, string> exporter = new KangarooExceptionExporter();
            exporter.Converter = new KangarooConvertExcpetionToString();
            exporter.Filter = (x) =>
            {
                return x is NullReferenceException;
            };
            exporter.Worker = new KangarooExportWorkerStringToConsole();
            kangaroo.AddExporter(exporter);
            kangaroo.AddExporter(exporter, MyEnum.Debug);

            // Log the exception without specifying a category
            kangaroo.AddData(new Exception());
            // Log the exception with a specific category
            kangaroo.AddData(new Exception(), MyEnum.Debug);

            kangaroo.StopTimebasedExport();
        }

        public class KangarooExceptionExporter : KangarooExporter<Exception, string>
        {
            // Create Converter property for the custom Converter implementation
            public override IKangarooConverter<Exception, string> Converter { get; set; }

            // Create Worker property for the custom Worker implemenation
            public override IKangarooExportWorker<string> Worker { get; set; }

            public override Predicate<Exception> Filter { get; set; }
        }

        public class KangarooConvertExcpetionToString : IKangarooConverter<Exception, string>
        {
            public string Convert(Exception data)
            {
                return data.ToString();
            }
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
