using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebTakerData.Utilities
{
    public static class CryptoServiceProvider
    {
        public static string Encrypt(string ToEncrypt, string Key)
        {
            byte[] keyArray;

            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(ToEncrypt);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
            tDes.Key = keyArray;

            tDes.Mode = CipherMode.ECB;

            tDes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tDes.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tDes.Clear();

            var text = Convert.ToBase64String(resultArray, 0, resultArray.Length);

            return text;
        }

        public static string Decrypt(string cypherString, string Key)
        {
            byte[] keyArray;

            byte[] toDecryptArray = Convert.FromBase64String(cypherString);

            MD5CryptoServiceProvider hashmd = new MD5CryptoServiceProvider();

            keyArray = hashmd.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));

            hashmd.Clear();

            TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
            tDes.Key = keyArray;

            tDes.Mode = CipherMode.ECB;

            tDes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tDes.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);

            tDes.Clear();

            var text = UTF8Encoding.UTF8.GetString(resultArray, 0, resultArray.Length);

            return text;

        }


    }
}
