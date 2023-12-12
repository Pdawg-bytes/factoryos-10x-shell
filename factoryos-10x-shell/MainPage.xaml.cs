using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using factoryos_10x_shell.Controls;
using System;
using factoryos_10x_shell.Controls.DesktopControls;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Input;
using Windows.UI.Xaml.Media;
using factoryos_10x_shell.Library.Services.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace factoryos_10x_shell
{
    public sealed partial class MainPage : Page
    {
        public static Frame DesktopFrameP { get; private set; }

        private readonly IStartManagerService m_startManager;

        public MainPage()
        {
            this.InitializeComponent();

            m_startManager = App.ServiceProvider.GetRequiredService<IStartManagerService>();

            // Titlebar Stuff
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;

            // Init frame
            DesktopFrameP = DesktopFrame;
            DesktopFrameP.Navigate(typeof(MainDesktop));

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.LeftWindows || args.VirtualKey == Windows.System.VirtualKey.RightWindows)
            {
                Default10xBar.startLaunched = !Default10xBar.startLaunched;
                Default10xBar.SetStartColorOpacity();

                if (Default10xBar.startLaunched)
                {
                    MainDesktop.OpenStartStoryboard.Begin();
                }
                else
                {
                    MainDesktop.CloseStartStoryboard.Begin();
                }
            }
        }
    }
}
