using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using OSS.NBEncode.Entities;
using OSS.NBEncode.Transforms;
using OSS.NBEncode.UnitTest.Helpers;

namespace OSS.NBEncode.UnitTest
{
    [TestClass]
    public class BEncodingListTests
    {
        [TestMethod]
        public void ListEncode_EmptyList_Positive()
        {
            BList inputList = new BList() { Value = new IBObject[] { } };
            var outputStream = new MemoryStream();
            var expectedBytes = Encoding.ASCII.GetBytes("le");

            var transform = new ListTransform(new BObjectTransform());

            transform.Encode(inputList, outputStream);

            outputStream.Position = 0;
            var actualBytes = outputStream.ToArray();

            Assert.IsTrue(expectedBytes.IsEqualWith(actualBytes), "Bytes returned does not match expected bytes");
        }
    }
}
