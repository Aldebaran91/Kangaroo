namespace Kangaroo.Core
{
    public interface IKangarooExportManager<T, U>
    {
        IKangarooConverter<T, U> Converter { get; set; }

        IKangarooExportWorker<U> Worker { get; set; }
    }
}