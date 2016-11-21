using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace JunkBox.Common
{
    public class Password
    {
        public static byte[] ComputeSaltBytes()
        {
            byte[] saltBytes;

            int minSaltSize = 4;
            int maxSaltSize = 8;

            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);

            saltBytes = new byte[saltSize];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            rng.GetNonZeroBytes(saltBytes);

            return saltBytes;
        }

        public static string ComputeHash(string plainText, byte[] saltBytes)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash = new SHA512Managed();

            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            return hashValue;
        }

        public static bool VerifyHash(string plainText, string hashValue)
        {
            try
            {
                byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

                int hashSizeInBits = 512,
                    hashSizeInBytes = hashSizeInBits / 8;

                if (hashWithSaltBytes.Length < hashSizeInBytes)
                    return false;

                byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

                for (int i = 0; i < saltBytes.Length; i++)
                    saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

                string expectedHashString = ComputeHash(plainText, saltBytes);

                return (hashValue == expectedHashString);
            }
            catch (FormatException e)
            {
                return false;
            }
        }
    }
}