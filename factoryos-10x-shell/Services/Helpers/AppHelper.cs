﻿using factoryos_10x_shell.Library.Models.InternalData;
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

        public AppHelper() 
        {
            m_dispatcherQueue = DispatcherQueue.GetForCurrentThread();

            _logoSize = new Size(48, 48);
            StartIcons = new ObservableCollection<StartIconModel>();
            LoadAppsAsync().Wait();

            List<StartIconModel> sortedIcons = new List<StartIconModel>(StartIcons.OrderBy(icon => icon.IconName));
            StartIcons = new ObservableCollection<StartIconModel>(sortedIcons);
        }

        private async Task LoadAppsAsync()
        {
            m_dispatcherQueue.TryEnqueue(async () =>
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
            });
        }
    }
}
