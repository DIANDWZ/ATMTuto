using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ATMTuto
{
    public static class AESHelper
    {
        private static readonly byte[] Key = new byte[] { 
            0x41, 0x54, 0x4D, 0x54, 0x75, 0x74, 0x6F, 0x32,
            0x30, 0x32, 0x34, 0x53, 0x65, 0x63, 0x75, 0x72,
            0x69, 0x74, 0x79, 0x4B, 0x65, 0x79, 0x46, 0x6F,
            0x72, 0x41, 0x45, 0x53, 0x32, 0x35, 0x36, 0x00
        };
        private static readonly byte[] IV = new byte[] { 
            0x41, 0x54, 0x4D, 0x54, 0x75, 0x74, 0x6F, 0x32,
            0x30, 0x32, 0x34, 0x49, 0x6E, 0x69, 0x74, 0x56
        };

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return null;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return null;

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    byte[] buffer = Convert.FromBase64String(cipherText);

                    using (MemoryStream ms = new MemoryStream(buffer))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static string EncryptAmount(int amount)
        {
            return Encrypt(amount.ToString());
        }

        public static int DecryptAmount(string encryptedAmount)
        {
            string decrypted = Decrypt(encryptedAmount);
            if (string.IsNullOrEmpty(decrypted))
                return 0;
            return int.TryParse(decrypted, out int result) ? result : 0;
        }

        public static string MaskPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length < 11)
                return phone;
            return phone.Substring(0, 3) + "****" + phone.Substring(7);
        }
    }
}