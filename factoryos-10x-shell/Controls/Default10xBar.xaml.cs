using System;
using System.Collections.Generic;
using Microsoft.Toolkit.Uwp.Connectivity;
using Windows.Devices.Power;
using Windows.System;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using factoryos_10x_shell.Controls.ActionCenterControls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Foundation.Metadata;
using Windows.UI.Notifications.Management;
using Windows.UI.Notifications;
using Windows.UI.Core;
using Windows.Networking.Connectivity;

namespace factoryos_10x_shell.Controls
{
    public sealed partial class Default10xBar : Page
    {
        bool reportRequested = false;
        public static string batteryActionCenter;

        public static bool batteryActionCenterEnabled;
        public static bool startLaunched = false;

        public static int connectionStatus;
        public static int notifcationCount;

        public Default10xBar()
        {
            this.InitializeComponent();

            // Init
            TimeAndDate();
            DetectBatteryPresence();
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
            UpdateNetworkStatus();
            Battery.AggregateBattery.ReportUpdated += AggregateBattery_ReportUpdated;
            InitNotifcation();
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
        private async void NetworkInformation_NetworkStatusChanged(object sender)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                UpdateNetworkStatus();
            });
        }
        private string[] wifiIcons = { "\uE871", "\uE872", "\uE873", "\uE874", "\uE701" };
        private string[] dataIcons = { "\uEC37", "\uEC38", "\uEC39", "\uEC3A", "\uEC3B" };
        private void UpdateNetworkStatus()
        {
            if (NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable)
            {
                switch (NetworkHelper.Instance.ConnectionInformation.ConnectionType)
                {
                    case ConnectionType.Ethernet:
                        WifiStatus.Text = "\uE839";
                        connectionStatus = 1;
                        break;
                    case ConnectionType.WiFi:
                        int WifiSignalBars = NetworkHelper.Instance.ConnectionInformation.SignalStrength.GetValueOrDefault(0);
                        WifiStatus.Text = wifiIcons[WifiSignalBars];
                        connectionStatus = 2;
                        break;
                    case ConnectionType.Data:
                        int DataSignalBars = NetworkHelper.Instance.ConnectionInformation.SignalStrength.GetValueOrDefault(0);
                        WifiStatus.Text = dataIcons[DataSignalBars];
                        connectionStatus = 2;
                        break;
                    case ConnectionType.Unknown:
                    default:
                        WifiStatus.Text = "\uE774";
                        connectionStatus = 3;
                        break;
                }
                ActionCenterHome.connected = true;
            }
            else
            {
                WifiStatus.Text = "\uEB55";
                connectionStatus = 0;
                ActionCenterHome.connected = false;
            }
        }
        #endregion

        #region Battery
        private void DetectBatteryPresence()
        {
            var aggDetectBattery = Battery.AggregateBattery;
            var report = aggDetectBattery.GetReport();
            string ReportResult = report.Status.ToString();
            if (ReportResult == "NotPresent")
            {
                BattStatus.Visibility = Visibility.Collapsed;
                batteryActionCenterEnabled = false;
            }
            else
            {
                BattStatus.Visibility = Visibility.Visible;
                batteryActionCenterEnabled = true;
                reportRequested = true;
                AggregateBattery();
            }
        }

        string[] batteryIconsCharge = { "\uEBAE", "\uEBAC", "\uEBAD", "\uEBAE", "\uEBAF", "\uEBB0", "\uEBB1", "\uEBB2", "\uEBB3", "\uEBB4", "\uEBB5" };
        string[] batteryIcons = { "\uEBA0", "\uEBA1", "\uEBA2", "\uEBA3", "\uEBA4", "\uEBA5", "\uEBA6", "\uEBA7", "\uEBA8", "\uEBA9", "\uEBAA" };
        private void AggregateBattery()
        {
            var aggBattery = Battery.AggregateBattery;
            var report = aggBattery.GetReport();
            string charging = report.Status.ToString();
            double fullCharge = Convert.ToDouble(report.FullChargeCapacityInMilliwattHours);
            double currentCharge = Convert.ToDouble(report.RemainingCapacityInMilliwattHours);
            double battLevel = Math.Ceiling((currentCharge / fullCharge) * 100);
            batteryActionCenter = Math.Floor(battLevel).ToString() + "%";
            if (charging == "Charging" || charging == "Idle")
            {
                int indexCharge = (int)Math.Floor(battLevel / 10);
                BattStatus.Text = batteryIconsCharge[indexCharge];
            }
            else
            {
                int indexDischarge = (int)Math.Floor(battLevel / 10);
                BattStatus.Text = batteryIcons[indexDischarge];
            }
        }

        private void AggregateBattery_ReportUpdated(Battery sender, object args)
        {
            if (reportRequested)
            {
                var dispatcherQueue = CoreApplication.MainView.DispatcherQueue;
                dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                {
                    AggregateBattery();
                });
            }
        }
        #endregion

        #region Notifications
        public static UserNotificationListener notifListener = UserNotificationListener.Current;
        private async void InitNotifcation()
        {
            await notifListener.RequestAccessAsync();
            notifListener.NotificationChanged += NotifListener_NotificationChanged;
        }

        private async void NotifListener_NotificationChanged(UserNotificationListener sender, UserNotificationChangedEventArgs args)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Notifications.Management.UserNotificationListener"))
            {
                UserNotificationListenerAccessStatus accessStatus = await notifListener.RequestAccessAsync();
                switch (accessStatus)
                {
                    case UserNotificationListenerAccessStatus.Allowed:
                        IReadOnlyList<UserNotification> notifsToast = await notifListener.GetNotificationsAsync(NotificationKinds.Toast);
                        IReadOnlyList<UserNotification> notifsOther = await notifListener.GetNotificationsAsync(NotificationKinds.Unknown);
                        if (notifsToast.Count > 0 || notifsOther.Count > 0)
                        {
                            notifcationCount = notifsToast.Count;
                            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                NotifStatus.Visibility = Visibility.Visible;
                                foreach (UserNotification notifcation in notifsToast)
                                {
                                    System.Diagnostics.Debug.WriteLine(notifcation.AppInfo.DisplayInfo.DisplayName);
                                    System.Diagnostics.Debug.WriteLine(notifcation.Notification);
                                    System.Diagnostics.Debug.WriteLine(notifcation.CreationTime); 
                                    System.Diagnostics.Debug.WriteLine(notifcation.Id);
                                    System.Diagnostics.Debug.WriteLine("----------------");
                                };
                            });
                        }
                        else
                        {
                            notifcationCount = 0;
                            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                NotifStatus.Visibility = Visibility.Collapsed;
                            });
                        }
                        break;
                    case UserNotificationListenerAccessStatus.Denied:
                        NotifStatus.Visibility = Visibility.Collapsed;
                        ErrorDialog dialogD = new ErrorDialog("Notifcation access was denied. Please enable it in settings.");
                        await dialogD.ShowAsync();
                        break;
                    case UserNotificationListenerAccessStatus.Unspecified:
                        ErrorDialog dialogU = new ErrorDialog("Notifcation access was denied. Please enable it in settings.");
                        await dialogU.ShowAsync();
                        break;
                }
            }
        }
        #endregion

        #region Bar events
        private void ActionCenterButton_Click(object sender, RoutedEventArgs e)
        {
            ActionCenterFrame.Navigate(typeof(ActionCenterHome));
        }

        private BitmapImage startColor = new BitmapImage(new Uri("ms-appx:///Assets/buttonIcons/startColor.png"));
        private BitmapImage startNormal = new BitmapImage(new Uri("ms-appx:///Assets/buttonIcons/startNormal.png"));
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            startLaunched = !startLaunched;
            if(startLaunched)
            {
                ColorTopLeft.Opacity = 1;
                ColorTopRight.Opacity = 1;
                ColorBottomLeft.Opacity = 1;
                ColorBottomRight.Opacity = 1;
                NormalTopLeft.Opacity = 0;
                NormalTopRight.Opacity = 0;
                NormalBottomLeft.Opacity = 0;
                NormalBottomRight.Opacity = 0;
            }
            else
            {
                ColorTopLeft.Opacity = 0;
                ColorTopRight.Opacity = 0;
                ColorBottomLeft.Opacity = 0;
                ColorBottomRight.Opacity = 0;
                NormalTopLeft.Opacity = 1;
                NormalTopRight.Opacity = 1;
                NormalBottomLeft.Opacity = 1;
                NormalBottomRight.Opacity = 1;
            }
        }
        #endregion
    }
}
