using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using OSS.NBEncode.UnitTest.Helpers;
using OSS.NBEncode.Exceptions;
using OSS.NBEncode.Transforms;
using OSS.NBEncode.Entities;

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
            var transform = new DictionaryTransform(new ByteStringTransform(), new BObjectTransform());
            Assert.IsNotNull(transform);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DictionaryTransform_NullBByteStringTransform_Exception()
        {
            var transform = new DictionaryTransform(null, new BObjectTransform());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DictionaryTransform_NullBObjectTransform_Exception()
        {
            var transform = new DictionaryTransform(new ByteStringTransform(), null);
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

            var transform = new DictionaryTransform(new ByteStringTransform(), new BObjectTransform());
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
            
            var expectedBytes = "d4:spami4e3:ham2:oke".GetASCIIBytes();
            var outputStream = new MemoryStream();

            var transform = new DictionaryTransform(new ByteStringTransform(), new BObjectTransform());
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
            
            var transform = new DictionaryTransform(new ByteStringTransform(), new BObjectTransform());
            transform.Encode(inputDict, null);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DictionaryEncode_NullInputDict_Exception()
        {
            var outputStream = new MemoryStream();

            var transform = new DictionaryTransform(new ByteStringTransform(), new BObjectTransform());
            transform.Encode(null, outputStream);
        }


        /******************************************
         * Dictionary decoding tests
         ******************************************/

    }
}
