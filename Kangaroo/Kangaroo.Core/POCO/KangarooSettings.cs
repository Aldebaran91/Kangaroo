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
        /// Time interval member to be used as trigger, in case of cumulative export. Corresponding property
        /// provides external access to the appropriate value.
        /// </summary>
        private readonly TimeSpan interval = TimeSpan.Zero;

        /// <summary>
        /// Integer member to maximize the number of collected data items, in case of cumulative export.
        /// Corresponding property provides external access to the appropriate value.
        /// </summary>
        private readonly long maxStoredObjects = -1;

        /// <summary>
        /// Enumerable member with a collection of categories to classify the data to be exported.
        /// Corresponding property provides external access to the appropriate value.
        /// </summary>
        private IReadOnlyList<string> categories = null;

        public KangarooSettings(long maxStoredObjects = 0, IReadOnlyList<string> categories = null)
        {
            this.maxStoredObjects = maxStoredObjects;
            this.categories = categories;
        }

        public KangarooSettings(TimeSpan interval, IReadOnlyList<string> categories = null)
        {
            this.interval = interval;
            this.categories = categories;
        }

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