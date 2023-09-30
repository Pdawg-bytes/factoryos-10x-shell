using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using factoryos_10x_shell.Controls;
using System;
using factoryos_10x_shell.Controls.DesktopControls;

namespace factoryos_10x_shell
{
    public sealed partial class MainPage : Page
    {
        public static Frame DesktopFrameP { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();

            // Titlebar Stuff
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;

            // Init frame
            DesktopFrameP = DesktopFrame;
            DesktopFrameP.Navigate(typeof(MainDesktop));
        }
    }
}
