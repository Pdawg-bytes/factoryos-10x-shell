using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.Services.Hardware
{
    public interface IBluetoothService
    {
        public bool IsBluetoothEnabled { get; set; }

        public string ConnectedDeviceName { get; }

        public Task InitializeAsync();
    }
}
