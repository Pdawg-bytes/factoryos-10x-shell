using factoryos_10x_shell.Library.Events;
using factoryos_10x_shell.Library.Models.InternalData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Management.Deployment;

namespace factoryos_10x_shell.Library.Services.Helpers
{
    /// <summary>
    /// A service that provides data about the UWP apps on the system.
    /// </summary>
    public interface IAppHelper
    {
        public ObservableCollection<StartIconModel> StartIcons { get; set; }

        public Task LoadAppsAsync();

        public Package PackageFromAumid(string aumid);
    }
}
