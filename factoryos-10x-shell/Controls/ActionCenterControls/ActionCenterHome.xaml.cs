using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace factoryos_10x_shell.Controls.ActionCenterControls
{
    public sealed partial class ActionCenterHome : Page
    {
        public ActionCenterHome()
        {
            this.InitializeComponent();
            BatteryPercent.Text = Default10xBar.batteryActionCenter;
        }

        #region Border events
        private async void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog powerDialog = new ShutdownDialog();
            await powerDialog.ShowAsync();
        }

        private void ExpnanderControlButton_Checked(object sender, RoutedEventArgs e)
        {
            ControlPanel.Height = 250;
            ExpanderIcon.Text = "\uE09D";
        }

        private void ExpnanderControlButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ControlPanel.Height = 80;
            ExpanderIcon.Text = "\uE09C";
        }
        #endregion

        #region Control events
        private void VolumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            // TODO: Implement this
        }

        #endregion
    }
}
