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
using factoryos_10x_shell.Helpers.Models;
using System.Collections.ObjectModel;

using static factoryos_10x_shell.Helpers.VisualHelper;
using Windows.Storage.Streams;
using static System.Net.Mime.MediaTypeNames;
using Windows.UI.Xaml.Media.Imaging;
using Windows.System;
using Microsoft.Extensions.DependencyInjection;
using factoryos_10x_shell.Library.ViewModels;

namespace factoryos_10x_shell.Controls.DesktopControls
{
    public sealed partial class StartMenu : Page
    {
        private bool _appListExpanded;

        private ObservableCollection<StartIconModel> StartIconCollection;

        public StartMenu()
        {
            this.InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<StartMenuViewModel>();

            StartIconCollection = new ObservableCollection<StartIconModel>();
            StartIconCollection = App.StartIcons;

            AppListGrid.ItemsSource = StartIconCollection;
        }

        public StartMenuViewModel ViewModel => (StartMenuViewModel)this.DataContext;
       
        private void AppListShow_Click(object sender, RoutedEventArgs e)
        {
            _appListExpanded = !_appListExpanded;

            ScrollViewer gridScrollViewer = FindVisualChild<ScrollViewer>(AppListGrid);

            if (gridScrollViewer != null)
            {
                gridScrollViewer.VerticalScrollMode = ScrollMode.Disabled;
                gridScrollViewer.HorizontalScrollMode = ScrollMode.Disabled;
            }

            AppListShow.Content = _appListExpanded ? "Show less" : "Show all";
            if (_appListExpanded)
            {
                AppListGrid.Height = 480;
                gridScrollViewer.VerticalScrollMode = ScrollMode.Enabled;
                gridScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            else
            {
                AppListGrid.Height = 310;
                gridScrollViewer.VerticalScrollMode = ScrollMode.Disabled;
                gridScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
        }
    }
}
