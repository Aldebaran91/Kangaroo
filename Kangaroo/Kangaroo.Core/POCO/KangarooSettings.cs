using System;

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
        private readonly uint maxStoredObjects = 0;

        /// <summary>
        /// Creates a new KangarooSetting with max items in queue.
        /// </summary>
        /// <param name="maxStoredObjects"></param>
        public KangarooSettings(uint maxStoredObjects = 0)
        {
            this.maxStoredObjects = maxStoredObjects;
        }

        /// <summary>
        /// Creates a new KangarooSetting with a timespan interval.
        /// </summary>
        /// <param name="interval"></param>
        public KangarooSettings(TimeSpan interval)
        {
            this.interval = interval;
        }

        /// <summary>
        /// Time interval property to be used as trigger, in case of cumulative export.
        /// </summary>
        public TimeSpan Inverval
        {
            get => interval;
        }

        /// <summary>
        /// Integer property to maximize the number of collected data items, in case of cumulative export.
        /// </summary>
        public long MaxStoredObjects
        {
            get => maxStoredObjects;
        }
    }
}