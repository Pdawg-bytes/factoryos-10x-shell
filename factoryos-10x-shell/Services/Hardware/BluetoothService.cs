using factoryos_10x_shell.Library.Services.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Windows.Devices.Radios;

namespace factoryos_10x_shell.Services.Hardware
{
    internal class BluetoothService : IBluetoothService
    {
        public bool IsBluetoothEnabled 
        {
            get
            {
                //return GetBluetoothIsEnabledAsync().Wait();
                return true;
            }
            set
            {

            }
        }

        public string ConnectedDeviceName { get; }

        private BluetoothAdapter _bluetoothAdapter;

        public BluetoothService() 
        {
            
        }

        public async Task InitializeAsync()
        {
            _bluetoothAdapter = await BluetoothAdapter.GetDefaultAsync();
            return;
        }

        private async Task<bool> GetBluetoothIsEnabledAsync()
        {
            if (_bluetoothAdapter == null)
            {
                return false;
            }

            var radios = await Radio.GetRadiosAsync();
            var bluetoothRadio = radios.FirstOrDefault(radio => radio.Kind == RadioKind.Bluetooth);
            return bluetoothRadio != null && bluetoothRadio.State == RadioState.On;
        }
    }
}
