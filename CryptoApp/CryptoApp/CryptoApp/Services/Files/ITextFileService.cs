namespace CryptoApp.Services.Files
{
    public interface ITextFileService
    {
        string ReadRawText(byte[] fileData);
        byte[] CreateFileFromString(string rawText);
    }
}
