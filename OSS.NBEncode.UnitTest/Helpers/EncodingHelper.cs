using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSS.NBEncode.UnitTest.Helpers
{
    public class EncodingHelper
    {
        /// <summary>
        /// Get the ascii value of a single character
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static byte GetASCIIValue(char c)
        {
            // Ugly rundabout way of finding it, but nevermind since this is test code
            return Encoding.ASCII.GetBytes(new string(c, 1))[0];
        }
    }

}
