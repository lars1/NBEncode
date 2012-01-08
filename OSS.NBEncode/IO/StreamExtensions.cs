using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OSS.NBEncode.IO
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Copies N bytes from this stream to another stream. Updates both streams Positions.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="destination"></param>
        public static void WriteBytesTo(this Stream from, Stream destination, long amountOfBytesToCopy)
        {
            long bufferSize = 65536;

            BufferedStream bufferedFrom = new BufferedStream(from);
            BufferedStream bufferedDestination = new BufferedStream(destination);

            byte[] readBuffer = new byte[bufferSize];
            int bytesRead = 0;

            while (amountOfBytesToCopy > 0)
            {
                //long amountToRead = Math.Min(amountOfBytesToCopy, bufferSize);
                //from.Read(
                //byte[] bytesRead = reader.ReadBytes((int)amountToRead);

                bytesRead = bufferedFrom.Read(readBuffer, 0, (int)bufferSize);

                if (bytesRead > 0)
                {
                    bufferedDestination.Write(readBuffer, 0, bytesRead);
                }
                else if (bytesRead == 0)
                {
                    break;
                }
                
                amountOfBytesToCopy -= bytesRead;
            }

            bufferedDestination.Flush();

            if (amountOfBytesToCopy > 0)
            {
                throw new Exception("Too few bytes to copy in the source stream");
            }
        }
    }
}
