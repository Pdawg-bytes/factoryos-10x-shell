using factoryos_10x_shell.Library.ViewModels;
using factoryos_10x_shell.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace factoryos_10x_shell.Views
{
    public sealed partial class ActionCenterHome : Page
    {
        public static bool connected;
        private bool isExpaneded = false;
        public ActionCenterHome()
        {
            this.InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<ActionCenterHomeViewModel>();
        }

        public ActionCenterHomeViewModel ViewModel => (ActionCenterHomeViewModel)this.DataContext;
    }
}
