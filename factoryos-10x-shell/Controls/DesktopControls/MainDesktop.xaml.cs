using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace factoryos_10x_shell.Controls.DesktopControls
{
    public sealed partial class MainDesktop : Page
    {
        public static Frame TaskbarFrameP { get; private set; }

        public MainDesktop()
        {
            this.InitializeComponent();

            // Frame init
            TaskbarFrameP = TaskbarFrame;
            TaskbarFrameP.Navigate(typeof(Default10xBar));
        }
    }
}
