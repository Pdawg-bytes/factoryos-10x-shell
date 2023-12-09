using factoryos_10x_shell.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Management.Deployment;
using Windows.Media.Playback;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace factoryos_10x_shell
{
    sealed partial class App : Application
    {
        public static MediaPlayer MediaPlayer;

        private List<StartIconModel> _icons;
        public static ObservableCollection<StartIconModel> StartIcons;

        private Size _logoSize;
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;

            MediaPlayer = BackgroundMediaPlayer.Current;

            _icons = new List<StartIconModel>();
            _logoSize = new Size(50, 50);
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            await LoadAppsAsync();
            List<StartIconModel> sortedIcons = new List<StartIconModel>(_icons.OrderBy(icon => icon.IconName));
            StartIcons = new ObservableCollection<StartIconModel>(sortedIcons);

            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {

                }
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        private async Task LoadAppsAsync()
        {
            try
            {
                PackageManager packageManager = new PackageManager();
                IEnumerable<Package> packages = packageManager.FindPackagesForUser("");

                RandomAccessStreamReference logoData;

                foreach (Package package in packages)
                {
                    if (!package.IsFramework && !package.IsResourcePackage && !package.IsStub && package.GetAppListEntries().FirstOrDefault() != null)
                    {
                        try
                        {
                            IReadOnlyList<AppListEntry> entries = package.GetAppListEntries();
                            foreach (AppListEntry entry in entries)
                            {
                                logoData = null;
                                logoData = package.GetLogoAsRandomAccessStreamReference(_logoSize);

                                IRandomAccessStreamWithContentType stream = await logoData.OpenReadAsync();
                                BitmapImage bitmapImage = new BitmapImage();
                                await bitmapImage.SetSourceAsync(stream);

                                _icons.Add(new StartIconModel { IconName = entry.DisplayInfo.DisplayName, AppId = entry.AppUserModelId, IconSource = bitmapImage });
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error accessing logo for package {package.Id.FullName}: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LoadApps => Get: " + ex.Message);
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
