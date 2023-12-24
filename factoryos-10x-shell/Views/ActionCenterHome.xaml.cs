using factoryos_10x_shell.Library.ViewModels;
using factoryos_10x_shell.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace factoryos_10x_shell.Views
{
    public sealed partial class ActionCenterHome : Page
    {
        public static bool connected;
        private bool isExpaneded = false;
        public ActionCenterHome()
        {
            this.InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<ActionCenterHomeViewModel>();

            //InternetInit();
            //Init();
        }

        #region Init
        /*private void Init()
        {
            if (Default10xBar.batteryActionCenterEnabled == true)
            {
                BatteryPercent.Text = Default10xBar.batteryActionCenter;
            }
            else
            {
                BatteryPercent.Text = "";
            }

            if (Default10xBar.notifcationCount > 0)
            {
                NotifCount.Text = Default10xBar.notifcationCount.ToString();
                NotifRootPanel.Visibility = Visibility.Visible;
            }
            else
            {
                NotifRootPanel.Visibility = Visibility.Collapsed;
            }
        }*/
        #endregion

        #region Border events
        private async void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog powerDialog = new PowerDialog();
            await powerDialog.ShowAsync();
        }

        private void ExpnanderControlButton_Click(object sender, RoutedEventArgs e)
        {
            isExpaneded = !isExpaneded;
            if(isExpaneded)
            {
                ControlPanel.Height = 250;
                ExpanderIcon.Text = "\uE09D";
            }
            else
            {
                ControlPanel.Height = 80;
                ExpanderIcon.Text = "\uE09C";
            }
        }

        private async void BatteryButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:batterysaver"));
        }
        private async void LockButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog lockDialog = new LockDialog();
            await lockDialog.ShowAsync();
        }

        private async void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:"));
        }

        private async void OSKButton_Click(object sender, RoutedEventArgs e)
        {
            // todo: IMPLEMENT OPENING OSK
        }
        private void ClearNotifs_Click(object sender, RoutedEventArgs e)
        {
            NotifCount.Text = "0";
            NotifRootPanel.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Control events
        private string GetNetName()
        {
            IReadOnlyList<string> names = NetworkHelper.Instance.ConnectionInformation.NetworkNames;
            return names.FirstOrDefault();
        }
        /*private void InternetInit()
        {
            string[] connectionString = { "Not connected", "Ethernet", GetNetName(), "Connected" };
            NetworkStatus.Text = connectionString[Default10xBar.connectionStatus];
            InternetButton.IsChecked = connected;
        }*/

        private void VolumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if(VolumeSlider.Value == 0)
            {
                SndStatus.Text = "\uE198";
            }
            else
            {
                SndStatus.Text = "\uE15D";
            }
        }

        #endregion
    }
}
