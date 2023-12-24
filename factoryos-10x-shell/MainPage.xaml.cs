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
using factoryos_10x_shell.Library.Services.Navigation;

namespace factoryos_10x_shell
{
    public sealed partial class MainPage : Page
    {
        private readonly IStartManagerService m_startManager;
        private readonly IDesktopNavigator m_desktopNavigator;

        public MainPage()
        {
            this.InitializeComponent();

            m_startManager = App.ServiceProvider.GetRequiredService<IStartManagerService>();
            m_desktopNavigator = App.ServiceProvider.GetRequiredService<IDesktopNavigator>();

            // Titlebar
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;

            // Init frame
            m_desktopNavigator.FrameContext = DesktopFrame;
            m_desktopNavigator.DesktopNavigate(DesktopPageType.RootContentDesktop);

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.LeftWindows || args.VirtualKey == Windows.System.VirtualKey.RightWindows)
            {
                m_startManager.RequestStartVisibilityChange(!m_startManager.IsStartOpen);
            }
        }
    }
}
