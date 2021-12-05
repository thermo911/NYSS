using System;
using System.IO;
using System.Net;
using System.Windows;

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
                string message = "Проверьте подключение к сети Интернет";
                string caption = "Ошибка загрузки";
                MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
