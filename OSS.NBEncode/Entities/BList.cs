using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OSS.NBEncode.Exceptions;

namespace OSS.NBEncode.Entities
{
    public class BList : BObject<IBObject[]>
    {
        public BList()
        {
        }


        public override IBObject[] Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
            }
        }
        
        
        public override BObjectType BType
        {
            get
            {
                return BObjectType.List;
            }
        }


        //public static override BObject<IBObject[]> Decode(Stream inputStream)
        //{
        //    List<IBObject> listItems = new List<IBObject>();

        //    BufferedStream bufferedStream = new BufferedStream(inputStream);
        //    long lastPosition = bufferedStream.Position;
        //    int openingByte = bufferedStream.ReadByte();

        //    if (openingByte != Definitions.ASCII_l)
        //    {
        //        throw new BEncodingException("List did not start with correct character at position " + lastPosition);
        //    }

        //    while (true)
        //    {

        //        lastPosition = bufferedStream.Position;
        //        int firstByteNextObject = bufferedStream.ReadByte();

        //        if (firstByteNextObject == Definitions.ASCII_i)
        //        {
        //            bufferedStream.Seek(-1, SeekOrigin.Current);
        //            listItems.Add(BInteger.Decode(inputStream));            //i10e
        //        }
        //        else if (firstByteNextObject == Definitions.ASCII_l)
        //        {
        //            bufferedStream.Seek(-1, SeekOrigin.Current);
        //            listItems.Add(BList.Decode(inputStream));               //li10e4:spame
        //        }
        //        else if (firstByteNextObject > Definitions.ASCII_0 && firstByteNextObject < Definitions.ASCII_9)
        //        {
        //            bufferedStream.Seek(-1, SeekOrigin.Current);
        //            listItems.Add(BByteString.Decode(inputStream));         // 4:spam
        //        }
        //    }


        //}

        //public override void Encode(Stream outputStream)
        //{
        //    outputStream.WriteByte(Definitions.ASCII_l);

        //    foreach (IBObject item in Value)
        //    {
        //        item.Encode(outputStream);
        //    }

        //    outputStream.WriteByte(Definitions.ASCII_e);
        //}
    }
}
