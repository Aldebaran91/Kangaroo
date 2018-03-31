using Kangaroo.Core.Interfaces;
using System.Threading.Tasks;

namespace Kangaroo.Docu.Implementations
{
    #region Example1
    public class ConverterExample : IConverter<int, string>
    {
        public string Convert(int data)
        {
            return data.ToString();
        }

        public Task<string> ConvertAsync(int data)
        {
            return new Task<string>(() =>
            {
                return data.ToString();
            });
        }
    }
    #endregion
}