using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kangaroo.Core.Test.Implementations.UnitTests
{
    [TestClass]
    public class KangarooConverterTests
    {
        [TestMethod]
        public void KangarooConverterTestMethodConvert()
        {
            var obj = new KangarooConvertExcpetionToString();
            var result = obj.Convert(new Exception());
            Assert.IsInstanceOfType(result, typeof(string));
        }

        public class KangarooConvertExcpetionToString : IKangarooConverter<Exception, string>
        {
            public string Convert(Exception data)
            {
                return data.ToString();
            }
        }
    }
}