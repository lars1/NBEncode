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
        private BObjectTransform objectTransform;

        public DictionaryTransform(BObjectTransform objectTransform)
        {
            if (objectTransform == null)
            {
                throw new ArgumentNullException("objectTransform");
            }

            this.objectTransform = objectTransform;
        }

        // TODO: BEncode dicts should be output with keys in alphabetical order (or lexographical)
        public void Encode(BDictionary input, Stream outputStream)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            outputStream.WriteByte(Definitions.ASCII_d);

            foreach (KeyValuePair<BByteString, IBObject> kwPair in input.Value)
            {
                objectTransform.EncodeObject(kwPair.Key, outputStream);
                objectTransform.EncodeObject(kwPair.Value, outputStream);
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

            IBObject nextKey = objectTransform.DecodeNext(inputStream);   //objectTransform.DecodeNext(inputStream);
            IBObject nextValue = objectTransform.DecodeNext(inputStream);
            
            while (nextKey != null && nextValue != null)
            {
                if (nextKey.BType != BObjectType.ByteString)
                {
                    throw new BEncodingException(string.Format("Illegal type {0} detected for BDictionary key at position {1}", nextKey.BType.ToString(), lastPosition));
                }

                dict.Add((BByteString)nextKey, nextValue);

                lastPosition = inputStream.Position;
                nextKey = objectTransform.DecodeNext(inputStream);
                nextValue = objectTransform.DecodeNext(inputStream);
            }

            return new BDictionary()
            {
                Value = dict
            };
        }
    }
}
