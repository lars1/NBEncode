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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OSS.NBEncode.Entities;
using OSS.NBEncode.IO;
using OSS.NBEncode.Transforms;
using OSS.NBEncode.UnitTest.Helpers;



namespace OSS.NBEncode.UnitTest
{
    [TestClass]
    public class BObjectTransformTests
    {
        /******************************************
         * Constructor tests
         ******************************************/

        [TestMethod]
        public void BObjectTransform_DefaultCtor_Positive()
        {
            var bObjectTransform = new BObjectTransform();      // just make sure no exceptions are thrown
            Assert.IsNotNull(bObjectTransform);
        }


        /******************************************
         * DecodeNext tests
         ******************************************/

        [TestMethod]
        public void DecodeNext_EmptyString_Positive()
        {
            var dataStream = new MemoryStream(Encoding.UTF8.GetBytes("0:"), false);

            var bot = new BObjectTransform();
            IBObject decodedString = bot.DecodeNext(dataStream);

            Assert.IsNotNull(decodedString);
            Assert.IsInstanceOfType(decodedString, typeof(BByteString));
            var castDecodedString = (BByteString)decodedString;
            Assert.AreEqual<string>(string.Empty, castDecodedString.ConvertToText(Encoding.UTF8));
        }


        [TestMethod]
        public void DecodeNext_NineLetterString_Positive()
        {
            string testWord = "debugging";
            var dataStream = new MemoryStream(Encoding.UTF8.GetBytes("9:" + testWord), false);

            var bot = new BObjectTransform();
            IBObject decodedString = bot.DecodeNext(dataStream);

            Assert.IsNotNull(decodedString);
            Assert.IsInstanceOfType(decodedString, typeof(BByteString));
            var castDecodedString = (BByteString)decodedString;
            Assert.AreEqual<string>(testWord, castDecodedString.ConvertToText(Encoding.UTF8));
        }


        [TestMethod]
        public void DecodeNext_EmptyStringFollowingEmptyString_Positive()
        {
            string str3 = "hello";      // third example string in test data list

            var dataStream = new MemoryStream(Encoding.UTF8.GetBytes("l0:0:5:" + str3 + "e"), false);      // list with two empty string, followed by "hello"

            var bot = new BObjectTransform();
            IBObject decodedList = bot.DecodeNext(dataStream);

            Assert.IsNotNull(decodedList);
            Assert.IsInstanceOfType(decodedList, typeof(BList));

            var castList = (BList)decodedList;

            Assert.AreEqual<int>(3, castList.Value.Length);
            Assert.IsInstanceOfType(castList.Value[0], typeof(BByteString));
            Assert.IsInstanceOfType(castList.Value[1], typeof(BByteString));
            Assert.IsInstanceOfType(castList.Value[2], typeof(BByteString));

            Assert.AreEqual<string>(string.Empty, ((BByteString)castList.Value[0]).ConvertToText(Encoding.UTF8));
            Assert.AreEqual<string>(string.Empty, ((BByteString)castList.Value[1]).ConvertToText(Encoding.UTF8));
            Assert.AreEqual<string>(str3, ((BByteString)castList.Value[2]).ConvertToText(Encoding.UTF8));
        }


        /******************************************
         * EncodeObject tests
         ******************************************/

        [TestMethod]
        public void EncodeObject_BDictionary_Positive()
        {
            string expectedOutput = "d3:one9:value_one3:twoi1ee";
            MemoryStream outputBuffer = new MemoryStream(64);
            
            // Create input test data
            BDictionary data = new BDictionary();
            data.Value.Add(new BByteString("one"), new BByteString("value_one"));
            data.Value.Add(new BByteString("two"), new BInteger(1));
            
            // Test
            var bot = new BObjectTransform();
            bot.EncodeObject(data, outputBuffer);

            // Get result and check it
            int length = (int) outputBuffer.Position;
            string actualOutput = Encoding.UTF8.GetString(outputBuffer.ToArray(), 0, length);

            Assert.AreEqual<string>(expectedOutput, actualOutput);
        }
    }
}
