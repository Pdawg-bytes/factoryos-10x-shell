using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace factoryos_10x_shell.Library.Services.Navigation
{
    public enum DesktopPageType
    {
        RootContentDesktop,
        RootContentLockScreen
    }

    public interface IDesktopNavigator
    {
        public void DesktopNavigate();

        public Frame FrameContext { get; set; }
    }
}
