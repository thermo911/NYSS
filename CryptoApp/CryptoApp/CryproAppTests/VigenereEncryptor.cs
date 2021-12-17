using System;
using CryptoApp.Services.Encryption;
using CryptoApp.Services.Encryption.Impl;
using NUnit.Framework;

namespace CryproAppTests
{
    public class VigenereEncryptorTests
    {
        private VigenereEncryptor _encryptor = new VigenereEncryptor();

        [Test]
        public void Encrypt_RU_EncryptedCorrectly()
        {
            string plain = "поздравл€ю";
            string cipher = "бщцфаирщри";
            string key = "скорпион";

            Assert.AreEqual(cipher, _encryptor.Encrypt(plain, key, Language.Rus));
        }

        [Test]
        public void Encrypt_EN_EncryptedCorrectly()
        {
            string plain = "congratulations";
            string cipher = "uqbxgihhdchzdvg";
            string key = "scorpion";

            Assert.AreEqual(cipher, _encryptor.Encrypt(plain, key, Language.Eng));
        }

        [Test]
        public void Encrypt_RU_PreserveCase()
        {
            string plain = "позƒ–јвл€ю";
            string cipher = "бщц‘ј»рщри";
            string key = "скорпион";

            Assert.AreEqual(cipher, _encryptor.Encrypt(plain, key, Language.Rus));
        }

        [Test]
        public void Encrypt_EN_PreserveCase()
        {
            string plain = "CongratulationS";
            string cipher = "UqbxgihhdchzdvG";
            string key = "scorpion";

            Assert.AreEqual(cipher, _encryptor.Encrypt(plain, key, Language.Eng));
        }

        [Test]
        public void Encrypt_RU_IgnoreNotRussianLetters()
        {
            string plain = "ѕоз драв, л€ю! qwerty";
            string cipher = "Ѕщц фаир, щри! qwerty";
            string key = "скорпион";

            Assert.AreEqual(cipher, _encryptor.Encrypt(plain, key, Language.Rus));
        }

        [Test]
        public void Encrypt_EN_IgnoreNotEnglishLetters()
        {
            string plain = "Con? grat ul, atio: nS! йцуке";
            string cipher = "Uqb? xgih hd, chzd: vG! йцуке";
            string key = "scorpion";

            Assert.AreEqual(cipher, _encryptor.Encrypt(plain, key, Language.Eng));
        }

        [Test]
        public void Decrypt_RU_DecryptedCorrectly()
        {
            string plain = "ѕоз драв, л€ю! qwerty";
            string cipher = "Ѕщц фаир, щри! qwerty";
            string key = "скорпион";

            Assert.AreEqual(plain, _encryptor.Decrypt(cipher, key, Language.Rus));
        }

        [Test]
        public void Decrypt_EN_DecryptedCorrectly()
        {
            string plain = "Con? grat ul, atio: nS! йцуке";
            string cipher = "Uqb? xgih hd, chzd: vG! йцуке";
            string key = "scorpion";

            Assert.AreEqual(plain, _encryptor.Decrypt(cipher, key, Language.Eng));
        }
    }
}