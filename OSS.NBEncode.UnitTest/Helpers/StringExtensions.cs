using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSS.NBEncode.UnitTest.Helpers
{
    public static class StringExtensions
    {
        public static byte[] GetASCIIBytes(this string first)
        {
            return Encoding.ASCII.GetBytes(first);
        }
    }
}
