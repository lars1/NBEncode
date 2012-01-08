using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using OSS.NBEncode.UnitTest.Helpers;

namespace OSS.NBEncode.UnitTest
{
    [TestClass]
    public class BEncodingByteStringTests
    {
        [TestMethod]
        public void EncodeByteString_SimpleScenario_Positive()
        {
            // Prepare input and expected output value
            byte[] inputBytes = {1,2,3,4};
            
            byte[] expectedBytes = {0,0,1,2,3,4};
            expectedBytes[0] = EncodingHelper.GetASCIIValue('4');
            expectedBytes[1] = EncodingHelper.GetASCIIValue(':');

            MemoryStream outputStream = new MemoryStream(6);

            // Do encoding
            BEncoding benc = new BEncoding();
            benc.EncodeByteString(inputBytes.LongLength, new MemoryStream(inputBytes), outputStream);

            // Check results
            Assert.AreEqual<long>(expectedBytes.LongLength, outputStream.Position);         // Ensure 6 bytes were (likely) written
            
            outputStream.Position = 0;
            byte[] outputtedBytes = outputStream.ToArray();

            Assert.IsTrue(outputtedBytes.IsEqualWith(expectedBytes), "Outputted bytes are different than expected");
        }

        [TestMethod]
        public void EncodeByteString_NoBytesInInput_Positive()
        {
            // Prepare input and expected output value
            byte[] inputBytes = { };

            byte[] expectedBytes = { 0, 0 };
            expectedBytes[0] = EncodingHelper.GetASCIIValue('0');
            expectedBytes[1] = EncodingHelper.GetASCIIValue(':');

            MemoryStream outputStream = new MemoryStream(6);

            // Do encoding
            BEncoding benc = new BEncoding();
            benc.EncodeByteString(inputBytes.LongLength, new MemoryStream(inputBytes), outputStream);

            // Check results
            outputStream.Position = 0;
            byte[] outputtedBytes = outputStream.ToArray();

            Assert.IsTrue(outputtedBytes.IsEqualWith(expectedBytes), "Outputted bytes are different than expected");
        }

        [TestMethod]
        public void EncodeByteString_NullInputBytes_Exception()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void EncodeByteString_MismatchBetweenInputByteCountAndStream_Exception()
        {
            Assert.Inconclusive();
        }


        [TestMethod]
        public void EncodeByteString_NullOutputStream_Exception()
        {
            Assert.Inconclusive();
        }
    }
}
