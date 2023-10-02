using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.DataProtection;
using Windows.Security.Cryptography;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Security.Cryptography.Core;
using System.IO;
using System.Diagnostics;

namespace factoryos_10x_shell.Helpers
{
    internal class PinSecurityManager
    {
        public static void SetEncryptedPin(int[] pin, string machineName)
        {
            try
            {
                string pinHash = CalculatePinHash(pin);
                string encryptionKey = machineName + pinHash;

                byte[] encryptedPin = EncryptArray(pin, encryptionKey);
                ApplicationData.Current.LocalSettings.Values["EncryptedPIN"] = Convert.ToBase64String(encryptedPin);
            }
            catch(Exception ex) { Debug.WriteLine(ex.Message); }
        }

        public static bool CheckPin(int[] enteredPin, string machineName)
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("EncryptedPIN", out object encryptedPinObj))
            {
                string encryptedPinString = encryptedPinObj as string;
                byte[] encryptedPin = Convert.FromBase64String(encryptedPinString);

                string pinHash = CalculatePinHash(enteredPin);
                string encryptionKey = machineName + pinHash;

                int[] decryptedPin = DecryptArray(encryptedPin, encryptionKey);

                return ArraysEqual(decryptedPin, enteredPin);
            }
            return false;
        }

        private static string CalculatePinHash(int[] pin)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] pinBytes = Encoding.UTF8.GetBytes(string.Join(",", pin));
                byte[] hashBytes = sha256.ComputeHash(pinBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private static byte[] EncryptArray(int[] data, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.GenerateIV();

                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    byte[] plaintextBytes = Encoding.UTF8.GetBytes(string.Join(",", data));
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(plaintextBytes, 0, plaintextBytes.Length);
                            csEncrypt.FlushFinalBlock();
                            return aesAlg.IV.Concat(msEncrypt.ToArray()).ToArray();
                        }
                    }
                }
            }
        }

        private static int[] DecryptArray(byte[] encryptedData, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                byte[] iv = encryptedData.Take(16).ToArray();
                byte[] cipherText = encryptedData.Skip(16).ToArray();

                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv))
                {
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            byte[] decryptedBytes = new byte[cipherText.Length];
                            csDecrypt.Read(decryptedBytes, 0, decryptedBytes.Length);
                            string decryptedDataString = Encoding.UTF8.GetString(decryptedBytes);
                            return Array.ConvertAll(decryptedDataString.Split(','), int.Parse);
                        }
                    }
                }
            }
        }

        private static bool ArraysEqual(int[] arr1, int[] arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                    return false;
            }
            return true;
        }
    }
}
