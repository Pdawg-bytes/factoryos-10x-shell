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
using Windows.System;

namespace factoryos_10x_shell.Services.Helpers
{
    internal class AppHelper : IAppHelper
    {
        private DispatcherQueue m_dispatcherQueue;

        private Size _logoSize;
        public ObservableCollection<StartIconModel> StartIcons { get; set; }
        private List<StartIconModel> _iconCache { get; set; }

        public PackageManager PackageManager { get; set; }

        private Dictionary<string, string> m_pkgFamilyMap;

        public AppHelper() 
        {
            m_dispatcherQueue = DispatcherQueue.GetForCurrentThread();
            PackageManager = new PackageManager();
            m_pkgFamilyMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            _logoSize = new Size(176, 176);
            _iconCache = new List<StartIconModel>();
        }

        public async Task LoadAppsAsync()
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
                        m_pkgFamilyMap[package.Id.FamilyName] = package.Id.FullName;
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
                                _iconCache.Add(new StartIconModel { IconName = entry.DisplayInfo.DisplayName, AppId = entry.AppUserModelId, IconSource = bitmapImage });
                                if (entry.AppUserModelId.Contains("Visualizer"))
                                {
                                    Debug.WriteLine(package.Id.FullName);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error accessing logo for package {package.Id.FullName}: {ex.Message}");
                        }
                    }
                }

                StartIcons = new ObservableCollection<StartIconModel>(_iconCache.OrderBy(icon => icon.IconName));
                _iconCache = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("LoadApps => Get: " + ex.Message);
            }
        }

        public Package PackageFromAumid(string aumid)
        {
            string[] aumidParts = aumid.Split('!');
            string packageFamilyName = aumidParts[0];

            if (m_pkgFamilyMap.TryGetValue(packageFamilyName, out string packageFullName))
            {
                return PackageManager.FindPackageForUser(string.Empty, packageFullName);
            }
            return null;
        }
    }
}
