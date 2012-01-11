using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OSS.NBEncode.Entities;
using OSS.NBEncode.IO;
using OSS.NBEncode.Exceptions;

namespace OSS.NBEncode
{
    public class BEncoding : IBencoding
    {
        public BEncoding()
        { }


        public void Encode(BDocument input, Stream outputStream)
        {
            throw new NotImplementedException();
        }


        public Entities.BDocument Decode(Stream inputStream)
        {
            throw new NotImplementedException();
        }


        //public void EncodeByteString(long inputByteLength, Stream inputStream, Stream outputStream)
        //{
        //    BByteString.EncodeStreaming(inputByteLength, inputStream, outputStream);
        //}


        //public void DecodeByteString(Stream inputStream, Stream outputStream)
        //{
        //    BByteString.DecodeStreaming(inputStream, outputStream);
        //}


        //public void EncodeInteger(long input, Stream outputStream)
        //{
        //    BInteger bInt = new BInteger(input);
        //    bInt.Encode(outputStream);

            //outputStream.WriteByte(ASCII_i);

            //byte[] numberStringBytes = Encoding.ASCII.GetBytes(input.ToString());
            //outputStream.Write(numberStringBytes, 0, numberStringBytes.Length);

            //outputStream.WriteByte(ASCII_e);

            
        //}


        //public Entities.BInteger DecodeInteger(Stream inputStream)
        //{
        //    return (BInteger) BInteger.Decode(inputStream);

            //byte[] characters = new byte[MaxIntegerDigits];
            //int characterCount = 0;

            //inputStream.ReadByte();                                 // skip 'i'

            //int readByte = inputStream.ReadByte();

            //while (readByte > 0 && readByte != ASCII_e && characterCount <= MaxIntegerDigits)
            //{
            //    // Validation: only allow minus or digit as byte zero and otherwise only digits:
            //    if ((readByte == ASCII_minus && characterCount == 0) ||
            //        (readByte >= ASCII_0 && readByte <= ASCII_9))
            //    {
            //        characters[characterCount] = (byte)readByte;
            //        characterCount++;
            //        readByte = inputStream.ReadByte();
            //    }
            //    else
            //    {
            //        throw new Exception("Byte " + characterCount + " of Integer is invalid, at position " + inputStream.Position);
            //    }
            //}

            //// Validate the input:
            //if ((characterCount < 1) ||             // no characters
            //    (readByte != ASCII_e) ||            // OR did not end in 'e'
            //    (characterCount >= 2 && characters[0] == ASCII_minus && characters[1] == ASCII_0))      // OR "-0" detected
            //{
            //    throw new Exception("Malformed Integer at position " + inputStream.Position);
            //}
            
            //BInteger returnValue = new BInteger();
            //returnValue.Value = long.Parse(Encoding.ASCII.GetString(characters, 0, characterCount));

            //return returnValue;
        //}



        //public void EncodeList(BList input, Stream outputStream)
        //{
        //    input.Encode(outputStream);
        //}


        //public BList DecodeList(Stream inputStream)
        //{
        //    return (BList)BList.Decode(inputStream);
        //}

    }
}
