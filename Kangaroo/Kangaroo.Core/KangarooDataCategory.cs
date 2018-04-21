namespace Kangaroo.Core
{
    /// <summary>
    /// Class for categories to be applied optionally to export items.
    /// </summary>
    public sealed class KangarooDataCategory
    {
        /// <summary>
        /// Least severe category for debugging purposes.
        /// </summary>
        public readonly static KangarooDataCategory Debug = new KangarooDataCategory(0, "Debug");

        /// <summary>
        /// Informational level export items.
        /// </summary>
        public readonly static KangarooDataCategory Info = new KangarooDataCategory(1, "Info");

        /// <summary>
        /// Level for warnings.
        /// </summary>
        public readonly static KangarooDataCategory Warning = new KangarooDataCategory(2, "Warning");

        /// <summary>
        /// Level for errors.
        /// </summary>
        public readonly static KangarooDataCategory Error = new KangarooDataCategory(3, "Error");

        /// <summary>
        /// Level for most severe / fatal items.
        /// </summary>
        public readonly static KangarooDataCategory Fatal = new KangarooDataCategory(4, "Fatal");

        /// <summary>
        /// Property for integer values belonging to predefined levels (0-4).
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// Property for string values belonging to predefined level (from Debug to Fatal)
        /// </summary>
        public string Identifier { get; private set; }

		/// <summary>
		/// Parameterized constructor passing the level and identifier values.
		/// </summary>
		/// <param name="level">Integer value belonging to the selected level</param>
		/// <param name="identifier">String value belonging to the selected level</param>
		private KangarooDataCategory(byte level, string identifier)
        {
            this.Level = level;
            this.Identifier = identifier;
        }
    }
}