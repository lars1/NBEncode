using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSS.NBEncode.Entities;
using System.IO;
using OSS.NBEncode.Exceptions;

namespace OSS.NBEncode.Transforms
{
    /// <summary>
    /// TODO: Implement hashing of the keys, because it just default hashes the BBytesString object now
    /// </summary>
    public class DictionaryTransform
    {
        private ByteStringTransform keyTransform;
        private BObjectTransform valueTransform;

        public DictionaryTransform(ByteStringTransform keyTransform, BObjectTransform valueTransform)
        {
            if (keyTransform == null)
            {
                throw new ArgumentNullException("keyTransform");
            }
            if (valueTransform == null)
            {
                throw new ArgumentNullException("valueTransform");
            }

            this.keyTransform = keyTransform;
            this.valueTransform = valueTransform;
        }


        public void Encode(BDictionary input, Stream outputStream)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            outputStream.WriteByte(Definitions.ASCII_d);

            foreach (KeyValuePair<BByteString, IBObject> kwPair in input.Value)
            {
                keyTransform.Encode(kwPair.Key, outputStream);
                valueTransform.EncodeObject(kwPair.Value, outputStream);
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
            
            BByteString nextKey = keyTransform.Decode(inputStream);   //objectTransform.DecodeNext(inputStream);
            IBObject nextValue = valueTransform.DecodeNext(inputStream);
            
            while (nextKey != null && nextValue != null)
            {
                dict.Add(nextKey, nextValue);

                nextKey = keyTransform.Decode(inputStream);
                nextValue = valueTransform.DecodeNext(inputStream);
            }

            return new BDictionary()
            {
                Value = dict
            };
        }
    }
}
