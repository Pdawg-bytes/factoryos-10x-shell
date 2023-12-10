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
        public static string batteryActionCenter;
        public static bool batteryActionCenterEnabled;

        public static bool startLaunched = false;
        public static bool extStartLaunchFlag = false;

        public static int connectionStatus;
        public static int notifcationCount;

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

            DataContext = App.ServiceProvider.GetRequiredService<Default10xBarViewModel>();

            ColorTopLeftP = ColorTopLeft;
            ColorTopRightP = ColorTopRight;
            ColorBottomLeftP = ColorBottomLeft;
            ColorBottomRightP = ColorBottomRight;
            NormalTopLeftP = NormalTopLeft;
            NormalTopRightP = NormalTopRight;
            NormalBottomLeftP = NormalBottomLeft;
            NormalBottomRightP = NormalBottomRight;
            StartButtonP = StartButton;
        }

        public Default10xBarViewModel ViewModel => (Default10xBarViewModel)this.DataContext;

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
    }
}
