using Kangaroo.Core;
using System;

namespace Kangaroo.Docu.Implementations
{
    #region Example1

    public class KangarooConvertExcpetionToString : IKangarooConverter<Exception, string>
    {
        public string Convert(Exception data)
        {
            return data.ToString();
        }
    }

    #endregion Example1
}