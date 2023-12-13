using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using factoryos_10x_shell.Library.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace factoryos_10x_shell.Library.ViewModels
{
    public partial class StartMenuViewModel : ObservableObject
    {
        private readonly IStartManagerService m_startManager;


        public StartMenuViewModel(IStartManagerService startManager) 
        {
            m_startManager = startManager;

            AppsListGridHeight = 310;
            AppsListToggleContent = "Show all";
        }


        [ObservableProperty]
        private ScrollMode appGridViewVerticalScrollMode;

        [ObservableProperty]
        private ScrollMode appGridViewHorizontalScrollMode;

        [ObservableProperty]
        private ScrollBarVisibility appGridViewScrollVisibility;

        [ObservableProperty]
        private int appsListGridHeight;

        [ObservableProperty]
        private string appsListToggleContent;

        public event EventHandler ScrollAppsToTopRequested;

        [RelayCommand]
        public void AppsListToggleClicked()
        {
            m_startManager.IsAppsListExpanded = !m_startManager.IsAppsListExpanded;
            bool expanded = m_startManager.IsAppsListExpanded;

            AppGridViewVerticalScrollMode = ScrollMode.Disabled;
            AppGridViewHorizontalScrollMode = ScrollMode.Disabled;

            AppsListToggleContent = expanded ? "Show less" : "Show all";
            if (expanded)
            {
                AppsListGridHeight = 480;
                AppGridViewVerticalScrollMode = ScrollMode.Enabled;
                AppGridViewScrollVisibility = ScrollBarVisibility.Auto;
            }
            else
            {
                AppsListGridHeight = 310;
                AppGridViewVerticalScrollMode = ScrollMode.Disabled;
                AppGridViewHorizontalScrollMode = ScrollMode.Disabled;
                AppGridViewScrollVisibility = ScrollBarVisibility.Hidden;

                ScrollAppsToTopRequested?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
