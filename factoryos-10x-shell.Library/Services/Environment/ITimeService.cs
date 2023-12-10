using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.Services.Environment
{
    /// <summary>
    /// Defines a platform-agonistic service to provide clock data.
    /// </summary>
    public interface ITimeService
    {
        /// <summary>
        /// The formatter string for the clock.
        /// </summary>
        public string ClockFormat { get; set; }

        /// <summary>
        /// The formatter string for the date.
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// The event that is raised to update frontend properties.
        /// </summary>
        public event EventHandler UpdateClockBinding;
    }
}
