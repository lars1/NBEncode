using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSS.NBEncode.Entities
{
    public class BDictionary : BObject<Dictionary<BByteString, IBObject>>
    {
        public BDictionary()
            : base()
        {
            Value = new Dictionary<BByteString, IBObject>();
        }

        public override BObjectType BType
        {
            get 
            {
                return BObjectType.Dictionary;
            }
        }
    }
}
