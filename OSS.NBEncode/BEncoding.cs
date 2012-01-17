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
    }
}
