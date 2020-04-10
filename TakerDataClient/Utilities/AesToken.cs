using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebTakerData.Utilities
{
    public static class AesToken
    {
        private static Random random = new Random();
        private const char DELIMITER = ']';
        private const string key = "ItK8MY4BgI9G3ifzRgKb0ERbVEk+L2qYkpfOIVHwHrE=";

        public static string EncryptToAES(this string value, string key)
        {
            byte[] keyBytes = Convert.FromBase64String(key);

            byte[] ivBytes = generateBytes(16);

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);

            AesManaged tdes = new AesManaged();

            tdes.Key = keyBytes;

            tdes.Mode = CipherMode.CBC;

            tdes.Padding = PaddingMode.PKCS7;

            tdes.IV = ivBytes;

            var crypt = tdes.CreateEncryptor();

            var cipherBytes = crypt.TransformFinalBlock(valueBytes, 0, valueBytes.Length);

            return string.Format("{0}" + DELIMITER + "{1}", Convert.ToBase64String(ivBytes), Convert.ToBase64String(cipherBytes));
        }

        private static byte[] generateBytes(int length)
        {
            byte[] b = new byte[length];

            random.NextBytes(b);

            return b;
        }

        public static string DecryptAES(this string value, string key)
        {
            string[] fields = value.Split(DELIMITER);

            byte[] keyBytes = Convert.FromBase64String(key);

            byte[] ivBytes = Convert.FromBase64String(fields[0]);

            byte[] cipherBytes = Convert.FromBase64String(fields[1]);

            AesManaged tdes = new AesManaged();

            tdes.Key = keyBytes;

            tdes.Mode = CipherMode.CBC;

            tdes.Padding = PaddingMode.PKCS7;

            tdes.IV = ivBytes;

            var crypt = tdes.CreateDecryptor();

            byte[] desencrypted = crypt.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(desencrypted);
        }

    }
}
