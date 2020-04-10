using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebTakerData.Utilities
{
    public static class RsaCrypto
    {
        public static string Encrypt(this string text, string key)
        {
            var buffer = Encoding.UTF8.GetBytes(text);

            var hash = new SHA512CryptoServiceProvider();

            var aesKey = new byte[24];

            Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);

            using (var aes = Aes.Create())
            {
                aes.Key = aesKey;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))

                using (var resultStream = new MemoryStream())
                {
                    using (var aesStream = new CryptoStream(resultStream, encryptor, CryptoStreamMode.Write))

                    using (var plainStream = new MemoryStream(buffer))
                    {
                        plainStream.CopyTo(aesStream);
                    }

                    var result = resultStream.ToArray();

                    var combined = new byte[aes.IV.Length + result.Length];

                    Array.ConstrainedCopy(aes.IV, 0, combined, 0, aes.IV.Length);

                    Array.ConstrainedCopy(result, 0, combined, aes.IV.Length, result.Length);

                    var resultado = Convert.ToBase64String(combined);

                    return resultado;
                }
            }
        }

        public static string Decrypt(this string encryptedText, string key)
        {
            var combined = Convert.FromBase64String(encryptedText);

            var buffer = new byte[combined.Length];

            var hash = new SHA512CryptoServiceProvider();

            var aesKey = new byte[24];

            Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);

            using (var aes = Aes.Create())
            {
                aes.Key = aesKey;

                var iv = new byte[aes.IV.Length];

                var ciphertext = new byte[buffer.Length - iv.Length];

                Array.ConstrainedCopy(combined, 0, iv, 0, iv.Length);

                Array.ConstrainedCopy(combined, iv.Length, ciphertext, 0, ciphertext.Length);

                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))

                using (var resultStream = new MemoryStream())
                {
                    using (var aesStream = new CryptoStream(resultStream, decryptor, CryptoStreamMode.Write))

                    using (var plainStream = new MemoryStream(ciphertext))
                    {
                        plainStream.CopyTo(aesStream);
                    }
                    var resultado = Encoding.UTF8.GetString(resultStream.ToArray());

                    return resultado;
                }
            }
        }
    }
}
