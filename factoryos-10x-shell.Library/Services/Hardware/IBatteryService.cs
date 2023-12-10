using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using factoryos_10x_shell.Library.Models.Hardware;

namespace factoryos_10x_shell.Library.Services.Hardware
{
    /// <summary>
    /// Defines a platform-agnostic service interface to get battery information.
    /// </summary>
    public interface IBatteryService
    {
        /// <summary>
        /// An event raised when the battery's status changes.
        /// </summary>
        public event EventHandler BatteryStatusChanged;

        /// <summary>
        /// Gets a status report of the system's battery.
        /// </summary>
        /// <returns>The new <see cref="BatteryReport"/> object.</returns>
        public BatteryReport GetStatusReport();

        /// <summary>
        /// Checks if there is a battery installed in the system.
        /// </summary>
        public bool IsBatteryInstalled { get; }
    }
}
