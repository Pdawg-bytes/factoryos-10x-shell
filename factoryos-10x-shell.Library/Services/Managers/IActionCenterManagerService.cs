using factoryos_10x_shell.Library.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.Services.Managers
{
    /// <summary>
    /// Defines a manager service that handles interactions with the Action Center.
    /// </summary>
    public interface IActionCenterManagerService
    {
        /// <summary>
        /// If the Action Center is open or not.
        /// </summary>
        public bool IsActionCenterOpen { get; set; }

        /// <summary>
        /// If the full toggle list is expanded or not.
        /// </summary>
        public bool IsToggleListExpanded { get; set; }

        /// <summary>
        /// Requests the visibility of the Action Center to change.
        /// </summary>
        /// <param name="visible">The requested visibility.</param>
        /// <remarks>true = visible; false = hidden.</remarks>
        public void RequestActionVisibilityChange(bool visible);

        /// <summary>
        /// An event that is raised when the Action Center's visibility changes.
        /// </summary>
        public event EventHandler<ActionCenterVisibilityChangedEventArgs> ActionVisibilityChanged;
    }
}
