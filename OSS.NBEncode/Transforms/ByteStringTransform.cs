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
    public class ByteStringTransform
    {
        public void Encode(BByteString input, Stream outputStream)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");
            
            // Write byte string "header":
            byte[] strLengthBytes = Encoding.ASCII.GetBytes(input.Value.Length.ToString());

            outputStream.Write(strLengthBytes, 0, strLengthBytes.Length);
            outputStream.WriteByte(Definitions.ASCII_colon);

            outputStream.Write(input.Value, 0, input.Value.Length);
        }


        public BByteString Decode(Stream inputStream)
        {
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");


            byte[] strLengthBytes = new byte[Definitions.MaxIntegerDigits];

            int readByte = 1;
            int countBytesRead = 0;

            while (readByte >= 0)
            {
                readByte = inputStream.ReadByte();

                if (readByte == Definitions.ASCII_colon)
                {
                    break;                      // found delimiter between header (length) and contents of the byte string, so start copying
                }
                if (readByte < 0)
                {
                    throw new BEncodingException("Badly formatted byte string, no colon-character found");
                }
                if (countBytesRead >= Definitions.MaxIntegerDigits)
                {
                    throw new BEncodingException("Byte string length is a larger number than what is supported");
                }

                countBytesRead++;
                strLengthBytes[countBytesRead - 1] = (byte)readByte;
            }
            

            string byteAmountAsString = Encoding.ASCII.GetString(strLengthBytes, 0, countBytesRead);
            long byteAmount = long.Parse(byteAmountAsString);

            byte[] byteStringBytes = new byte[byteAmount];
            int numberOfBytesRead = inputStream.Read(byteStringBytes, 0, (int)byteAmount);

            if (numberOfBytesRead != byteAmount)
            {
                throw new BEncodingException(string.Format("Expected string to be {0} bytes long, could only read {1} bytes", byteAmount, numberOfBytesRead));
            }

            return new BByteString()
            {
                Value = byteStringBytes
            };
        }
    }
}
