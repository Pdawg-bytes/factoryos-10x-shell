﻿using factoryos_10x_shell.Library.Services.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace factoryos_10x_shell.Services.Hardware
{
    internal class NetworkService : INetworkService, IDisposable
    {
        public InternetConnection InternetType
        {
            get
            {
                ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();

                if (profile == null)
                    return InternetConnection.Unknown;

                switch (profile.NetworkAdapter.IanaInterfaceType)
                {
                    case 6:
                        return InternetConnection.Wired;
                    case 71:
                        return InternetConnection.Wireless;
                    case 243:
                    case 244:
                        return InternetConnection.Data;
                    default:
                        return InternetConnection.Unknown;
                }
            }
        }

        public bool IsInternetAvailable
        {
            get
            {
                ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();
                return profile != null && profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            }
        }

        public byte SignalStrength
        {
            get
            {
                ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();
                return profile.GetSignalBars() ?? 0;
            }
        }

        public event EventHandler InternetStatusChanged;

        public NetworkService()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
        }

        private void NetworkInformation_NetworkStatusChanged(object sender)
        {
            InternetStatusChanged?.Invoke(this, EventArgs.Empty);
        }


        public void Dispose()
        {
            NetworkInformation.NetworkStatusChanged -= NetworkInformation_NetworkStatusChanged;
        }
    }
}