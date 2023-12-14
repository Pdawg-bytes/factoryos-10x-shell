using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using factoryos_10x_shell.Library.Models.InternalData;

namespace factoryos_10x_shell.Library.Events
{
    public class AppsListUpdatedEventArgs : EventArgs
    {
        public AppsListUpdatedEventArgs(ObservableCollection<StartIconModel> appIcons) 
        {
            AppIcons = appIcons;
        }

        public ObservableCollection<StartIconModel> AppIcons { get; set; }
    }
}
