/**************************************************************

Copyright 2012, Lars Warholm, Norway (lars@witservices.no)

This file is part of NBEncode, a .NET library for encoding and decoding
"bencoded" data

NBEncode is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

NBEncode is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with NBEncode.  If not, see <http://www.gnu.org/licenses/>.

**************************************************************/
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
