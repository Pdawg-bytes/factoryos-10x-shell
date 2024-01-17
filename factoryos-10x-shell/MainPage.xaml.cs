using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using factoryos_10x_shell.Controls;
using System;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Input;
using Windows.UI.Xaml.Media;
using factoryos_10x_shell.Library.Services.Managers;
using Microsoft.Extensions.DependencyInjection;
using factoryos_10x_shell.Library.Services.Navigation;
using factoryos_10x_shell.Views;
using Windows.System;
using System.Diagnostics;

namespace factoryos_10x_shell
{
    public sealed partial class MainPage : Page
    {
        private readonly IStartManagerService m_startManager;
        private readonly IDesktopNavigator m_desktopNavigator;

        private readonly DebugMenu m_debugMenu;

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
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += Dispatcher_AcceleratorKeyActivated;

            m_debugMenu = new DebugMenu();
        }

        private void Dispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            if (args.EventType.ToString().Contains("Down"))
            {
                var ctrl = Window.Current.CoreWindow.GetKeyState(VirtualKey.Control);
                if (ctrl.HasFlag(CoreVirtualKeyStates.Down))
                {
                    switch (args.VirtualKey)
                    {
                        case VirtualKey.Insert:
                            m_debugMenu.Hide();
                            m_debugMenu.ShowAsync();
                            break;
                    }
                }
            }
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.LeftWindows || args.VirtualKey == Windows.System.VirtualKey.RightWindows)
            {
                m_startManager.RequestStartVisibilityChange(!m_startManager.IsStartOpen);
            }

            var ctrl = Window.Current.CoreWindow.GetKeyState(VirtualKey.Control);
            if (ctrl.HasFlag(CoreVirtualKeyStates.Down) && args.VirtualKey == VirtualKey.NumberKeyLock)
            {
            }
        }
    }
}
