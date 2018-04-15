namespace Kangaroo.Core
{
    /// <summary>
    /// Defines the generic methods to be implemented for converting data from a data type (T) to another (U). Provides basis for custom converter implemenations.
    /// </summary>
    /// <typeparam name="T">T is the generic type for the input.</typeparam>
    /// <typeparam name="U">U is the generic type for the export.</typeparam>
    /// <example>
    /// <code
    /// source="..\Kangaroo.Docu\Implementations\KangarooConverterExamples.cs"
    /// region="Example1"
    /// title="How create a converter for the export manager"
    /// language="csharp"/>
    /// </example>
    public interface IKangarooConverter<T, U>
    {
        /// <summary>
        /// Generic method to convert data from a data type (T) to another (U). Conversion is perfomed in preparation for exporting data.
        /// </summary>
        /// <param name="data">Data is the object which should be converted to the generic type U</param>
        /// <returns>Return a new type converted from input data.</returns>
        U Convert(T data);
    }
}