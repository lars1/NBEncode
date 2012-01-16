using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using OSS.NBEncode.IO;
using OSS.NBEncode.Exceptions;

namespace OSS.NBEncode.Entities
{
    public class BByteString : BObject<byte[]>
    {
        public BByteString()
            : base()
        {
        }

        public BByteString(string text)
        {
            // TODO: unit test this ctor
            Value = Encoding.ASCII.GetBytes(text);
        }
        
        public override BObjectType BType
        {
            get
            {
                return BObjectType.ByteString;
            }
        }

        /// <summary>
        /// Text string can be a maximum of 2^31 - 1 bytes long
        /// </summary>
        /// <param name="encodingToUse">Encoding to use when converting bytes to .NET string</param>
        /// <returns>The text string</returns>
        public string ConvertToText(Encoding encodingToUse)
        {
            return encodingToUse.GetString(Value, 0, Value.Length);
        }
    }
}
