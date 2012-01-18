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
using OSS.NBEncode.Entities;
using OSS.NBEncode.Exceptions;

namespace OSS.NBEncode.Transforms
{
    public class ListTransform
    {
        private BObjectTransform objectTransform;
        
        public ListTransform(BObjectTransform objectTransform)
        {
            if (objectTransform == null)
            {
                throw new ArgumentNullException("objectTransform");
            }
            this.objectTransform = objectTransform;
        }


        public void Encode(BList input, Stream outputStream)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            outputStream.WriteByte(Definitions.ASCII_l);

            foreach (IBObject item in input.Value)
            {
                objectTransform.EncodeObject(item, outputStream);
            }

            outputStream.WriteByte(Definitions.ASCII_e);
        }


        public BList Decode(Stream inputStream)
        {
            if (inputStream == null) 
                throw new ArgumentNullException("inputStream");

            List<IBObject> listItems = new List<IBObject>();

            long lastPosition = inputStream.Position;
            int openingByte = inputStream.ReadByte();

            if (openingByte != Definitions.ASCII_l)
            {
                throw new BEncodingException("List did not start with correct character at position " + lastPosition);
            }

            IBObject nextObject = objectTransform.DecodeNext(inputStream);
            while (nextObject != null)
            {
                listItems.Add(nextObject);
                nextObject = objectTransform.DecodeNext(inputStream);
            }

            return new BList() 
            {
                Value = listItems.ToArray()
            };
        }
    }
}
