using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSS.NBEncode.IO
{
    public static class ArrayExtensions
    {
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
    }
}
