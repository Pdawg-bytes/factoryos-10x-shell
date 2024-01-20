using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace factoryos_10x_shell.Library.Models.InternalData
{
    public class UserNotificationModel : INotifyPropertyChanged
    {
        public UserNotificationModel() { }

        private string _appName;
        public string AppName
        {
            get => _appName;
            set
            {
                _appName = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _iconSource;
        public BitmapImage IconSource
        {
            get => _iconSource;
            set
            {
                if (_iconSource != value)
                {
                    _iconSource = value;
                    OnPropertyChanged();
                }
            }
        }

        private uint _notificationId;
        public uint NotificationId
        {
            get => _notificationId;
            set
            {
                if (_notificationId != value)
                {
                    _notificationId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _notificationTime;
        public string NotificationTime
        {
            get => _notificationTime;
            set
            {
                if (_notificationTime != value)
                {
                    _notificationTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _notificationMainContent;
        public string NotificationMainContent
        {
            get => _notificationMainContent;
            set
            {
                if (_notificationMainContent != value)
                {
                    _notificationMainContent = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _notificationSecondaryContent;
        public string NotificationSecondaryContent
        {
            get => _notificationSecondaryContent;
            set
            {
                if (_notificationSecondaryContent != value)
                {
                    _notificationSecondaryContent = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
