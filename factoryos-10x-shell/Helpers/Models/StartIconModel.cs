using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace factoryos_10x_shell.Helpers.Models
{
    public class StartIconModel : INotifyPropertyChanged
    {
        private string _iconName;
        public string IconName
        {
            get { return _iconName; }
            set
            {
                if (_iconName != value)
                {
                    _iconName = value;
                    OnPropertyChanged();
                }
            }
        }

        private BitmapImage _iconSource;
        public BitmapImage IconSource
        {
            get { return _iconSource; }
            set
            {
                if (_iconSource != value)
                {
                    _iconSource = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _appId;
        public string AppId
        {
            get { return _appId; }
            set
            {
                if (_appId != value)
                {
                    _appId = value;
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
