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
