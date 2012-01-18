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
using System.Linq;
using System.Text;
using System.IO;
using OSS.NBEncode.IO;
using OSS.NBEncode.Exceptions;

namespace OSS.NBEncode.Entities
{
    public class BByteString : BObject<byte[]>
    {
        private const long maxBytesToUseInHash = 12;
        private const long hashCoeff = 37;

        public BByteString()
            : base()
        {
        }

        public BByteString(string text)
        {
            // TODO: unit test this ctor
            Value = Encoding.ASCII.GetBytes(text);
        }
        
        public override BObjectType BType
        {
            get
            {
                return BObjectType.ByteString;
            }
        }
        
        /// <summary>
        /// Create a hash value using Horner's rule on the N first bytes, and the length of the array
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            long hashValue = 0;
            long maxTableSize = (long) UInt32.MaxValue;
            long bytesToHash = Value.LongLength < 12 ? Value.LongLength : maxBytesToUseInHash;

            for (long i = 0; i < bytesToHash; i++)
            {
                hashValue = (hashCoeff * hashValue + (long)Value[i]) % maxTableSize;
            }

            // Transform hash value from UInt32 space to signed Int32 space:
            int returnValue = (int)(hashValue - (long)Int32.MaxValue) - 1;
            return returnValue;
        }
        
        /// <summary>
        /// Compare BByteString's byte arrays
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool equals = false;
            if (obj is BByteString)
            {
                var byteString = obj as BByteString;
                equals = Value.IsEqualWith(byteString.Value);
            }
            return equals;
        }
        
        /// <summary>
        /// Text string can be a maximum of 2^31 - 1 bytes long
        /// </summary>
        /// <param name="encodingToUse">Encoding to use when converting bytes to .NET string</param>
        /// <returns>The text string</returns>
        public string ConvertToText(Encoding encodingToUse)
        {
            return encodingToUse.GetString(Value, 0, Value.Length);
        }
    }
}
