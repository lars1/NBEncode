using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSS.NBEncode.Exceptions
{
    [Serializable]
    public class BEncodingException : Exception
    {
        public BEncodingException() { }
        public BEncodingException(string message) : base(message) { }
        public BEncodingException(string message, Exception inner) : base(message, inner) { }
        protected BEncodingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
