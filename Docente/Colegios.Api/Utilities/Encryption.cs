using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Colegios.Api.Utilities
{
    public static class Encryption
    {
        private static byte[] key = Encoding.UTF8.GetBytes("gxVn8N7UplrM+8hP");
        private static byte[] iv = Encoding.UTF8.GetBytes("M9d/y+y7vakiED5o");

        /// <summary>
        /// descifrar bytes[] AES
        /// </summary>
        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");

            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");

            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("key");

            string plaintext = null;

            using var AES = Aes.Create("AesManaged");
            AES.Mode = CipherMode.ECB;
            AES.Padding = PaddingMode.PKCS7;
            AES.FeedbackSize = 128;
            AES.Key = key;
            AES.IV = iv;

            var decryptor = AES.CreateDecryptor();

            try
            {
                using var msDecrypt = new MemoryStream(cipherText);
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using (var srDecrypt = new StreamReader(csDecrypt))
                    plaintext = srDecrypt.ReadToEnd();
            }
            catch
            {
                plaintext = "keyError";
            }

           
            return plaintext;
        }


        /// <summary>
        /// descifrar string AES 
        /// </summary> 
        public static string DecryptStringAES(string cipherText)
        {
            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, key, iv);
            return decriptedFromJavascript;
        }
        /// <summary>
        /// descrifrar extension string AES 
        /// </summary> 
        public static string DecryptAES(this string cipherText)
        {
            return DecryptStringAES(cipherText);
        }
        /// <summary>
        /// cifrar string AES 
        /// </summary> 
        public static string EncryptStringAES(string plainText)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");

            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");

            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("key");

            byte[] encrypted;

            using var AES = Aes.Create("AesManaged");
            AES.Mode = CipherMode.ECB;
            AES.Padding = PaddingMode.PKCS7;
            AES.FeedbackSize = 128;
            AES.Key = key;
            AES.IV = iv;

            var encryptor = AES.CreateEncryptor();

            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
            encrypted = msEncrypt.ToArray();

            return Convert.ToBase64String(encrypted);
        }


        /// <summary>
        /// cifrar extension string AES 
        /// </summary> 
        public static string EncryptAES(this string plainText)
        {
            return EncryptStringAES(plainText);
        }

    }
}
