using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using factoryos_10x_shell.Library.Constants;
using factoryos_10x_shell.Library.Models.InternalData;
using factoryos_10x_shell.Library.Services.Environment;
using factoryos_10x_shell.Library.Services.Hardware;
using factoryos_10x_shell.Library.Services.Helpers;
using factoryos_10x_shell.Library.Services.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace factoryos_10x_shell.Library.ViewModels
{
    public partial class ActionCenterHomeViewModel : ObservableObject
    {
        private readonly IDispatcherService m_dispatcherService;
        private readonly IAppHelper m_appHelper;

        private readonly IBatteryService m_powerService;
        private readonly INetworkService m_netService;
        private readonly IBluetoothService m_btService;

        private readonly IDialogService m_dialogService;

        private readonly INotificationManager m_notifManager;
        private readonly IActionCenterManagerService m_actionManager;

        public ObservableCollection<UserNotificationModel> NotificationModels { get; set; }
        private Size _logoSize;

        public ActionCenterHomeViewModel(
            IBatteryService powerService,
            IDispatcherService dispatcherService,
            INotificationManager notifManager,
            IActionCenterManagerService actionManager,
            IDialogService dialogService,
            IAppHelper appHelper,
            INetworkService netService,
            IBluetoothService btService) 
        {
            m_powerService = powerService;
            m_dispatcherService = dispatcherService;
            m_notifManager = notifManager;
            m_actionManager = actionManager;
            m_dialogService = dialogService;
            m_appHelper = appHelper;
            m_netService = netService;
            m_btService = btService;

            m_powerService.BatteryStatusChanged += PowerService_BatteryStatusChanged;

            _logoSize = new Size(64, 64);
            NotificationModels = new ObservableCollection<UserNotificationModel>();
            m_notifManager.NotificationChanged += NotificationManager_NotificationChanged;
            Task.Run(UpdateNotifications).Wait();

            m_netService.InternetStatusChanged += NetworkService_InternetStatusChanged;
            UpdateNetworkStatus();

            ToggleSectionHeight = 100;
            ExpanderText = "\uE70E";
        }


        [RelayCommand]
        private void PowerOptionsClicked() { m_dialogService.OpenPowerDialog(); }
        [RelayCommand]
        private void LockScreenClicked() { m_dialogService.OpenLockDialog(); }
        [RelayCommand]
        private async void SettingsClicked() { await Launcher.LaunchUriAsync(new Uri("ms-settings:")); }
        [RelayCommand]
        private void OSKClicked() { /* todo: IMPLEMENT OPENING OSK */ }


        #region Toggle expander
        [RelayCommand]
        private void ToggleExpanderClicked()
        {
            if (m_actionManager.IsToggleListExpanded = !m_actionManager.IsToggleListExpanded)
            {
                ToggleSectionHeight = 278;
                ExpanderText = "\uE70D";
            }
            else
            {
                ToggleSectionHeight = 100;
                ExpanderText = "\uE70E";
            }
        }
        [ObservableProperty]
        private int toggleSectionHeight;

        [ObservableProperty]
        private string expanderText;
        #endregion


        #region Notification menu
        private void NotificationManager_NotificationChanged(object sender, UserNotificationChangedEventArgs e)
        {
            UpdateNotifications().Wait();
        }
        private async Task UpdateNotifications()
        {
            IReadOnlyList<UserNotification> notifsToast = await m_notifManager.NotificationListener.GetNotificationsAsync(NotificationKinds.Toast);
            IReadOnlyList<UserNotification> notifsOther = await m_notifManager.NotificationListener.GetNotificationsAsync(NotificationKinds.Unknown);

            m_dispatcherService.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
            {
                NotifWindowVisibility = notifsToast.Count > 0 || notifsOther.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
                NotifCount = notifsToast.Count;

                if (notifCount == 0)
                {
                    NotificationModels.Clear();
                }
            });
        }
        [ObservableProperty]
        private int notifCount;
        [RelayCommand]
        private void ClearNotifications()
        {
            m_notifManager.ClearUserNotifications();
        }
        [ObservableProperty]
        private Visibility notifWindowVisibility;

        public ObservableCollection<UserNotificationModel> Notifications => m_notifManager.UserNotifications;
        #endregion


        #region Battery status
        [RelayCommand]
        private async void BatteryStatusClicked()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-settings:batterysaver"));
        }
        private void PowerService_BatteryStatusChanged(object sender, EventArgs e)
        {
            m_dispatcherService.DispatcherQueue.TryEnqueue(() =>
            {
                BatteryStatusText = m_powerService.GetStatusReport().ChargePercentage.ToString() + "%";
            });
        }
        public Visibility BatteryStatusVisibility
        {
            get
            {
                if (m_powerService.IsBatteryInstalled) { return Visibility.Visible; }
                else { return Visibility.Collapsed; }
            }
        }
        [ObservableProperty]
        private string batteryStatusText;
        #endregion


        #region Network status
        [ObservableProperty]
        private string networkStatusCharacter;

        [ObservableProperty]
        private string networkName;

        [ObservableProperty]
        private bool networkIsConnected;

        private void NetworkService_InternetStatusChanged(object sender, EventArgs e)
        {
            UpdateNetworkStatus();
        }
        private void UpdateNetworkStatus()
        {
            string statusTextBuf = "\uE774";
            if (m_netService.IsInternetAvailable)
            {
                switch (m_netService.InternetType)
                {
                    case InternetConnection.Wired:
                        statusTextBuf = "\uE839";
                        break;
                    case InternetConnection.Wireless:
                        statusTextBuf = "\uE701";
                        break;
                    case InternetConnection.Data:
                        statusTextBuf = "\uEC3B";
                        break;
                    case InternetConnection.Unknown:
                    default:
                        statusTextBuf = "\uE774";
                        break;
                }
            }
            else
            {
                statusTextBuf = "\uEB55";
            }
            m_dispatcherService.DispatcherQueue.TryEnqueue(() =>
            {
                if (m_netService.IsInternetAvailable) 
                { 
                    NetworkName = m_netService.ConnectionName;
                    //NetworkIsConnected = true;
                } 
                else 
                { 
                    NetworkName = "Not connected";
                    //NetworkIsConnected = false;
                }

                NetworkIsConnected = m_netService.IsAdapterEnabled;

                NetworkStatusCharacter = statusTextBuf;
            });
        }
        #endregion

        public bool IsBluetoothEnabled => m_btService.IsBluetoothEnabled;
    }
}