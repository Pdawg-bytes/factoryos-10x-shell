using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using factoryos_10x_shell.Library.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using System.Diagnostics;
using System.Numerics;

namespace factoryos_10x_shell.Library.ViewModels
{
    public partial class MainDesktopViewModel : ObservableObject
    {
        private readonly IStartManagerService m_startManager;


        public MainDesktopViewModel(IStartManagerService startManager) 
        {
            m_startManager = startManager;
        }
    }
}
