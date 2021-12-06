using System;
using System.IO;
using System.Net;
using System.Windows;
using Parser.Exceptions;

namespace Parser.Services
{
    public class DataLoader
    {
        public DataLoader(string fileName, string fileUri)
        {
            FileName = fileName;
            FileUri = fileUri;
        }

        public string FileName { get; }
        public string FileUri { get; }


        public bool FileExists()
        {
            return File.Exists(FileName);
        }

        public void LoadFile()
        {
            try
            {
                using var client = new WebClient();
                client.DownloadFile(FileUri, FileName);
            }
            catch (Exception)
            {
                throw new LoadingException("Ошибка при скачивании файла!");
            }
        }
    }
}
