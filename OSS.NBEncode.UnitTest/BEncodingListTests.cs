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
        public void ListTransform_ValidCtorParam_Positive()
        {
            var transform = new ListTransform(new BObjectTransform());      // test that this does not cause exception
            Assert.IsNotNull(transform);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ListTransform_NullCtorParam_Exception()
        {
            var transform = new ListTransform(null);
        }
        

        [TestMethod]
        public void ListEncode_EmptyList_Positive()
        {
            BList inputList = new BList() { Value = new IBObject[] { } };

            var expectedBytes = "le".GetASCIIBytes();
            var outputStream = new MemoryStream();
            
            var transform = new ListTransform(new BObjectTransform());

            transform.Encode(inputList, outputStream);

            outputStream.Position = 0;
            var actualBytes = outputStream.ToArray();

            Assert.IsTrue(expectedBytes.IsEqualWith(actualBytes), "Bytes returned does not match expected bytes");
        }


        [TestMethod]
        public void ListEncode_PopulatedList_Positive()
        {
            BList inputList = new BList() 
            { 
                Value = new IBObject[] 
                {
                    new BInteger(9L), new BByteString() { Value = "spam".GetASCIIBytes() }
                } 
            };

            var expectedBytes = "li9e4:spame".GetASCIIBytes();
            var outputStream = new MemoryStream();
            
            var transform = new ListTransform(new BObjectTransform());

            transform.Encode(inputList, outputStream);

            outputStream.Position = 0;
            var actualBytes = outputStream.ToArray();
            
            Assert.IsTrue(expectedBytes.IsEqualWith(actualBytes), "Bytes returned does not match expected bytes");
        }


        [TestMethod]
        public void ListEncode_NestedList_Positive()
        {
            // A list with an empty list as it's only item:
            BList inputList = new BList()
            {
                Value = new IBObject[] 
                {
                    new BList() 
                    {
                        Value = new IBObject[] { }
                    }
                }
            };

            var expectedBytes = "llee".GetASCIIBytes();
            var outputStream = new MemoryStream();

            var transform = new ListTransform(new BObjectTransform());

            transform.Encode(inputList, outputStream);

            outputStream.Position = 0;
            var actualBytes = outputStream.ToArray();

            Assert.IsTrue(expectedBytes.IsEqualWith(actualBytes), "Bytes returned does not match expected bytes");
        }
        

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ListEncode_NullListParam_Exception()
        {
            var outputStream = new MemoryStream();
            var transform = new ListTransform(new BObjectTransform());

            transform.Encode(null, outputStream);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ListEncode_NullOutputStream_Exception()
        {
            BList inputList = new BList() { Value = new IBObject[] { } };
            var transform = new ListTransform(new BObjectTransform());

            transform.Encode(inputList, null);
        }
    }
}
