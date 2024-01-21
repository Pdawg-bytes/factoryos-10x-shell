using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.Services.Hardware
{
    public enum InternetConnection
    {
        /// <summary>
        /// A wired connection, usually Ethernet.
        /// </summary>
        Wired,

        /// <summary>
        /// A wireless connection, usually Wi-Fi.
        /// </summary>
        Wireless,

        /// <summary>
        /// A satellite or mobile data connection.
        /// </summary>
        Data,

        /// <summary>
        /// An unknown type of connection, or no connection at all.
        /// </summary>
        Unknown
    }

    /// <summary>
    /// Defines a platform-agnostic service interface to get network information.
    /// </summary>
    public interface INetworkService
    {
        /// <summary>
        /// The type of internet connection in use.
        /// </summary>
        public InternetConnection InternetType { get; }

        /// <summary>
        /// The name of the current connection.
        /// </summary>
        public string ConnectionName { get; }

        /// <summary>
        /// Checks if an internet connection is available.
        /// </summary>
        public bool IsInternetAvailable { get; }

        /// <summary>
        /// Gets the signal strength of the connection in case of a wireless one.
        /// </summary>
        /// <remarks>Returns 0 on wired or unknown connections.</remarks>
        public byte SignalStrength { get; }

        public bool IsAdapterEnabled { get; set; }

        /// <summary>
        /// An event raised when the network's status changes.
        /// </summary>
        public event EventHandler InternetStatusChanged;
    }
}
