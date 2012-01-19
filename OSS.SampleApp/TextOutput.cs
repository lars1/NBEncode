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
using OSS.NBEncode.Entities;

namespace OSS.SampleApp
{
    public class TextOutput
    {
        private int spacesPerIndentLevel;


        public TextOutput(int spacesPerIndentLevel)
        {
            this.spacesPerIndentLevel = spacesPerIndentLevel;
        }


        public void WriteObject(int indentLevel, IBObject obj)
        {
            switch(obj.BType)
            {
                case BObjectType.Integer:
                    WriteInteger(indentLevel, (BInteger)obj);
                    break;
                case BObjectType.ByteString:
                    WriteByteString(indentLevel, (BByteString)obj);
                    break;
                case BObjectType.List:
                    WriteList(indentLevel, (BList)obj);
                    break;
                case BObjectType.Dictionary:
                    WriteDictionary(indentLevel, (BDictionary)obj);
                    break;         
            }
        }

        
        private void WriteInteger(int indentLevel, BInteger integer)
        {
            Console.WriteLine("{0}{1}", GetIndentSpaces(indentLevel), integer.Value.ToString());
        }


        private void WriteByteString(int indentLevel, BByteString byteString)
        {
            Console.WriteLine("{0}{1}", GetIndentSpaces(indentLevel), byteString.ConvertToText(Encoding.ASCII));
        }


        private void WriteList(int indentLevel, BList list)
        {
            Console.WriteLine("{0}List:", GetIndentSpaces(indentLevel));
            foreach (IBObject obj in list.Value)
            {
                WriteObject(indentLevel + 1, obj);
            }
        }


        private void WriteDictionary(int indentLevel, BDictionary dict)
        {
            Console.WriteLine("{0}Dict:", GetIndentSpaces(indentLevel));
            foreach (var kvPair in dict.Value)
            {
                WriteByteString(indentLevel + 1, kvPair.Key);
                WriteObject(indentLevel + 2, kvPair.Value);
            }
        }
        

        private string GetIndentSpaces(int indentLevel)
        {
            return new string(' ', indentLevel * spacesPerIndentLevel);
        }
    }
}
