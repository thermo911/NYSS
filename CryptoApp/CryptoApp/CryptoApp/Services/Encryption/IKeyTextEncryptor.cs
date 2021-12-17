using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoApp.Services.Encryption
{
    public interface IKeyTextEncryptor
    {
        string Encrypt(string plainText, string key, Language lang);
        string Decrypt(string cipherText, string key, Language lang);
    }
}
