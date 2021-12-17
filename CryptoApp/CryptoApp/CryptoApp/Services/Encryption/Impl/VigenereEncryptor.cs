using System.Linq;
using System.Text;
using CryptoApp.Services.Encryption.Tools;

namespace CryptoApp.Services.Encryption.Impl
{
    public class VigenereEncryptor : IKeyTextEncryptor
    { 
        public string Encrypt(string plainText, string key, Language lang)
        {
            int[] offsets = GetOffsets(key, lang, Mode.Encryption);
            return ProcessString(plainText, offsets, lang);
        }

        public string Decrypt(string cipherText, string key, Language lang)
        {
            int[] offsets = GetOffsets(key, lang, Mode.Decryption);
            return ProcessString(cipherText, offsets, lang);
        }

        private string ProcessString(string s, int[] offsets, Language lang)
        {
            string alphabet = LangService.Alphabets[lang];
            StringBuilder resultString = new StringBuilder();

            int offsetsPos = 0;

            foreach (char c in s)
            {
                char oldChar = char.ToUpper(c);
                bool inLowerCase = char.IsLower(c);

                if (!LangService.LetterIndex[lang].TryGetValue(oldChar, out int oldIndex))
                {
                    resultString.Append(c);
                    continue;
                }

                int offset = offsets[offsetsPos];
                int newIndex = (alphabet.Length + oldIndex + offset) % alphabet.Length;

                char newChar = alphabet[newIndex];
                if (inLowerCase)
                    newChar = char.ToLower(newChar);

                resultString.Append(newChar);
                offsetsPos = (offsetsPos + 1) % offsets.Length;
            }

            return resultString.ToString();
        }

        private int[] GetOffsets(string key, Language lang, Mode mode)
        {
            return key.ToUpper().Select(c
                => (mode == Mode.Decryption ? -1 : 1) * LangService.LetterIndex[lang][c])
                .ToArray();
        }
    }
}
