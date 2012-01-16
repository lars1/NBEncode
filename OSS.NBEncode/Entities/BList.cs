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
            : base()
        {
        }


        //public override IBObject[] Value
        //{
        //    get
        //    {
        //        return base.Value;
        //    }
        //    set
        //    {
        //        base.Value = value;
        //    }
        //}
        
        
        public override BObjectType BType
        {
            get
            {
                return BObjectType.List;
            }
        }
    }
}
