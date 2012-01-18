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
using OSS.NBEncode.Exceptions;
using OSS.NBEncode.Transforms;
using OSS.NBEncode.Entities;
using OSS.NBEncode.IO;
using OSS.NBEncode.UnitTest.Helpers;



namespace OSS.NBEncode.UnitTest
{
    [TestClass]
    public class BDictionaryTests
    {
        /******************************************
         * Constructor tests
         ******************************************/

        [TestMethod]
        public void DictionaryTransform_Positive()
        {
            var transform = new DictionaryTransform(new BObjectTransform());
            Assert.IsNotNull(transform);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DictionaryTransform_NullBObjectTransform_Exception()
        {
            var transform = new DictionaryTransform(null);
        }


        /******************************************
         * Dictionary encoding tests
         ******************************************/

        [TestMethod]
        public void DictionaryEncode_NoKeyValuePairs_Positive()
        {
            Dictionary<BByteString, IBObject> inputValue = new Dictionary<BByteString, IBObject>();
            BDictionary inputDict = new BDictionary() { Value = inputValue };

            var expectedBytes = "de".GetASCIIBytes();
            var outputStream = new MemoryStream();

            var transform = new DictionaryTransform(new BObjectTransform());
            transform.Encode(inputDict, outputStream);

            outputStream.Position = 0;
            var actualBytes = outputStream.ToArray();

            Assert.IsTrue(expectedBytes.IsEqualWith(actualBytes), "Bytes returned does not match expected bytes");
        }


        [TestMethod]
        public void DictionaryEncode_SimpleSample_Positive()
        {
            // Prep input
            Dictionary<BByteString, IBObject> inputValue = new Dictionary<BByteString, IBObject>();
            inputValue.Add(new BByteString("spam"), new BInteger(4));
            inputValue.Add(new BByteString("ham"), new BByteString("ok"));
            BDictionary inputDict = new BDictionary() { Value = inputValue };

            var expectedBytes = "d3:ham2:ok4:spami4ee".GetASCIIBytes();
            var outputStream = new MemoryStream();

            var transform = new DictionaryTransform(new BObjectTransform());
            transform.Encode(inputDict, outputStream);

            outputStream.Position = 0;
            var actualBytes = outputStream.ToArray();

            Assert.IsTrue(expectedBytes.IsEqualWith(actualBytes), "Bytes returned does not match expected bytes");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DictionaryEncode_NullOutputStream_Exception()
        {
            Dictionary<BByteString, IBObject> inputValue = new Dictionary<BByteString, IBObject>();
            BDictionary inputDict = new BDictionary() { Value = inputValue };
            
            var transform = new DictionaryTransform(new BObjectTransform());
            transform.Encode(inputDict, null);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DictionaryEncode_NullInputDict_Exception()
        {
            var outputStream = new MemoryStream();

            var transform = new DictionaryTransform(new BObjectTransform());
            transform.Encode(null, outputStream);
        }


        /******************************************
         * Dictionary decoding tests
         ******************************************/

        [TestMethod]
        public void DictionaryDecode_NoKeyValuePairs_Positive()
        {
            var inputBytes = "de".GetASCIIBytes();
            var inputStream = new MemoryStream(inputBytes, false);
            inputStream.Position = 0;

            var transform = new DictionaryTransform(new BObjectTransform());
            var outputBDictionary = transform.Decode(inputStream);

            Assert.AreEqual<int>(0, outputBDictionary.Value.Count);
        }


        [TestMethod]
        public void DictionaryDecode_SimpleSample_Positive()
        {
            // Prep input
            var inputBytes = "d4:spami4e3:ham2:oke".GetASCIIBytes();
            var inputStream = new MemoryStream(inputBytes, false);
            inputStream.Position = 0;
            
            // Prep expected data
            KeyValuePair<BByteString, BInteger> expectedKV1 = new KeyValuePair<BByteString,BInteger>(new BByteString("spam"), new BInteger(4));
            KeyValuePair<BByteString, BByteString> expectedKV2 = new KeyValuePair<BByteString, BByteString>(new BByteString("ham"), new BByteString("ok"));
            
            var transform = new DictionaryTransform(new BObjectTransform());
            var outputBDictionary = transform.Decode(inputStream);

            // Assert output data
            var outputDict = outputBDictionary.Value;

            Assert.AreEqual<BObjectType>(BObjectType.Dictionary, outputBDictionary.BType);
            Assert.AreEqual(2, outputDict.Count);

            // Ensure key-value pair 1 is in the output:
            Assert.IsTrue(outputDict.ContainsKey(expectedKV1.Key));
            var outputValue1 = (BInteger) outputDict[expectedKV1.Key];
            Assert.AreEqual<long>(expectedKV1.Value.Value, outputValue1.Value);

            // Ensure key-value pair 2 is in the output:
            Assert.IsTrue(outputDict.ContainsKey(expectedKV2.Key));
            var outputValue2 = (BByteString)outputDict[expectedKV2.Key];
            Assert.IsTrue(expectedKV2.Value.Value.IsEqualWith(outputValue2.Value), "Byte strings are not equal for key-value pair 2");
        }
    }
}
