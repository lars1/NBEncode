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

namespace OSS.NBEncode
{
    public static class Definitions
    {
        // bencode integer constants
        public static byte ASCII_e = 101;
        public static byte ASCII_i = 105;
        public static byte ASCII_minus = 45;
        public static byte ASCII_0 = 48;
        public static byte ASCII_9 = 57;

        public static int MaxIntegerDigits = 19;

        // bencode bytestring consts:
        public static byte ASCII_colon = 58;    // :

        // bencode list consts
        public static byte ASCII_l = 108;

        // bencode dictionary consts
        public static byte ASCII_d = 100;

    }
}
