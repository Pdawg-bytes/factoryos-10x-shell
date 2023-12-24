using factoryos_10x_shell.Library.Services.Navigation;
using factoryos_10x_shell.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace factoryos_10x_shell.Services.Navigation
{
    internal class DesktopNavigator : IDesktopNavigator
    {
        public DesktopNavigator()
        {
        }

        public Frame FrameContext { get; set; }

        public void DesktopNavigate(DesktopPageType pageType)
        {
            Type navType = pageType switch
            {
                DesktopPageType.RootContentLockScreen => typeof(LockScreen),
                DesktopPageType.RootContentDesktop => typeof(MainDesktop)
            };

            if (FrameContext == null)
            {
                throw new InvalidOperationException("Frame context was accessed before initialization.");
            }

            FrameContext.Navigate(navType);
        }
    }
}
