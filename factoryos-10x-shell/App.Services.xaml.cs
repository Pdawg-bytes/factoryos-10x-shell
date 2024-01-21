using factoryos_10x_shell.Library.Services.Environment;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using factoryos_10x_shell.Services.Environment;
using factoryos_10x_shell.Library.ViewModels;
using factoryos_10x_shell.Library.Services.Hardware;
using factoryos_10x_shell.Services.Hardware;
using factoryos_10x_shell.Library.Services.Managers;
using factoryos_10x_shell.Services.Managers;
using factoryos_10x_shell.Library.Services.Helpers;
using factoryos_10x_shell.Services.Helpers;
using factoryos_10x_shell.Library.Services.Navigation;
using factoryos_10x_shell.Services.Navigation;

namespace factoryos_10x_shell
{
    partial class App
    {
        private IServiceProvider m_serviceProvider;
        public static IServiceProvider ServiceProvider
        {
            get
            {
                IServiceProvider serviceProvider = (Current as App).m_serviceProvider ??
                    throw new InvalidOperationException("Service provider was not initialized before accessing.");

                return serviceProvider;
            }
        }

        private void ConfigureServices()
        {
            IServiceCollection collection = new ServiceCollection()
                .AddSingleton<ITimeService, TimeService>()
                .AddSingleton<IAppHelper, AppHelper>()
                .AddSingleton<IDispatcherService, DispatcherService>()
                .AddSingleton<IBatteryService, BatteryService>()
                .AddSingleton<INetworkService, NetworkService>()
                .AddSingleton<INotificationManager, NotificationManager>()
                .AddSingleton<IThemeService, ThemeService>()
                .AddSingleton<IStartManagerService, StartManagerService>()
                .AddSingleton<IActionCenterManagerService, ActionCenterManagerService>()
                .AddSingleton<IDesktopNavigator, DesktopNavigator>()
                .AddSingleton<IEnvironmentService, EnvironmentService>()
                .AddTransient<IPinManagerService, PinManagerService>()
                .AddSingleton<IDialogService, DialogService>()
                .AddSingleton<IBluetoothService, BluetoothService>()
                .AddTransient<Default10xBarViewModel>()
                .AddTransient<MainDesktopViewModel>()
                .AddTransient<StartMenuViewModel>()
                .AddTransient<ActionCenterHomeViewModel>()
                .AddTransient<LockDialogViewModel>()
                .AddTransient<LockScreenViewModel>()
                .AddTransient<PowerDialogViewModel>()
                .AddTransient<DebugMenuViewModel>();

            m_serviceProvider = collection.BuildServiceProvider(true);
        }

        private void PreloadServices()
        {
            _ = m_serviceProvider.GetRequiredService<IDispatcherService>();
        }
    }
}
