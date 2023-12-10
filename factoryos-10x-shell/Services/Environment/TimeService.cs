using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using factoryos_10x_shell.Library.Services.Environment;

namespace factoryos_10x_shell.Services.Environment
{
    internal class TimeService : ITimeService, IDisposable
    {
        private Timer updateCheck;
        private int lastUpdateSecond;

        public string ClockFormat { get; set; }
        public string DateFormat { get; set; }

        public TimeService()
        {
            ClockFormat = "h:mm";
            DateFormat = "M/d/yyyy";

            updateCheck = new Timer();
            updateCheck.Interval = 10;
            updateCheck.Elapsed += UpdateCheck_Elapsed;

            updateCheck.Start();
        }

        private void UpdateCheck_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            if (currentTime.Second != lastUpdateSecond)
            {
                lastUpdateSecond = currentTime.Second;
                UpdateClockBinding?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler UpdateClockBinding;

        public void Dispose()
        {
            updateCheck.Elapsed -= UpdateCheck_Elapsed;
        }
    }
}
