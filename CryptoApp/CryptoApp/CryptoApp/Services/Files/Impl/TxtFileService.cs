using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoApp.Services.Files.Impl
{
    public class TxtFileService : ITextFileService
    {
        public string ReadRawText(byte[] fileData)
        {
            using MemoryStream memoryStream = new MemoryStream(fileData);
            using StreamReader streamReader = new StreamReader(memoryStream);

            return streamReader.ReadToEnd();
        }

        public byte[] CreateFileFromString(string rawText)
        {
            using MemoryStream memoryStream = new MemoryStream();
            using StreamWriter streamWriter = new StreamWriter(memoryStream);

            streamWriter.Write(rawText);
            streamWriter.Flush();
            return memoryStream.ToArray();
        }
    }
}
