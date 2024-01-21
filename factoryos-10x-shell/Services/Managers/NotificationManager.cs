using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Notifications.Management;
using Windows.UI.Notifications;
using factoryos_10x_shell.Library.Services.Managers;
using factoryos_10x_shell.Library.Models.InternalData;
using System.Collections.ObjectModel;
using Windows.Foundation;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Windows.ApplicationModel;
using Windows.System;
using factoryos_10x_shell.Library.Services.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Windows.ApplicationModel.Core;

namespace factoryos_10x_shell.Services.Managers
{
    internal class NotificationManager : INotificationManager, IDisposable
    {
        public UserNotificationListener NotificationListener { get; set; }
        public UserNotificationListenerAccessStatus NotificationAccessStatus { get; set; }

        public ObservableCollection<UserNotificationModel> UserNotifications { get; set; }

        private readonly IAppHelper m_appHelper;
        private readonly Size _logoSize;
        private DispatcherQueue _dispatcherQueue;

        public NotificationManager()
        {
            if (!ApiInformation.IsTypePresent("Windows.UI.Notifications.Management.UserNotificationListener"))
            {
                NotificationAccessStatus = UserNotificationListenerAccessStatus.Unspecified;
                return;
            }

            _logoSize = new Size(64, 64);
            m_appHelper = App.ServiceProvider.GetRequiredService<IAppHelper>();
            _dispatcherQueue = DispatcherQueue.GetForCurrentThread();
            UserNotifications = new ObservableCollection<UserNotificationModel>();

            NotificationListener = UserNotificationListener.Current;
            if (Task.Run(async () => await InitializeEventAsync()).Result)
            {
                NotificationListener.NotificationChanged += NotificationManager_NotificationChanged;
            }
            else
            {
                Task.Run(RequestNotificationAccess).Wait();
            }
        }

        private async Task<bool> InitializeEventAsync()
        {
            UserNotificationListenerAccessStatus accessStatus = await NotificationListener.RequestAccessAsync();
            NotificationAccessStatus = accessStatus;
            return accessStatus == UserNotificationListenerAccessStatus.Allowed;
        }

        private async Task RequestNotificationAccess()
        {
            NotificationAccessStatus = await NotificationListener.RequestAccessAsync();
        }

        public event EventHandler<UserNotificationChangedEventArgs> NotificationChanged;

        private async void NotificationManager_NotificationChanged(UserNotificationListener sender, UserNotificationChangedEventArgs args)
        {
            UserNotification notif = NotificationListener.GetNotification(args.UserNotificationId);
            if (notif != null && args.ChangeKind == UserNotificationChangedKind.Added)
            {
                Package notifPackage = m_appHelper.PackageFromAumid(notif.AppInfo.AppUserModelId);
                IRandomAccessStreamWithContentType stream = await notifPackage.GetLogoAsRandomAccessStreamReference(_logoSize).OpenReadAsync();
                AppListEntry entry = notifPackage.GetAppListEntries().FirstOrDefault();
                _dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, async () =>
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(stream);

                    string mainContent = /*notif.Notification.Visual.Bindings?.FirstOrDefault().GetTextElements().FirstOrDefault()?.Text*/ "Fake title";
                    string secondaryContent = /*notif.Notification.Visual.Bindings?.FirstOrDefault().GetTextElements().Skip(1).FirstOrDefault()?.Text;*/ "Sorry! The WinRT Notifications API is currently broken, so we can't display content right now.";

                    UserNotifications.Add(new UserNotificationModel
                    {
                        AppName = entry.DisplayInfo.DisplayName,
                        IconSource = bitmapImage,
                        NotificationId = notif.Id,
                        NotificationTime = DateTime.Now.ToString("t"),
                        NotificationMainContent = mainContent,
                        NotificationSecondaryContent = secondaryContent
                    });
                });
            }
            else if (notif != null)
            {
                _dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
                {
                    UserNotifications.Remove(UserNotifications.First(param => param.NotificationId == notif.Id));
                });
            }
            NotificationChanged?.Invoke(this, args);
        }

        public void ClearUserNotifications()
        {
            NotificationListener.ClearNotifications();
        }


        public void Dispose()
        {
            NotificationListener.NotificationChanged -= NotificationManager_NotificationChanged;
        }
    }
}
