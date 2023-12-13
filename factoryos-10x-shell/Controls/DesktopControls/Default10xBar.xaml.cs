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
using Windows.Media.Core;
using Microsoft.Extensions.DependencyInjection;
using factoryos_10x_shell.Library.ViewModels;

namespace factoryos_10x_shell.Controls
{
    public sealed partial class Default10xBar : Page
    {
        public Default10xBar()
        {
            this.InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<Default10xBarViewModel>();
        }

        public Default10xBarViewModel ViewModel => (Default10xBarViewModel)this.DataContext;

        #region Bar events
        private void ActionCenterButton_Click(object sender, RoutedEventArgs e)
        {
            ActionCenterFrame.Navigate(typeof(ActionCenterHome));
        }
        #endregion
    }
}
