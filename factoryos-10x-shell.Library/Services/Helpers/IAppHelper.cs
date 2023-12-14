using factoryos_10x_shell.Library.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.Services.Helpers
{
    /// <summary>
    /// A service that provides data about the UWP apps on the system.
    /// </summary>
    public interface IAppHelper
    {
        public event EventHandler<AppsListUpdatedEventArgs> AppsListUpdated;
    }
}
