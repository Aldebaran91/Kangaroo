using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kangaroo.Core.Interfaces
{
    public interface IConverter<T, U>
    {
        U Convert(T data);
        Task<U> ConvertAsync(T data);
    }
}
