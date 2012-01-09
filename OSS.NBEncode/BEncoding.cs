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
        // bencode integer constants
        const byte ASCII_e = 101;
        const byte ASCII_i = 105;
        const byte ASCII_minus = 45;
        const byte ASCII_0 = 48;
        const byte ASCII_9 = 57;

        const int MaxIntegerDigits = 19;

        // bencode bytestring consts:
        const byte ASCII_colon = 58;    // :






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


        public void EncodeByteString(long inputByteLength, Stream inputStream, Stream outputStream)
        {
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            // Write byte string "header":
            byte[] strLengthBytes = Encoding.ASCII.GetBytes(inputByteLength.ToString());

            outputStream.Write(strLengthBytes, 0, strLengthBytes.Length);
            outputStream.WriteByte(ASCII_colon);

            // Write byte string itself:
            inputStream.WriteBytesTo(outputStream, inputByteLength);
        }


        public void DecodeByteString(Stream inputStream, Stream outputStream)
        {
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");


            byte[] strLengthBytes = new byte[MaxIntegerDigits];

            int readByte = 1;
            int countBytesRead = 0;

            while (readByte >= 0)
            {
                readByte = inputStream.ReadByte();

                if (readByte == ASCII_colon)
                {
                    break;                      // found delimiter between header (length) and contents of the byte string, so start copying
                }
                if (readByte < 0)
                {
                    throw new BEncodingException("Badly formatted byte string, no colon-character found");
                }
                if (countBytesRead >= MaxIntegerDigits)
                {
                    throw new BEncodingException("Byte string length is a larger number than what is supported");
                }

                countBytesRead++;
                strLengthBytes[countBytesRead - 1] = (byte)readByte;                
            }


            string strLengthAsString = Encoding.ASCII.GetString(strLengthBytes, 0, countBytesRead);
            long strLength = long.Parse(strLengthAsString);

            inputStream.WriteBytesTo(outputStream, strLength);
        }


        public void EncodeInteger(long input, Stream outputStream)
        {
            outputStream.WriteByte(ASCII_i);

            byte[] numberStringBytes = Encoding.ASCII.GetBytes(input.ToString());
            outputStream.Write(numberStringBytes, 0, numberStringBytes.Length);

            outputStream.WriteByte(ASCII_e);
        }
        

        public Entities.BInteger DecodeInteger(Stream inputStream)
        {
            byte[] characters = new byte[MaxIntegerDigits];           
            int characterCount = 0;

            inputStream.ReadByte();                                 // skip 'i'
            
            int readByte = inputStream.ReadByte();

            while (readByte > 0 && readByte != ASCII_e && characterCount <= MaxIntegerDigits)
            {
                // Validation: only allow minus or digit as byte zero and otherwise only digits:
                if ((readByte == ASCII_minus && characterCount == 0) ||
                    (readByte >= ASCII_0 && readByte <= ASCII_9))
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
            if ((characterCount < 1) ||             // no characters
                (readByte != ASCII_e) ||            // OR did not end in 'e'
                (characterCount >= 2 && characters[0] == ASCII_minus && characters[1] == ASCII_0))      // OR "-0" detected
            {
                throw new Exception("Malformed Integer at position " + inputStream.Position);
            }
            

            BInteger returnValue = new BInteger();
            returnValue.Value = long.Parse(Encoding.ASCII.GetString(characters, 0, characterCount));

            return returnValue;
        }
    }
}
