/**************************************************************

Copyright 2012, Lars Warholm, Norway (lars@witservices.no)

This file is part of NBEncode, a .NET library for encoding and decoding
"bencoded" data

NBEncode is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

NBEncode is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with NBEncode.  If not, see <http://www.gnu.org/licenses/>.

**************************************************************/
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using OSS.NBEncode.IO;
using OSS.NBEncode.Exceptions;
using OSS.NBEncode.Transforms;
using OSS.NBEncode.Entities;
using OSS.NBEncode.UnitTest.Helpers;

namespace OSS.NBEncode.UnitTest
{
    [TestClass]
    public class BEncodingByteStringTests
    {

        /************************************************
         * Encoding byte string tests
         ************************************************/
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
            BByteString bstrObj = new BByteString()
            {
                Value = inputBytes
            };

            var transform = new ByteStringTransform();
            transform.Encode(bstrObj, outputStream);
            
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
            BByteString bstrObj = new BByteString()
            {
                Value = inputBytes
            };

            var transform = new ByteStringTransform();
            transform.Encode(bstrObj, outputStream);


            // Check results
            outputStream.Position = 0;
            byte[] outputtedBytes = outputStream.ToArray();

            Assert.IsTrue(outputtedBytes.IsEqualWith(expectedBytes), "Outputted bytes are different than expected");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncodeByteString_NullInputObject_Exception()
        {
            MemoryStream outputStream = new MemoryStream();

            var transform = new ByteStringTransform();
            transform.Encode(null, outputStream);


        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncodeByteString_NullOutputStream_Exception()
        {
            BByteString bstrObj = new BByteString()
            {
                Value = new byte[] { }
            };

            var transform = new ByteStringTransform();
            transform.Encode(bstrObj, null);
        }
        
        
        /************************************************
         * Decode byte string tests
         ************************************************/
        [TestMethod]
        public void DecodeByteString_SimpleScenario_Positive()
        {
            var inputStream = new MemoryStream(new byte[] { 52, 58, 49, 50, 51, 52 }, false);       // "4:1234"
            var expectedOutput = new byte[] { 49, 50, 51, 52 };
 
            var transform = new ByteStringTransform();
            var bstrObj = transform.Decode(inputStream);

            Assert.IsTrue(expectedOutput.IsEqualWith(bstrObj.Value), "Outputted bytes are different than expected");
        }


        [TestMethod]
        public void DecodeByteString_NoBytesInInput_Positive()
        {
            var inputStream = new MemoryStream(new byte[] { 48, 58 }, false);       // "0:"

            var transform = new ByteStringTransform();
            var bstrObj = transform.Decode(inputStream);

            Assert.AreEqual<long>(0L, bstrObj.Value.Length);
        }



        [TestMethod]
        [ExpectedException(typeof(BEncodingException))]
        public void DecodeByteString_NotEnoughBytesInInput_Exception()
        {
            var input = "100:12345";
            var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(input), false);

            var transform = new ByteStringTransform();
            var bstrObj = transform.Decode(inputStream);
        }


        [TestMethod]
        [ExpectedException(typeof(BEncodingException))]
        public void DecodeByteString_InsaneByteStringLength_Exception()
        {
            var input = "12345678901234567890:123";
            var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(input), false);

            var transform = new ByteStringTransform();
            var bstrObj = transform.Decode(inputStream);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DecodeByteString_NullInputStream_Exception()
        {
            var transform = new ByteStringTransform();
            var bstrObj = transform.Decode(null);
        }
    }
}
