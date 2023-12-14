using factoryos_10x_shell.Library.Models.InternalData;
using factoryos_10x_shell.Library.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel;
using Windows.Management.Deployment;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using System.Collections.ObjectModel;
using factoryos_10x_shell.Library.Events;

namespace factoryos_10x_shell.Services.Helpers
{
    internal class AppHelper : IAppHelper
    {
        private Size _logoSize;

        private ObservableCollection<StartIconModel> StartIcons;

        public AppHelper() 
        {
            _logoSize = new Size(48, 48);
            LoadAppsAsync().Wait();

            List<StartIconModel> sortedIcons = new List<StartIconModel>(StartIcons.OrderBy(icon => icon.IconName));
            StartIcons = new ObservableCollection<StartIconModel>(sortedIcons);
        }

        public event EventHandler<AppsListUpdatedEventArgs> AppListUpdated;

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
                                logoData = entry.DisplayInfo.GetLogo(_logoSize);

                                IRandomAccessStreamWithContentType stream = await logoData.OpenReadAsync();
                                BitmapImage bitmapImage = new BitmapImage();
                                await bitmapImage.SetSourceAsync(stream);

                                StartIcons.Add(new StartIconModel { IconName = entry.DisplayInfo.DisplayName, AppId = entry.AppUserModelId, IconSource = bitmapImage });
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
    }
}
