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
