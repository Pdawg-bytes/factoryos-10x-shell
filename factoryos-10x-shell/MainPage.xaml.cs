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

            CoreApplication.GetCurrentView().CoreWindow.PointerPressed += MainPage_PointerPressed;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void MainPage_PointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            if (Default10xBar.startLaunched)
            {
                PointerPoint point = args.CurrentPoint;

                Rect startMenuBounds = MainDesktop.StartFrameP.TransformToVisual(null).TransformBounds(new Rect(0, 0, MainDesktop.StartFrameP.ActualWidth, MainDesktop.StartFrameP.ActualHeight));

                if (!startMenuBounds.Contains(point.Position))
                {
                    if (!IsStartButtonPressed(point.Position))
                    {
                        Default10xBar.startLaunched = false;
                        Default10xBar.SetStartColorOpacity();
                        MainDesktop.CloseStartStoryboard.Begin();
                    }
                }
            }
        }

        private bool IsStartButtonPressed(Point point)
        {
            GeneralTransform transform = Default10xBar.StartButtonP.TransformToVisual(null);
            Rect startButtonBounds = transform.TransformBounds(new Rect(0, 0, Default10xBar.StartButtonP.ActualWidth, Default10xBar.StartButtonP.ActualHeight));
            return startButtonBounds.Contains(point);
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
