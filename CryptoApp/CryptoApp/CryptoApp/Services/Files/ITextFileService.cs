using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoApp.Services.Files
{
    public interface ITextFileService
    {
        string ReadRawText(byte[] fileData);
        byte[] CreateFileFromString(string rawText);
    }
}
