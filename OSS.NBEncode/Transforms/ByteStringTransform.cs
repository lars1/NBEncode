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
            

            string strLengthAsString = Encoding.ASCII.GetString(strLengthBytes, 0, countBytesRead);
            long strLength = long.Parse(strLengthAsString);

            byte[] byteStringBytes = new byte[strLength];
            inputStream.Read(byteStringBytes, 0, (int)strLength);

            return new BByteString()
            {
                Value = byteStringBytes
            };
        }
    }
}
