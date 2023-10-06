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

namespace factoryos_10x_shell.Controls.DesktopControls
{
    public sealed partial class StartMenu : Page
    {
        private bool _appListExpanded;

        public ObservableCollection<StartIconModel> StartIconCollection;
        public StartMenu()
        {
            this.InitializeComponent();

            StartIconCollection = new ObservableCollection<StartIconModel>();

            try
            {
                PackageManager packageManager = new PackageManager();
                IEnumerable<Package> packages = packageManager.FindPackagesForUser("");
                string iconPath;
                string iconName;
                foreach (Package package in packages)
                {
                    if (!package.IsFramework && !package.IsResourcePackage && !package.IsStub)
                    {
                        try
                        {
                            /*using (FileStream stream = new FileStream(iconPath, FileMode.Open, FileAccess.Read))
                            {
                            }*/
                            iconPath = string.Empty;
                            iconPath = Uri.UnescapeDataString(Uri.UnescapeDataString(package.Logo.AbsolutePath)).Replace("/", "\\");

                            iconName = string.Empty;
                            iconName = package.DisplayName;
                            StartIconCollection.Add(new StartIconModel { IconName = iconName, AppId = package.Id.FamilyName, IconPath = iconPath });
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error accessing logo for package {package.Id.FullName}: {ex.Message}");
                        }
                    }
                }
                ObservableCollection<StartIconModel> sortedPackageInfos = new ObservableCollection<StartIconModel>(StartIconCollection.OrderBy(info => info.IconName));
                StartIconCollection = sortedPackageInfos;
                Debug.WriteLine("t");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LoadBitmapFromUwpIcon => Get: " + ex.Message);
            }
        }

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
            if (_appListExpanded )
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
