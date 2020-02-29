namespace Kangaroo.Core
{
    /// <summary>
    /// Defines the generic method to be implemented for custom export worker implemenations.
    /// </summary>
    /// <typeparam name="T">T is the generic type for the input.</typeparam>
    public interface IKangarooExportWorker<in T>
    {
        /// <summary>
        /// Generic method to export an enumerable collection of data objects.
        /// </summary>
        /// <param name="input">T is the generic type for the input.</param>
        void Export(T[] input);
    }
}