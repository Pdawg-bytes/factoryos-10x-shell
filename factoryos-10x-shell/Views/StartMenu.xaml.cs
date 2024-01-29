using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Management.Deployment;
using Windows.Networking.Proximity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System.UserProfile;
using System.Collections.ObjectModel;

using static factoryos_10x_shell.Helpers.VisualHelper;
using Windows.Storage.Streams;
using static System.Net.Mime.MediaTypeNames;
using Windows.UI.Xaml.Media.Imaging;
using Windows.System;
using Microsoft.Extensions.DependencyInjection;
using factoryos_10x_shell.Library.ViewModels;
using factoryos_10x_shell.Library.Models.InternalData;
using Windows.ApplicationModel.Core;

namespace factoryos_10x_shell.Views
{
    public sealed partial class StartMenu : Page
    {
        public StartMenu()
        {
            this.InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<StartMenuViewModel>();
        }

        public StartMenuViewModel ViewModel => (StartMenuViewModel)this.DataContext;

        private async void AppButton_Click(object sender, RoutedEventArgs e)
        {
            StartIconModel model = ((FrameworkElement)sender).DataContext as StartIconModel;
            if (model != null)
            {
                await (model.Data as AppListEntry).LaunchAsync();
            }
        }
    }
}