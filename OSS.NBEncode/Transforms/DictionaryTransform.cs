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
using OSS.NBEncode.Entities;
using OSS.NBEncode.Exceptions;
using OSS.NBEncode.IO;

namespace OSS.NBEncode.Transforms
{
    public class DictionaryTransform
    {
        private BObjectTransform objectTransform;

        public DictionaryTransform(BObjectTransform objectTransform)
        {
            if (objectTransform == null)
            {
                throw new ArgumentNullException("objectTransform");
            }

            this.objectTransform = objectTransform;
        }


        /// <summary>
        /// Encodes a BEncode dictionary as bytes. Key-value pairs are output in sorted order based on raw byte sorting of the keys.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="outputStream"></param>
        public void Encode(BDictionary input, Stream outputStream)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            outputStream.WriteByte(Definitions.ASCII_d);

            var sortedKVPairs = input.Value.OrderBy(
                                    (kvPair) => { return kvPair.Key; }, 
                                    new BByteStringComparer());

            foreach (KeyValuePair<BByteString, IBObject> kwPair in sortedKVPairs)
            {
                objectTransform.EncodeObject(kwPair.Key, outputStream);
                objectTransform.EncodeObject(kwPair.Value, outputStream);
            }

            outputStream.WriteByte(Definitions.ASCII_e);
        }


        public BDictionary Decode(Stream inputStream)
        {
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            Dictionary<BByteString, IBObject> dict = new Dictionary<BByteString, IBObject>();

            long lastPosition = inputStream.Position;
            int openingByte = inputStream.ReadByte();

            if (openingByte != Definitions.ASCII_d)
            {
                throw new BEncodingException("Dictionary did not start with correct character at position " + lastPosition);
            }

            IBObject nextKey = objectTransform.DecodeNext(inputStream);
            IBObject nextValue = objectTransform.DecodeNext(inputStream);
            
            while (nextKey != null && nextValue != null)
            {
                if (nextKey.BType != BObjectType.ByteString)
                {
                    throw new BEncodingException(string.Format("Illegal type {0} detected for BDictionary key at position {1}", nextKey.BType.ToString(), lastPosition));
                }

                dict.Add((BByteString)nextKey, nextValue);

                lastPosition = inputStream.Position;
                nextKey = objectTransform.DecodeNext(inputStream);
                nextValue = objectTransform.DecodeNext(inputStream);
            }

            return new BDictionary()
            {
                Value = dict
            };
        }
    }
}
