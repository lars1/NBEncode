﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSS.NBEncode.Entities
{
    public class BInteger : BObject<long>
    {
        public override BObjectType BType
        {
            get
            {
                return BObjectType.Integer;
            }
        }
    }
}
