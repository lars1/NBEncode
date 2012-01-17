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
    }
}
