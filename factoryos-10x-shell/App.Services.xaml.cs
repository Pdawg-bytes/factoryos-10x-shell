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
                .AddSingleton<IDispatcherService, DispatcherService>()
                .AddSingleton<IBatteryService, BatteryService>()
                .AddSingleton<INetworkService, NetworkService>()
                .AddTransient<Default10xBarViewModel>();

            m_serviceProvider = collection.BuildServiceProvider(true);
        }

        private void PreloadServices()
        {

        }
    }
}
