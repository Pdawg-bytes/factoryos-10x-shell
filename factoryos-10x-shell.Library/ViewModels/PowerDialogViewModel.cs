using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace factoryos_10x_shell.Library.ViewModels
{
    public partial class PowerDialogViewModel : ObservableObject
    {
        public PowerDialogViewModel() 
        {
            MainDialogVisibility = Visibility.Visible;
            ShutdownVisibility = Visibility.Collapsed;
            RestartVisibility = Visibility.Collapsed;
        }

        [ObservableProperty]
        private Visibility mainDialogVisibility;

        [ObservableProperty]
        private Visibility shutdownVisibility;

        [ObservableProperty]
        private Visibility restartVisibility;

        [RelayCommand]
        private void ShutdownClicked()
        {
            MainDialogVisibility = Visibility.Collapsed;
            ShutdownVisibility = Visibility.Visible;
        }

        [RelayCommand]
        private void RestartClicked()
        {
            MainDialogVisibility = Visibility.Collapsed;
            RestartVisibility = Visibility.Visible;
        }
    }
}
