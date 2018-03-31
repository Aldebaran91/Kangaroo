using System;
using System.Collections.Generic;
using System.Text;

namespace Kangaroo.Core.Interfaces
{
    /// <summary>
    /// Defines properies to be implemented for specifying export settings.
    /// </summary>
    public interface IExportSettings
    {
        /// <summary>
        /// Boolean property to indicate if the export should be continuous, or data should be collected and exported cumulatively based on a trigger. 
        /// </summary>
        bool ContinousExport { get; set; }

        /// <summary>
        /// Time interval property to be used as trigger, in case of cumulative export.
        /// </summary>
        TimeSpan LapsOfTimeExport { get; set; }

        /// <summary>
        /// Integer property to maximize the number of collected data items, in case of cumulative export.
        /// </summary>
        long MaxValuesStored { get; set; }

        /// <summary>
        /// Enumerable property with a collection of categories to classify the data to be exported.
        /// </summary>
        IEnumerable<string> Categories { get; set; }
    }
}
