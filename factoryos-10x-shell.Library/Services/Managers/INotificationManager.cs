using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications.Management;

namespace factoryos_10x_shell.Library.Services.Managers
{
    /// <summary>
    /// Defines a WinRT based service to handle the user's notifcations.
    /// </summary>
    public interface INotificationManager
    {
        /// <summary>
        /// The current Notification Listener.
        /// </summary>
        public UserNotificationListener NotificationListener { get; set; }

        /// <summary>
        /// Event that is fired when any notification is changed.
        /// </summary>
        public event EventHandler NotifcationChanged;

        /// <summary>
        /// The accessibility of the user's notifications.
        /// </summary>
        public UserNotificationListenerAccessStatus NotificationAccessStatus { get; set; }
    }
}
