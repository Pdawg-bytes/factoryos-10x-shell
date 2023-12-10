using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using factoryos_10x_shell.Library.Models.Hardware;
using factoryos_10x_shell.Library.Services.Hardware;
using BatteryReport = factoryos_10x_shell.Library.Models.Hardware.BatteryReport;
using Windows.Devices.Power;
using Windows.System.Power;

namespace factoryos_10x_shell.Services.Hardware
{
    internal class BatteryService : IBatteryService
    {
        public bool IsBatteryInstalled
        {
            get
            {
                BatteryReport report = GetStatusReport();
                return report.PowerStatus != BatteryPowerStatus.NotInstalled;
            }
        }

        public event EventHandler BatteryStatusChanged;

        public BatteryService()
        {
            Battery.AggregateBattery.ReportUpdated += AggregateBattery_ReportUpdated;
        }

        public BatteryReport GetStatusReport()
        {
            Battery aggBattery = Battery.AggregateBattery;
            Windows.Devices.Power.BatteryReport report = aggBattery.GetReport();

            BatteryReport batteryReport = new BatteryReport()
            {
                ChargeRate = report.ChargeRateInMilliwatts.GetValueOrDefault(0),
                FullCapacity = report.FullChargeCapacityInMilliwattHours.GetValueOrDefault(0),
                RemainingCapacity = report.RemainingCapacityInMilliwattHours.GetValueOrDefault(0)
            };

            batteryReport.PowerStatus = report.Status switch
            {
                BatteryStatus.NotPresent => BatteryPowerStatus.NotInstalled,
                BatteryStatus.Idle => BatteryPowerStatus.Idle,
                BatteryStatus.Charging => BatteryPowerStatus.Charging,
                BatteryStatus.Discharging => BatteryPowerStatus.Draining,
                _ => BatteryPowerStatus.NotInstalled
            };

            if (batteryReport.PowerStatus != BatteryPowerStatus.NotInstalled)
                batteryReport.ChargePercentage = (int)Math.Ceiling((double)(batteryReport.RemainingCapacity / batteryReport.FullCapacity) * 100);
            else
                batteryReport.ChargePercentage = 0;

            return batteryReport;
        }

        private void AggregateBattery_ReportUpdated(Battery sender, object args)
        {
            BatteryStatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
