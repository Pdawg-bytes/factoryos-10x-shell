using CommunityToolkit.Mvvm.ComponentModel;
using factoryos_10x_shell.Library.Services.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.ViewModels
{
    public partial class ActionCenterHomeViewModel : ObservableObject
    {
        private readonly IBatteryService m_powerService;

        public ActionCenterHomeViewModel(IBatteryService powerService) 
        {
            m_powerService = powerService;
        }


    }
}
