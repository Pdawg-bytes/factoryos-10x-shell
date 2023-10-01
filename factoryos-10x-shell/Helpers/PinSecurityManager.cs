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

namespace factoryos_10x_shell.Helpers
{
    internal class PinSecurityManager
    {
        public static bool SetPin(int[] pin)
        {
            try
            {
                string machineName = Environment.MachineName;
                string salt = GenerateSalt();
                string pinHash = HashPin(pin, salt);

                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values["PinHashKey"] = pinHash;
                localSettings.Values["SaltKey"] = salt;
                localSettings.Values["MachineNameKey"] = machineName;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckPin(int[] enteredPin)
        {
            try
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                string storedPinHash = localSettings.Values["PinHashKey"] as string;
                string storedSalt = localSettings.Values["SaltKey"] as string;
                string storedMachineName = localSettings.Values["MachineNameKey"] as string;

                if (storedPinHash != null && storedSalt != null && storedMachineName != null)
                {
                    string enteredPinHash = HashPin(enteredPin, storedSalt);
                    return enteredPinHash == storedPinHash && Environment.MachineName == storedMachineName;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private static string GenerateSalt()
        {
            return Guid.NewGuid().ToString();
        }

        private static string HashPin(int[] pin, string salt)
        {
            string pinAsString = string.Join("", pin);
            string dataToHash = pinAsString + salt;
            byte[] dataBytes = Encoding.UTF8.GetBytes(dataToHash);

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(dataBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
