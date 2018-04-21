namespace Kangaroo.Core
{
    /// <summary>
    /// Defines the generic method to be implemented for custom export worker implemenations.
    /// </summary>
    /// <typeparam name="T">T is the generic type for the input.</typeparam>
    /// <example>
    /// <code
    /// source="..\Kangaroo.Docu\Implementations\KangarooExportWorkerExamples.cs"
    /// region="Example1"
    /// title="How to create a custom export worker implementation corresponding to the output type"
    /// language="csharp"/>
    /// </example>
    public interface IKangarooExportWorker<in T>
    {
        /// <summary>
        /// Generic method to export an enumerable collection of data objects.
        /// </summary>
        /// <param name="input">T is the generic type for the input.</param>
        void Export(T[] input);
    }
}