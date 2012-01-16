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
