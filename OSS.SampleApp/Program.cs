using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OSS.NBEncode.Transforms;
using OSS.NBEncode.Entities;

namespace OSS.SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var inFileStream = new FileStream(args[0], FileMode.Open);

                //BEncoding
                var transform = new BObjectTransform();
                IBObject bObject = transform.DecodeNext(inFileStream);

                var textOutput = new TextOutput(4);
                textOutput.WriteObject(0, bObject);
            }
            else
            {
                Console.WriteLine("\nSyntax is:\nOSS.SampleApp.exe <torrent file path>");
            }
        }
    }
}
