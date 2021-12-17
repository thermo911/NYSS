using System.Collections.Generic;
using System.Text;

namespace CryptoApp.Services.Encryption
{
    public static class LangService
    {
        // alphabets in UPPER case!
        private static readonly Dictionary<char, int> _rusLettersIndices = new Dictionary<char, int>();
        private static readonly Dictionary<char, int> _engLettersIndices = new Dictionary<char, int>();

        private static Dictionary<Language, Dictionary<char, int>> _letterIndex;

        private static string _rusAlphabet;
        private static string _engAlphabet;

        private static Dictionary<Language, string> _alphabets = new Dictionary<Language, string>();

        static LangService()
        {
            _letterIndex = new Dictionary<Language, Dictionary<char, int>>();
            _letterIndex[Language.Eng] = _engLettersIndices;
            _letterIndex[Language.Rus] = _rusLettersIndices;

            Setup();

            _alphabets[Language.Eng] = _engAlphabet;
            _alphabets[Language.Rus] = _rusAlphabet;
        }

        public static IReadOnlyDictionary<Language, Dictionary<char, int>> LetterIndex => _letterIndex;
        public static IReadOnlyDictionary<Language, string> Alphabets => _alphabets;

        private static void Setup()
        {
            int rusCounter = 0;
            StringBuilder sb = new StringBuilder();
            for (char c = 'А'; c <= 'Я'; c++)
            {
                sb.Append(c);
                _rusLettersIndices[c] = rusCounter++;
                // append Ё in alphabet order
                if (c == 'Е') // Russian Е
                {
                    _rusLettersIndices['Ё'] = rusCounter++;
                    sb.Append("Ё");
                }
            }

            _rusAlphabet = sb.ToString();
            sb.Clear();

            int engCounter = 0;
            for (char c = 'A'; c <= 'Z'; c++)
            {
                sb.Append(c);
                _engLettersIndices[c] = engCounter++;
            }

            _engAlphabet = sb.ToString();
        }
    }
}
