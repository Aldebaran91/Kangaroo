using System;
using System.Collections.Generic;

namespace Kangaroo.Core
{
	/// <summary>
	/// Defines properies to be implemented for specifying export settings.
	/// </summary>
	public sealed class KangarooSettings
	{
		/// <summary>
		///  Boolean member to indicate if the export should be continuous, or data should be collected and 
        ///  exported cumulatively based on a trigger. Corresponding property provides external access to the appropriate value.
		/// </summary>
		private readonly bool continousExport;

		/// <summary>
		/// Time interval member to be used as trigger, in case of cumulative export. Corresponding property 
        /// provides external access to the appropriate value.
		/// </summary>
		private readonly TimeSpan interval;

		/// <summary>
		/// Integer member to maximize the number of collected data items, in case of cumulative export. 
        /// Corresponding property provides external access to the appropriate value.
		/// </summary>
		private readonly long maxStoredObjects;

		/// <summary>
		/// Enumerable member with a collection of categories to classify the data to be exported. 
        /// Corresponding property provides external access to the appropriate value.
		/// </summary>
		private IReadOnlyList<string> categories;

		/// <summary>
		/// BETA BETA BETA BETA
		/// </summary>
		/// <param name="continousExport"></param>
		/// <param name="maxStoredObjects"></param>
		/// <param name="categories"></param>
		public KangarooSettings(bool continousExport = true, long maxStoredObjects = 0, IReadOnlyList<string> categories = null)
		{
			this.continousExport = continousExport;
			this.interval = TimeSpan.Zero;
			this.maxStoredObjects = maxStoredObjects;
			this.categories = categories;
		}

		/// <summary>
		/// BETA BETA BETA BETA
		/// </summary>
		/// <param name="interval"></param>
		/// <param name="continousExport"></param>
		/// <param name="maxStoredObjects"></param>
		/// <param name="categories"></param>
		public KangarooSettings(TimeSpan interval, bool continousExport = false, long maxStoredObjects = 0,
            IReadOnlyList<string> categories = null)
		{
			this.interval = interval;
			this.continousExport = continousExport;
			this.interval = TimeSpan.Zero;
			this.maxStoredObjects = maxStoredObjects;
			this.categories = categories;
		}

		/// <summary>
		/// Boolean property to indicate if the export should be continuous, or data should be collected and 
        /// exported cumulatively based on a trigger.
		/// </summary>
		public bool ContinousExport { get => continousExport; }

		/// <summary>
		/// Time interval property to be used as trigger, in case of cumulative export.
		/// </summary>
		public TimeSpan Inverval { get => interval; }

		/// <summary>
		/// Integer property to maximize the number of collected data items, in case of cumulative export.
		/// </summary>
		public long MaxStoredObjects { get => maxStoredObjects; }

		/// <summary>
		/// Enumerable property with a collection of categories to classify the data to be exported.
		/// </summary>
		public IReadOnlyList<string> Categories { get => categories; }
	}
}