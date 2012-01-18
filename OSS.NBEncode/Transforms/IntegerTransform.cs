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

namespace OSS.NBEncode.Transforms
{
    public class IntegerTransform
    {
        public void Encode(BInteger input, Stream outputStream)
        {
            outputStream.WriteByte(Definitions.ASCII_i);

            byte[] numberStringBytes = Encoding.ASCII.GetBytes(input.Value.ToString());
            outputStream.Write(numberStringBytes, 0, numberStringBytes.Length);

            outputStream.WriteByte(Definitions.ASCII_e);
        }


        public BInteger Decode(Stream inputStream)
        {
            byte[] characters = new byte[Definitions.MaxIntegerDigits];
            int characterCount = 0;

            inputStream.ReadByte();                             // skip 'i'

            int readByte = inputStream.ReadByte();

            while (readByte > 0 && readByte != Definitions.ASCII_e && characterCount <= Definitions.MaxIntegerDigits)
            {
                // Validation: only allow minus or digit as byte zero and otherwise only digits:
                if ((readByte == Definitions.ASCII_minus && characterCount == 0) ||
                    (readByte >= Definitions.ASCII_0 && readByte <= Definitions.ASCII_9))
                {
                    characters[characterCount] = (byte)readByte;
                    characterCount++;
                    readByte = inputStream.ReadByte();
                }
                else
                {
                    throw new Exception("Byte " + characterCount + " of Integer is invalid, at position " + inputStream.Position);
                }
            }

            // Validate the input:
            if ((characterCount < 1) ||                         // no characters
                (readByte != Definitions.ASCII_e) ||            // OR did not end in 'e'
                (characterCount >= 2 && characters[0] == Definitions.ASCII_minus && characters[1] == Definitions.ASCII_0))      // OR "-0" detected
            {
                throw new Exception("Malformed Integer at position " + inputStream.Position);
            }


            long value = long.Parse(Encoding.ASCII.GetString(characters, 0, characterCount));
            return new BInteger(value);
        }
    }
}
