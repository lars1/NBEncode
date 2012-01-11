using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSS.NBEncode.Entities;
using System.IO;

namespace OSS.NBEncode.Transforms
{
    public class BObjectTransform
    {
        public BObjectTransform()
        {
            IntegerTransform = new IntegerTransform();
            ByteStringTransform = new ByteStringTransform();
            ListTransform = new ListTransform(this);
        }


        public IntegerTransform IntegerTransform { get; private set; }

        public ByteStringTransform ByteStringTransform { get; private set; }

        public ListTransform ListTransform { get; private set; }



        public IBObject DecodeNext(Stream inputStream)
        {
            IBObject returnValue = null;
            long lastPosition = inputStream.Position;
            int firstByteNextObject = inputStream.ReadByte();

            if (firstByteNextObject == Definitions.ASCII_i)
            {
                inputStream.Seek(-1, SeekOrigin.Current);
                returnValue = IntegerTransform.Decode(inputStream);     //i10e
            }
            else if (firstByteNextObject == Definitions.ASCII_l)
            {
                inputStream.Seek(-1, SeekOrigin.Current);
                returnValue = ListTransform.Decode(inputStream);        //li10e4:spame
            }
            else if (firstByteNextObject > Definitions.ASCII_0 && firstByteNextObject < Definitions.ASCII_9)
            {
                inputStream.Seek(-1, SeekOrigin.Current);
                returnValue = ByteStringTransform.Decode(inputStream);  // 4:spam
            }

            return returnValue;
        }


        public void EncodeObject(IBObject obj, Stream outputStream)
        {
            if (obj.BType == BObjectType.Integer)
            {
                IntegerTransform.Encode((BInteger)obj, outputStream);
            }
            else if (obj.BType == BObjectType.ByteString)
            {
                ByteStringTransform.Encode((BByteString)obj, outputStream);
            }
            else if (obj.BType == BObjectType.List)
            {
                ListTransform.Encode((BList)obj, outputStream);
            }
        }
    }
}
