using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using CryptoApp.Services.Encryption;
using CryptoApp.Services.Files;
using CryptoApp.Services.Files.Impl;
using Microsoft.AspNetCore.Http;

namespace CryptoApp.Controllers
{
    [Controller]
    [Route("crypto")]
    public class EncryptController : Controller
    {
        private IKeyTextEncryptor _encryptor;
        private TxtFileService _txtFileService;
        private DocxFileService _docxFileService;

        public EncryptController(
            IKeyTextEncryptor encryptor, 
            TxtFileService txtFileService, DocxFileService docxFileService)
        {
            _encryptor = encryptor;
            _txtFileService = txtFileService;
            _docxFileService = docxFileService;
        }

        [HttpGet("load_file")]
        public IActionResult LoadFileForm()
        {
            return View("LoadFileForm");
        }

        [HttpPost("encrypt")]
        public IActionResult Encrypt(IFormFile file, string cipherKey, string lang)
        {
            Language language = GetLanguage(lang);
            ITextFileService service = GetSuitableFileService(file);

            string text = GetFileRawText(file, service);
            string encryptedText = _encryptor.Encrypt(text, cipherKey, language);
            byte[] responseFileData = service.CreateFileFromString(encryptedText);

            string fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));

            return File(responseFileData, "text/plain", $"{Guid.NewGuid()}{fileFormat}");
        }

        [HttpPost("decrypt")]
        public IActionResult Decrypt(IFormFile file, string cipherKey, string lang)
        {
            Language language = GetLanguage(lang);
            ITextFileService service = GetSuitableFileService(file);

            string text = GetFileRawText(file, service);
            string decryptedText = _encryptor.Decrypt(text, cipherKey, language);
            byte[] responseFileData = service.CreateFileFromString(decryptedText);

            string fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));

            return File(responseFileData, "text/plain", $"{Guid.NewGuid()}{fileFormat}");
        }

        [NonAction]
        private string GetFileRawText(IFormFile file, ITextFileService service)
        {
            using Stream fileStream = file.OpenReadStream();
            using MemoryStream memoryStream = new MemoryStream();

            fileStream.CopyTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return service.ReadRawText(memoryStream.ToArray());
        }

        [NonAction]
        private Language GetLanguage(string lang)
        {
            if (lang == "RU")
                return Language.Rus;
            return Language.Eng;
        }

        [NonAction]
        private ITextFileService GetSuitableFileService(IFormFile file)
        {
            if (file.FileName.EndsWith(".txt"))
                return _txtFileService;

            if (file.FileName.EndsWith(".docx"))
                return _docxFileService;

            throw new ArgumentException($"type of file {file.FileName} not supported");
        }
    }
}
