using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OSS.NBEncode.Entities
{
    public class BInteger : BObject<long>
    {
        public BInteger(long value)
        {
            Value = value;
        }


        public override BObjectType BType
        {
            get
            {
                return BObjectType.Integer;
            }
        }
    }
}
