namespace Kangaroo.Core
{
	/// <summary>
	/// Defines the generic parameters to be implemented for custom exporter implemenations.
	/// </summary>
	/// <typeparam name="T">Generic type for the input.</typeparam>
	/// <typeparam name="U">Generic type for the export</typeparam>
    public interface IKangarooExportManager<T, U>
    {
		/// <summary>
		/// Property to specify data conversion in prepreration for the export.
		/// </summary>
        IKangarooConverter<T, U> Converter { get; set; }

		/// <summary>
		/// Property to specify worker object for the export.
		/// </summary>
        IKangarooExportWorker<U> Worker { get; set; }
    }
}