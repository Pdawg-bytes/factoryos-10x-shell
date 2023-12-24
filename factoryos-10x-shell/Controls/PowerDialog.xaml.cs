using factoryos_10x_shell.Helpers;
using Microsoft.Extensions.DependencyInjection;
using factoryos_10x_shell.Library.ViewModels;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace factoryos_10x_shell.Controls
{
    public sealed partial class PowerDialog : ContentDialog
    {
        public PowerDialog()
        {
            this.InitializeComponent();

            this.DataContext = App.ServiceProvider.GetRequiredService<PowerDialogViewModel>();
        }

        public PowerDialogViewModel ViewModel => (PowerDialogViewModel)this.DataContext;

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
