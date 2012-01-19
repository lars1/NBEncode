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

namespace OSS.NBEncode.IO
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Extension method for byte arrays. Ensures bytes in both arrays have the same values.
        /// </summary>
        /// <returns>True if arrays are of equal length and has the same contents, false otherwise</returns>
        public static bool IsEqualWith(this byte[] first, byte[] second)
        {
            int differencesFound = 0;

            if (first.Length == second.Length)
            {
                for (int i = 0; i < first.Length; i++)
                {
                    if (first[i] != second[i])
                    {
                        differencesFound++;
                        break;
                    }
                }
            }
            else
            {
                differencesFound++;
            }

            return (differencesFound == 0);
        }



        /// <summary>
        /// Compare two byte arrays byte-by-byte. 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int RawCompare(this byte[] first, byte[] second)
        {
            int comparisonValue = 0;
            long longestLength = (first.LongLength > second.LongLength ? first.LongLength : second.LongLength);

            for (long i = 0; i < longestLength; i++)
            {
                if (i >= first.LongLength)
                {
                    comparisonValue = -1;            // arrays are similar but second beats first on length
                    break;
                }
                else if (i >= second.LongLength)
                {
                    comparisonValue = 1;           // arrays are similar but first beats second on length
                    break;
                }
                else if (first[i] > second[i])
                {
                    comparisonValue = 1;           // first beats second because byte[i] is greater first
                    break;
                }
                else if (first[i] < second[i])
                {
                    comparisonValue = -1;             // second beats first because byte[i] is greater in it
                    break;
                }
            }
            
            return comparisonValue;
        }
    }
}
