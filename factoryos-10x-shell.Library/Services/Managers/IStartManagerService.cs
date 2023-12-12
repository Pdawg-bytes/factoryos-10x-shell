using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace factoryos_10x_shell.Library.Services.Managers
{
    /// <summary>
    /// Defines a manager service that handles interactions with the Start Menu.
    /// </summary>
    public interface IStartManagerService
    {
        /// <summary>
        /// If the Start Menu is open or not.
        /// </summary>
        public bool IsStartOpen { get; set; }

        /// <summary>
        /// If the app list is expanded or not.
        /// </summary>
        public bool IsAppsListExpanded { get; set; }

        /// <summary>
        /// Requests the visibility of the Start Menu to change.
        /// </summary>
        /// <param name="visible">The requested visibility.</param>
        /// <remarks>true = visible; false = hidden.</remarks>
        public void RequestStartVisibilityChange(bool visible);

        /// <summary>
        /// An event that is raised when the Start Menu's visibility changes.
        /// </summary>
        public event EventHandler StartVisibilityChanged;
    }
}
