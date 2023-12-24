using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using factoryos_10x_shell.Library.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.ViewModels
{
    public partial class LockDialogViewModel : ObservableObject
    {
        private readonly IDesktopNavigator m_desktopNavigator;

        public LockDialogViewModel(IDesktopNavigator desktopNavigator) 
        {
            m_desktopNavigator = desktopNavigator;
        }

        [RelayCommand]
        private void NavigateToLockScreen()
        {
            m_desktopNavigator.DesktopNavigate(DesktopPageType.RootContentLockScreen);
        }
    }
}
