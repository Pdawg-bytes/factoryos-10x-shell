using factoryos_10x_shell.Controls.DesktopControls;
using factoryos_10x_shell.Controls.LockControls;
using factoryos_10x_shell.Library.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace factoryos_10x_shell.Services.Navigation
{
    internal class DesktopNavigator
    {
        private readonly IDesktopNavigator m_desktopNavigator;

        public DesktopNavigator(IDesktopNavigator desktopNavigator)
        {
            m_desktopNavigator = desktopNavigator;
        }

        public Frame FrameContext { get; set; }

        public void DesktopNavigate(DesktopPageType pageType)
        {
            Type navType = pageType switch
            {
                DesktopPageType.RootContentLockScreen => typeof(LockScreen),
                DesktopPageType.RootContentDesktop => typeof(MainDesktop)
            };

            FrameContext.Navigate(navType);
        }
    }
}
