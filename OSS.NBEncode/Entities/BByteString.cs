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


        
        public override BObjectType BType
        {
            get
            {
                return BObjectType.ByteString;
            }
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
        



        //public static virtual void DecodeStreaming(Stream inputStream, Stream outputStream)
        //{
        //    if (inputStream == null)
        //        throw new ArgumentNullException("inputStream");
        //    if (outputStream == null)
        //        throw new ArgumentNullException("outputStream");


        //    byte[] strLengthBytes = new byte[Definitions.MaxIntegerDigits];

        //    int readByte = 1;
        //    int countBytesRead = 0;

        //    while (readByte >= 0)
        //    {
        //        readByte = inputStream.ReadByte();

        //        if (readByte == Definitions.ASCII_colon)
        //        {
        //            break;                      // found delimiter between header (length) and contents of the byte string, so start copying
        //        }
        //        if (readByte < 0)
        //        {
        //            throw new BEncodingException("Badly formatted byte string, no colon-character found");
        //        }
        //        if (countBytesRead >= Definitions.MaxIntegerDigits)
        //        {
        //            throw new BEncodingException("Byte string length is a larger number than what is supported");
        //        }

        //        countBytesRead++;
        //        strLengthBytes[countBytesRead - 1] = (byte)readByte;
        //    }


        //    string strLengthAsString = Encoding.ASCII.GetString(strLengthBytes, 0, countBytesRead);
        //    long strLength = long.Parse(strLengthAsString);

        //    inputStream.WriteBytesTo(outputStream, strLength);
        //}




        //public static virtual void EncodeStreaming(long inputStreamLength, Stream inputStream, Stream outputStream)
        //{
        //    if (inputStream == null)
        //        throw new ArgumentNullException("inputStream");
        //    if (outputStream == null)
        //        throw new ArgumentNullException("outputStream");


        //    // Write byte string "header":
        //    byte[] strLengthBytes = Encoding.ASCII.GetBytes(inputStreamLength.ToString());

        //    outputStream.Write(strLengthBytes, 0, strLengthBytes.Length);
        //    outputStream.WriteByte(Definitions.ASCII_colon);

        //    // Write byte string itself:
        //    inputStream.WriteBytesTo(outputStream, inputStreamLength);
        //}
    }
}
