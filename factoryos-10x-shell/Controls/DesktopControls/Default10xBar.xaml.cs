using System;
using System.Collections.Generic;
using Microsoft.Toolkit.Uwp.Connectivity;
using Windows.Devices.Power;
using Windows.System;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using factoryos_10x_shell.Controls.ActionCenterControls;
using Windows.Foundation.Metadata;
using Microsoft.Toolkit.Uwp.UI;
using Windows.UI.Notifications.Management;
using Windows.UI.Notifications;
using Windows.UI.Core;
using Windows.Networking.Connectivity;
using System.Linq;
using Microsoft.Toolkit.Uwp.UI.Helpers;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using factoryos_10x_shell.Controls.DesktopControls;
using Windows.UI.Xaml.Shapes;

namespace factoryos_10x_shell.Controls
{
    public sealed partial class Default10xBar : Page
    {
        public static string batteryActionCenter;

        bool reportRequested = false;
        public static bool batteryActionCenterEnabled;
        public static bool startLaunched = false;
        public static bool extStartLaunchFlag = false;

        public static int connectionStatus;
        public static int notifcationCount;

        private ThemeListener themeListener;

        private SolidColorBrush lightBrush;
        private SolidColorBrush darkBrush;

        public static Path ColorTopLeftP { get; private set; }
        public static Path ColorTopRightP { get; private set; }
        public static Path ColorBottomLeftP { get; private set; }
        public static Path ColorBottomRightP { get; private set; }
        public static Path NormalTopLeftP { get; private set; }
        public static Path NormalTopRightP { get; private set; }
        public static Path NormalBottomLeftP { get; private set; }
        public static Path NormalBottomRightP { get; private set; }

        public static Button StartButtonP { get; private set; }

        public Default10xBar()
        {
            this.InitializeComponent();
            ColorTopLeftP = ColorTopLeft;
            ColorTopRightP = ColorTopRight;
            ColorBottomLeftP = ColorBottomLeft;
            ColorBottomRightP = ColorBottomRight;
            NormalTopLeftP = NormalTopLeft;
            NormalTopRightP = NormalTopRight;
            NormalBottomLeftP = NormalBottomLeft;
            NormalBottomRightP = NormalBottomRight;
            StartButtonP = StartButton;

            // Init
            TimeAndDate();
            DetectBatteryPresence();
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
            UpdateNetworkStatus();
            Battery.AggregateBattery.ReportUpdated += AggregateBattery_ReportUpdated;
            InitNotifcation();

            lightBrush = new SolidColorBrush(Color.FromArgb(255, 99, 99, 98));
            darkBrush = new SolidColorBrush(Color.FromArgb(255, 120, 124, 126));

            themeListener = new ThemeListener();
            themeListener.ThemeChanged += ThemeListener_ThemeChanged;
            ThemeListener_ThemeChanged(themeListener);
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
            try
            {
                notifListener.NotificationChanged += NotifListener_NotificationChanged;
            }
            catch (Exception exCreate)
            {
                ErrorDialog dialogC = new ErrorDialog("Notifcation access was denied. Please enable it in settings.\n" + exCreate.Message);
                await dialogC.ShowAsync();
            }
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
                                foreach (UserNotification notification in notifsToast)
                                {
                                    System.Diagnostics.Debug.WriteLine(notification.AppInfo.DisplayInfo.DisplayName);
                                    NotificationBinding toastBinding = notification.Notification.Visual.GetBinding(KnownNotificationBindings.ToastGeneric);
                                    if (toastBinding != null)
                                    {
                                        IReadOnlyList<AdaptiveNotificationText> textElements = toastBinding.GetTextElements();
                                        string titleText = textElements.FirstOrDefault()?.Text;
                                        System.Diagnostics.Debug.WriteLine(titleText);
                                        string bodyText = string.Join("\n", textElements.Skip(1).Select(t => t.Text));
                                        System.Diagnostics.Debug.WriteLine(bodyText);
                                    }
                                    System.Diagnostics.Debug.WriteLine(notification.CreationTime.ToString("g")); 
                                    System.Diagnostics.Debug.WriteLine(notification.Id);
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

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            startLaunched = !startLaunched;
            SetStartColorOpacity();

            if (startLaunched)
            {
                MainDesktop.OpenStartStoryboard.Begin();
            }
            else
            {
                MainDesktop.CloseStartStoryboard.Begin();
            }
        }

        public static void SetStartColorOpacity()
        {
            if (startLaunched)
            {
                ColorTopLeftP.Opacity = 1;
                ColorTopRightP.Opacity = 1;
                ColorBottomLeftP.Opacity = 1;
                ColorBottomRightP.Opacity = 1;
                NormalTopLeftP.Opacity = 0;
                NormalTopRightP.Opacity = 0;
                NormalBottomLeftP.Opacity = 0;
                NormalBottomRightP.Opacity = 0;
            }
            else
            {
                ColorTopLeftP.Opacity = 0;
                ColorTopRightP.Opacity = 0;
                ColorBottomLeftP.Opacity = 0;
                ColorBottomRightP.Opacity = 0;
                NormalTopLeftP.Opacity = 1;
                NormalTopRightP.Opacity = 1;
                NormalBottomLeftP.Opacity = 1;
                NormalBottomRightP.Opacity = 1;
            }
        }
        #endregion

        #region Theming
        private void ThemeListener_ThemeChanged(ThemeListener sender)
        {
            var theme = sender.CurrentTheme;
            switch(theme)
            {
                case ApplicationTheme.Light:
                    NormalTopLeft.Fill = lightBrush;
                    NormalTopRight.Fill = lightBrush;
                    NormalBottomLeft.Fill = lightBrush;
                    NormalBottomRight.Fill = lightBrush;
                    ClockText.Foreground = lightBrush;
                    NotifStatus.Foreground = lightBrush;
                    BattStatus.Foreground = lightBrush;
                    WifiStatus.Foreground = lightBrush;
                    break;
                case ApplicationTheme.Dark:
                default:
                    NormalTopLeft.Fill = darkBrush;
                    NormalTopRight.Fill = darkBrush;
                    NormalBottomLeft.Fill = darkBrush;
                    NormalBottomRight.Fill = darkBrush;
                    ClockText.Foreground = darkBrush;
                    NotifStatus.Foreground = darkBrush;
                    BattStatus.Foreground = darkBrush;
                    WifiStatus.Foreground = darkBrush;
                    break;
            }
        }
        #endregion
    }
}
