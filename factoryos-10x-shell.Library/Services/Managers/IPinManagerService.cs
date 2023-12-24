using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.Services.Managers
{
    public interface IPinManagerService
    {
        /// <summary>
        /// Sets the pin and encrypts it.
        /// </summary>
        /// <param name="pin">The 4-digit array of numbers that represent the new pin.</param>
        /// <param name="machineName">The name of the machine that the pin is stored on. Used for salt.</param>
        public void SetEncryptedPin(int[] pin, string machineName);

        /// <summary>
        /// Checks if the pin input is equal to the stored one.
        /// </summary>
        /// <param name="enteredPin">The 4-digit array of numbers that represent the entered pin.</param>
        /// <param name="machineName">The name of the machine that the pin is stored on. Used for key derivement.</param>
        /// <returns></returns>
        public bool CheckPin(int[] enteredPin, string machineName);
    }
}
