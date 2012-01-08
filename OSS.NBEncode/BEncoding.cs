using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OSS.NBEncode.Entities;
using OSS.NBEncode.IO;

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
        
        // bencode bytestring consts:
        const byte ASCII_parens = 58;    // :

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
            // Write byte string "header":
            byte[] strLengthBytes = Encoding.ASCII.GetBytes(inputByteLength.ToString());

            outputStream.Write(strLengthBytes, 0, strLengthBytes.Length);
            outputStream.WriteByte(ASCII_parens);

            // Write byte string itself:
            inputStream.WriteBytesTo(outputStream, inputByteLength);
        }

        public void DecodeByteString(Stream inputStream, Stream outputStream)
        {
            throw new NotImplementedException();
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
            const int maxNumberOfCharacters = 19;                   // maximum value has 19 characters
            
            byte[] characters = new byte[maxNumberOfCharacters];           
            int characterCount = 0;

            inputStream.ReadByte();                                 // skip 'i'
            
            int readByte = inputStream.ReadByte();

            while (readByte > 0 && readByte != ASCII_e && characterCount <= maxNumberOfCharacters)
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
