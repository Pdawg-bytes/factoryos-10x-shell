using factoryos_10x_shell.Helpers;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace factoryos_10x_shell.Controls
{
    public sealed partial class ShutdownDialog : ContentDialog
    {
        public ShutdownDialog()
        {
            this.InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private async void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // TODO: implement shutdown
            }
            catch(Exception shutdown)
            {
                this.Hide();
                ErrorDialog shutdownError = new ErrorDialog(shutdown.Message + " Exception full message: " + shutdown.ToString());
                shutdownError.ShowAsync();
            }
        }

        private async void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // TODO: implement restart
            }
            catch (Exception restart)
            {
                this.Hide();
                ErrorDialog restartError = new ErrorDialog(restart.Message + " Exception full message: " + restart.ToString());
                restartError.ShowAsync();
            }
        }
    }
}
