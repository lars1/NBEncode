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
using OSS.NBEncode.Entities;
using OSS.NBEncode.Transforms;

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
        public void EncodeInteger_PositiveNumber_Positive()
        {
            EncodeIntegerTest(new BInteger(111222333444));
        }


        [TestMethod]
        public void EncodeInteger_Zero_Positive()
        {
            EncodeIntegerTest(new BInteger(0));
        }

        [TestMethod]
        public void EncodeInteger_NegativeNumber_Positive()
        {
            EncodeIntegerTest(new BInteger(-1));
        }


        [TestMethod]
        public void EncodeInteger_MaxValue_Positive()
        {
            EncodeIntegerTest(new BInteger(Int64.MaxValue));
        }


        [TestMethod]
        public void DecodeInteger_PositiveNumber_Positive()
        {
            DecodeIntegerTest(111222333444L, "i111222333444e");
        }


        [TestMethod]
        public void DecodeInteger_Zero_Positive()
        {
            DecodeIntegerTest(0L, "i0e");
        }

        [TestMethod]
        public void DecodeInteger_NegativeNumber_Positive()
        {
            DecodeIntegerTest(-1L, "i-1e");
        }


        [TestMethod]
        public void DecodeInteger_MaxValue_Positive()
        {
            DecodeIntegerTest(Int64.MaxValue, string.Format("i{0}e", Int64.MaxValue));
        }



        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DecodeInteger_NegativeZero_Exception()
        {
            DecodeIntegerTest(0L, "i-0e");
        }




        private void DecodeIntegerTest(long originalNumber, string inputAsStr)
        {
            MemoryStream inputBuffer = new MemoryStream(Encoding.ASCII.GetBytes(inputAsStr));
            inputBuffer.Position = 0;

            var transform = new IntegerTransform();
            var integer = transform.Decode(inputBuffer);

            Assert.IsTrue(integer.Value.GetType() == typeof(long), "value type should be long");
            Assert.AreEqual<long>(originalNumber, integer.Value);
            Assert.AreEqual<BObjectType>(BObjectType.Integer, integer.BType);
        }



        private void EncodeIntegerTest(BInteger input)
        {
            string expected = string.Format("i{0}e", input.Value);

            MemoryStream outputBuffer = new MemoryStream();

            var transform = new IntegerTransform();
            transform.Encode(input, outputBuffer);

            outputBuffer.Position = 0;
            StreamReader sr = new StreamReader(outputBuffer);
            string actual = sr.ReadToEnd();

            Assert.AreEqual<string>(expected, actual);
        }

    }
}
