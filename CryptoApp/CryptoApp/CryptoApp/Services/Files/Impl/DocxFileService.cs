using System.IO;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CryptoApp.Services.Files.Impl
{
    public class DocxFileService : ITextFileService
    {
        public string ReadRawText(byte[] fileData)
        {
            using MemoryStream memoryStream = new MemoryStream(fileData);

            using WordprocessingDocument document = WordprocessingDocument.Open(memoryStream, false);
            MainDocumentPart mainPart = document.MainDocumentPart;

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var child in mainPart.Document.Body.ChildElements)
            {
                stringBuilder.Append(child.InnerText);
                stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }

        public byte[] CreateFileFromString(string rawText)
        {
            using MemoryStream mem = new MemoryStream();
            // Create Document
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document, true))
            {
                // Add a main document part. 
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Create the document structure and add some text.
                mainPart.Document =
                    new Document(
                        new Body(
                            new Paragraph(
                                new Run(
                                    new Text(rawText)))));
            }

            return mem.ToArray();
        }
    }
}
