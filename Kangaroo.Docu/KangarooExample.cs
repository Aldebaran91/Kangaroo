using Kangaroo.Core.Interfaces;
using Kangaroo.Docu.Implementations;
using System;
using System.Collections.Generic;

namespace Kangaroo.Docu
{
    public class KangarooExample
    {
        public void CreateKangaroo()
        {
            #region Example1

            // First create an instance of Kangaroo
            Kangaroo kangaroo = new Kangaroo();

            IExportManager<String, String> exportManager = new ExportHandlerExample();

            kangaroo.ExportHandler = new List<IExportManager<object, object>>()
            {
                
            };
            
            #endregion KangarooExample1
        }
    }
}