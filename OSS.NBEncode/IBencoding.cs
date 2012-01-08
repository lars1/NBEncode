using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSS.NBEncode.Entities;
using System.IO;

namespace OSS.NBEncode
{
    public interface IBencoding
    {
        void Encode(BDocument input, Stream outputStream);

        BDocument Decode(Stream inputStream);


        void EncodeByteString(long inputByteLength, Stream inputStream, Stream outputStream);

        void DecodeByteString(Stream inputStream, Stream outputStream);

        
        void EncodeInteger(long input, Stream outputStream);

        BInteger DecodeInteger(Stream inputStream);





    }
}
