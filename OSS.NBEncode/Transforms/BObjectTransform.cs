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
using OSS.NBEncode.Entities;
using System.IO;

namespace OSS.NBEncode.Transforms
{
    public class BObjectTransform
    {
        private IntegerTransform integerTransform;
        private ByteStringTransform byteStringTransform;
        private ListTransform listTransform;
        private DictionaryTransform dictionaryTransform;


        public BObjectTransform()
        {
            integerTransform = new IntegerTransform();
            byteStringTransform = new ByteStringTransform();
            listTransform = new ListTransform(this);
            dictionaryTransform = new DictionaryTransform(this);
        }

        public IBObject DecodeNext(Stream inputStream)
        {
            IBObject returnValue = null;
            long lastPosition = inputStream.Position;
            int firstByteNextObject = inputStream.ReadByte();

            if (firstByteNextObject == Definitions.ASCII_i)
            {
                inputStream.Seek(-1, SeekOrigin.Current);
                returnValue = integerTransform.Decode(inputStream);     // ex: i10e
            }
            else if (firstByteNextObject == Definitions.ASCII_l)
            {
                inputStream.Seek(-1, SeekOrigin.Current);
                returnValue = listTransform.Decode(inputStream);        // ex: li10e4:spame
            }
            else if (firstByteNextObject == Definitions.ASCII_d)
            {
                inputStream.Seek(-1, SeekOrigin.Current);
                returnValue = dictionaryTransform.Decode(inputStream);
            }
            else if (firstByteNextObject >= Definitions.ASCII_0 && firstByteNextObject <= Definitions.ASCII_9)
            {
                inputStream.Seek(-1, SeekOrigin.Current);
                returnValue = byteStringTransform.Decode(inputStream);  // ex: 4:spam
            }

            return returnValue;
        }


        public void EncodeObject(IBObject obj, Stream outputStream)
        {
            if (obj.BType == BObjectType.Integer)
            {
                integerTransform.Encode((BInteger)obj, outputStream);
            }
            else if (obj.BType == BObjectType.ByteString)
            {
                byteStringTransform.Encode((BByteString)obj, outputStream);
            }
            else if (obj.BType == BObjectType.List)
            {
                listTransform.Encode((BList)obj, outputStream);
            }
            else if (obj.BType == BObjectType.Dictionary)
            {
                dictionaryTransform.Encode((BDictionary)obj, outputStream);
            }
        }
    }
}
