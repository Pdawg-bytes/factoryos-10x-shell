using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.Models.Hardware
{
    public enum BatteryPowerStatus
    {
        NotInstalled,
        Draining,
        Idle,
        Charging
    }

    public class BatteryReport
    {
        public double ChargeRate;

        public double FullCapacity;

        public double RemainingCapacity;

        public int ChargePercentage;

        public BatteryPowerStatus PowerStatus;
    }
}
