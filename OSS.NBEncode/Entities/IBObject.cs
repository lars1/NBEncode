using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OSS.NBEncode.Entities
{
    public interface IBObject
    {
        BObjectType BType { get; }
    }
}
