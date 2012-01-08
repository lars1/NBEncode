﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSS.NBEncode.Entities
{
    /// <summary>
    /// The base class for all BEncode data types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BObject<T>
    {
        public BObject()
        {
        }

        public T Value
        {
            get;
            set;
        }

        public Type ValueType
        {
            get
            {
                return typeof(T);
            }
        }
    }
}
