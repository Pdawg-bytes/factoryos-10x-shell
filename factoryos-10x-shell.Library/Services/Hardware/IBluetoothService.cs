using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.Services.Hardware
{
    public interface IBluetoothService
    {
        public string ConnectedDeviceName { get; }

        public Task InitializeAsync();

        public bool IsBluetoothEnabled { get; set; }
    }
}
