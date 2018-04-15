namespace Kangaroo.Core
{
    /// <summary>
    ///
    /// </summary>
    public sealed class KangarooDataCategory
    {
        /// <summary>
        ///
        /// </summary>
        public readonly static KangarooDataCategory Debug = new KangarooDataCategory(0, "Debug");

        /// <summary>
        ///
        /// </summary>
        public readonly static KangarooDataCategory Info = new KangarooDataCategory(1, "Info");

        /// <summary>
        ///
        /// </summary>
        public readonly static KangarooDataCategory Warning = new KangarooDataCategory(2, "Warning");

        /// <summary>
        ///
        /// </summary>
        public readonly static KangarooDataCategory Error = new KangarooDataCategory(3, "Error");

        /// <summary>
        ///
        /// </summary>
        public readonly static KangarooDataCategory Fatal = new KangarooDataCategory(4, "Fatal");

        /// <summary>
        ///
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string Identifier { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <param name="identifier"></param>
        private KangarooDataCategory(byte level, string identifier)
        {
            this.Level = level;
            this.Identifier = identifier;
        }
    }
}