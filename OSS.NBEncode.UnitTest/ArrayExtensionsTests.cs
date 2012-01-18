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

using OSS.NBEncode.IO;


namespace OSS.NBEncode.UnitTest
{
    [TestClass]
    public class ArrayExtensionsTests
    {
        [TestMethod]
        public void RawCompare_FirstIsGreater_PlusOne()
        {
            byte[] first = new byte[] { 1, 255 };
            byte[] second = new byte[] { 1, 100 };

            Assert.AreEqual<int>(1, first.RawCompare(second));
        }


        [TestMethod]
        public void RawCompare_FirstIsLongerAndGreater_PlusOne()
        {
            byte[] first = new byte[] { 1, 2, 3  };
            byte[] second = new byte[] { 1, 2 };

            Assert.AreEqual<int>(1, first.RawCompare(second));
        }


        [TestMethod]
        public void RawCompare_FirstIsLongerButSmaller_MinusOne()
        {
            byte[] first = new byte[] { 1, 1, 3 };
            byte[] second = new byte[] { 1, 2 };

            Assert.AreEqual<int>(-1, first.RawCompare(second));
        }


        [TestMethod]
        public void RawCompare_SecondIsGreater_MinusOne()
        {
            byte[] first = new byte[] { 1, 100 };
            byte[] second = new byte[] { 1, 255 };

            Assert.AreEqual<int>(-1, first.RawCompare(second));
        }


        [TestMethod]
        public void RawCompare_SecondIsLongerAndGreater_MinusOne()
        {
            byte[] first = new byte[] { 1, 2 };
            byte[] second = new byte[] { 1, 2, 3 };

            Assert.AreEqual<int>(-1, first.RawCompare(second));
        }


        [TestMethod]
        public void RawCompare_SecondIsLongerButSmaller_PlusOne()
        {
            byte[] first = new byte[] { 1, 2 };
            byte[] second = new byte[] { 1, 1, 3 };

            Assert.AreEqual<int>(1, first.RawCompare(second));
        }


        [TestMethod]
        public void RawCompare_TwoEqualArrays_Zero()
        {
            byte[] first = new byte[] { 1, 2, 3 };
            byte[] second = new byte[] { 1, 2, 3 };

            Assert.AreEqual<int>(0, first.RawCompare(second));
        }


        [TestMethod]
        public void RawCompare_DoSorting_Postive()
        {
            byte[][] unsorted = new byte[][] 
            { 
                new byte[]{ 6 }, 
                new byte[]{ 2 }, 
                new byte[]{ 3, 4, 5 }, 
                new byte[]{ 1 }  
            };

            byte[][] expectedSorting = new byte[][]
            {
                new byte[]{ 1 },
                new byte[]{ 2 },
                new byte[]{ 3, 4, 5 },
                new byte[]{ 6 }
            };

            
            List<byte[]> messyList = new List<byte[]>(unsorted);

            messyList.Sort(new Comparison<byte[]>((first, second) => 
                {
                    return first.RawCompare(second);
                }));


            for (int i=0; i<messyList.Count; i++)
            {
                Assert.IsTrue(messyList[i].IsEqualWith(expectedSorting[i]), "byte arrays not in expected sorted order");
            }
        }


        [TestMethod]
        public void IsEqualWith_EqualArrays_True()
        {
            byte[] first = new byte[] { 1, 2, 3 };
            byte[] second = new byte[] { 1, 2, 3 };

            Assert.IsTrue(first.IsEqualWith(second));
        }


        [TestMethod]
        public void IsEqualWith_EmptyArrays_True()
        {
            byte[] first = new byte[] { };
            byte[] second = new byte[] { };

            Assert.IsTrue(first.IsEqualWith(second));
        }


        [TestMethod]
        public void IsEqualWith_FirstLonger_False()
        {
            byte[] first = new byte[] { 1, 2, 3 };
            byte[] second = new byte[] { 1, 2 };

            Assert.IsFalse(first.IsEqualWith(second));
        }

        [TestMethod]
        public void IsEqualWith_FirstShorter_False()
        {
            byte[] first = new byte[] { 1, 2 };
            byte[] second = new byte[] { 1, 2, 3 };

            Assert.IsFalse(first.IsEqualWith(second));
        }

        [TestMethod]
        public void IsEqualWith_EqualLengthUnequalValues_False()
        {
            byte[] first = new byte[] { 1, 2, 9 };
            byte[] second = new byte[] { 1, 2, 3 };

            Assert.IsFalse(first.IsEqualWith(second));
        }
    }
}
