using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using OSS.NBEncode.Entities;

namespace OSS.NBEncode.UnitTest
{
    [TestClass]
    public class BEncodingIntegerTests
    {
        public BEncodingIntegerTests()
        {
        }

        #region Test context

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestEncodeInteger_PositiveNumber_Positive()
        {
            EncodeIntegerTest(111222333444);
        }


        [TestMethod]
        public void TestEncodeInteger_Zero_Positive()
        {
            EncodeIntegerTest(0);
        }

        [TestMethod]
        public void TestEncodeInteger_NegativeNumber_Positive()
        {
            EncodeIntegerTest(-1);
        }


        [TestMethod]
        public void TestEncodeInteger_MaxValue_Positive()
        {
            EncodeIntegerTest(Int64.MaxValue);
        }


        [TestMethod]
        public void TestDecodeInteger_PositiveNumber_Positive()
        {
            DecodeIntegerTest(111222333444L, "i111222333444e");
        }


        [TestMethod]
        public void TestDecodeInteger_Zero_Positive()
        {
            DecodeIntegerTest(0L, "i0e");
        }

        [TestMethod]
        public void TestDecodeInteger_NegativeNumber_Positive()
        {
            DecodeIntegerTest(-1L, "i-1e");
        }


        [TestMethod]
        public void TestDecodeInteger_MaxValue_Positive()
        {
            DecodeIntegerTest(Int64.MaxValue, string.Format("i{0}e", Int64.MaxValue));
        }



        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestDecodeInteger_NegativeZero_Exception()
        {
            DecodeIntegerTest(0L, "i-0e");
        }




        private void DecodeIntegerTest(long originalNumber, string inputAsStr)
        {
            MemoryStream inputBuffer = new MemoryStream(Encoding.ASCII.GetBytes(inputAsStr));
            inputBuffer.Position = 0;

            var bencode = new BEncoding();
            BInteger integer = bencode.DecodeInteger(inputBuffer);

            Assert.IsTrue(integer.ValueType == typeof(long), "value type should be long");
            Assert.AreEqual<long>(originalNumber, integer.Value);
            Assert.AreEqual<BObjectType>(BObjectType.Integer, integer.BType);
        }



        private void EncodeIntegerTest(long input)
        {
            string expected = string.Format("i{0}e", input);

            MemoryStream outputBuffer = new MemoryStream();

            var bencode = new BEncoding();
            bencode.EncodeInteger(input, outputBuffer);

            outputBuffer.Position = 0;
            StreamReader sr = new StreamReader(outputBuffer);
            string actual = sr.ReadToEnd();

            Assert.AreEqual<string>(expected, actual);
        }

    }
}
