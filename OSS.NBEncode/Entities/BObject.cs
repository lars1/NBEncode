using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OSS.NBEncode.Entities
{
    /// <summary>
    /// The base class for all BEncode data types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BObject<T> : IBObject
    {
        public virtual T Value
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

        public abstract BObjectType BType
        {
            get;
        }
    }
}
