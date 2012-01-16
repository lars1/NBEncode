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
        private IntegerTransform integerTransform;
        private ByteStringTransform byteStringTransform;
        private ListTransform listTransform;
        private DictionaryTransform dictionaryTransform;


        public BObjectTransform()
        {
            integerTransform = new IntegerTransform();
            byteStringTransform = new ByteStringTransform();
            listTransform = new ListTransform(this);
            dictionaryTransform = new DictionaryTransform(this);
        }

        public IBObject DecodeNext(Stream inputStream)
        {
            IBObject returnValue = null;
            long lastPosition = inputStream.Position;
            int firstByteNextObject = inputStream.ReadByte();

            if (firstByteNextObject == Definitions.ASCII_i)
            {
                inputStream.Seek(-1, SeekOrigin.Current);
                returnValue = integerTransform.Decode(inputStream);     //i10e
            }
            else if (firstByteNextObject == Definitions.ASCII_l)
            {
                inputStream.Seek(-1, SeekOrigin.Current);
                returnValue = listTransform.Decode(inputStream);        //li10e4:spame
            }
            else if (firstByteNextObject == Definitions.ASCII_d)
            {
                inputStream.Seek(-1, SeekOrigin.Current);
                returnValue = dictionaryTransform.Decode(inputStream);
            }
            else if (firstByteNextObject > Definitions.ASCII_0 && firstByteNextObject < Definitions.ASCII_9)
            {
                inputStream.Seek(-1, SeekOrigin.Current);
                returnValue = byteStringTransform.Decode(inputStream);  // 4:spam
            }

            return returnValue;
        }


        public void EncodeObject(IBObject obj, Stream outputStream)
        {
            if (obj.BType == BObjectType.Integer)
            {
                integerTransform.Encode((BInteger)obj, outputStream);
            }
            else if (obj.BType == BObjectType.ByteString)
            {
                byteStringTransform.Encode((BByteString)obj, outputStream);
            }
            else if (obj.BType == BObjectType.List)
            {
                listTransform.Encode((BList)obj, outputStream);
            }
        }
    }
}
