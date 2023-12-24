using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using factoryos_10x_shell.Library.Services.Managers;
using Windows.Storage;

namespace factoryos_10x_shell.Services.Managers
{
    internal class PinManagerService : IPinManagerService
    {
        public PinManagerService() 
        {

        }


        public void SetEncryptedPin(int[] pin, string machineName)
        {
            string pinHash = CalculatePinHash(pin);
            byte[] encryptionKey = DeriveKeyFromPin(pinHash, machineName);

            byte[] encryptedPin = EncryptArray(pin, encryptionKey);
            ApplicationData.Current.LocalSettings.Values["EncryptedPIN"] = Convert.ToBase64String(encryptedPin);
        }

        public bool CheckPin(int[] enteredPin, string machineName)
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("EncryptedPIN", out object encryptedPinObj))
            {
                if (enteredPin.Length == 4)
                {
                    string encryptedPinString = encryptedPinObj as string;
                    byte[] encryptedPin = Convert.FromBase64String(encryptedPinString);

                    string pinHash = CalculatePinHash(enteredPin);
                    byte[] encryptionKey = DeriveKeyFromPin(pinHash, machineName);

                    int[] decryptedPin = DecryptArray(encryptedPin, encryptionKey);
                    
                    return ArraysEqual(decryptedPin, enteredPin);
                }
            }
            return false;
        }

        private string CalculatePinHash(int[] pin)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] pinBytes = Encoding.UTF8.GetBytes(string.Join(",", pin));
                byte[] hashBytes = sha256.ComputeHash(pinBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private byte[] DeriveKeyFromPin(string pinHash, string machineName)
        {
            byte[] machineSalt = Encoding.UTF8.GetBytes(machineName);
            if (machineSalt.Length < 8)
            {
                for (int i = machineSalt.Length; i < 8; i++)
                {
                    byte[] originalArray = machineSalt;
                    byte[] dataToAppend = BitConverter.GetBytes(i);

                    machineSalt = originalArray.Concat(dataToAppend).ToArray();
                }
            }
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(pinHash, machineSalt, 10000, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32);
            }
        }

        private byte[] EncryptArray(int[] data, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
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

        private int[] DecryptArray(byte[] encryptedData, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                byte[] iv = encryptedData.Take(16).ToArray();
                byte[] cipherText = encryptedData.Skip(16).ToArray();

                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv))
                {
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            try
                            {
                                byte[] decryptedBytes = new byte[cipherText.Length];
                                csDecrypt.Read(decryptedBytes, 0, decryptedBytes.Length);
                                string decryptedDataString = Encoding.UTF8.GetString(decryptedBytes);
                                return Array.ConvertAll(decryptedDataString.Split(','), int.Parse);
                            }
                            catch
                            {
                                return null;
                            }
                        }
                    }
                }
            }
        }

        private bool ArraysEqual(int[] arr1, int[] arr2)
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
