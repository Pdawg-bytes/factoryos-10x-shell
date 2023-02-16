using System;
using System.Collections.Generic;
using Microsoft.Toolkit.Uwp.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace factoryos_10x_shell.Controls
{
    public sealed partial class Default10xBar : Page
    {
        public Default10xBar()
        {
            this.InitializeComponent();

            // Init
            TimeAndDate();
        }

        #region Clock
        private void TimeAndDate()
        {
            DispatcherTimer dateTimeUpdate = new DispatcherTimer();
            dateTimeUpdate.Tick += DTUpdateMethod;
            dateTimeUpdate.Interval = new TimeSpan(100000);
            dateTimeUpdate.Start();
        }
        private void DTUpdateMethod(object sender, object e)
        {
            ClockText.Text = DateTime.Now.ToString("h:mm");
        }
        #endregion

        #region Internet
        private void InternetUpdate()
        {
            DispatcherTimer internetUpdate = new DispatcherTimer();
            internetUpdate.Tick += ITUpdateMethod;
            internetUpdate.Interval = new TimeSpan(10000000);
            internetUpdate.Start();
        }
        private void ITUpdateMethod(object sender, object e)
        {
            if (NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable)
            {
                switch (NetworkHelper.Instance.ConnectionInformation.ConnectionType)
                {
                    case ConnectionType.Ethernet:
                        WifiStatus.Text = "\uE839";
                        break;
                    case ConnectionType.WiFi:
                        int WifiSignalBars = NetworkHelper.Instance.ConnectionInformation.SignalStrength.GetValueOrDefault(0);
                        switch (WifiSignalBars)
                        {
                            case 0:
                            default:
                                WifiStatus.Text = "\uE871";
                                break;
                            case 1:
                                WifiStatus.Text = "\uE872";
                                break;
                            case 2:
                                WifiStatus.Text = "\uE873";
                                break;
                            case 3:
                                WifiStatus.Text = "\uE874";
                                break;
                            case 4:
                                WifiStatus.Text = "\uE701";
                                break;
                        }
                        break;
                    case ConnectionType.Data:
                        int DataSignalBars = NetworkHelper.Instance.ConnectionInformation.SignalStrength.GetValueOrDefault(0);
                        switch (DataSignalBars)
                        {
                            case 1:
                            default:
                                WifiStatus.Text = "\uEC37";
                                break;
                            case 2:
                                WifiStatus.Text = "\uEC38";
                                break;
                            case 3:
                                WifiStatus.Text = "\uEC39";
                                break;
                            case 4:
                                WifiStatus.Text = "\uEC3A";
                                break;
                            case 5:
                                WifiStatus.Text = "\uEC3B";
                                break;
                        }
                        break;
                    case ConnectionType.Unknown:
                    default:
                        WifiStatus.Text = "\uE774";
                        break;
                }
            }
            else
            {
                WifiStatus.Text = "\uF140";
            }
        }
        #endregion
    }
}
