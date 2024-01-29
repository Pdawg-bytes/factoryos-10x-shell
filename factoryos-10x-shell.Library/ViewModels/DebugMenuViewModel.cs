using CommunityToolkit.Mvvm.ComponentModel;
using factoryos_10x_shell.Library.Services.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.System.Diagnostics;

namespace factoryos_10x_shell.Library.ViewModels
{
    public partial class DebugMenuViewModel : ObservableObject
    {
        private Timer _diagnosticsRefreshTimer;

        private readonly IDispatcherService m_dispatcherService;

        public DebugMenuViewModel(
            IDispatcherService dispatcherService
            )
        {
            m_dispatcherService = dispatcherService;

            _diagnosticsRefreshTimer = new Timer();
            _diagnosticsRefreshTimer.Interval = 1000;
            _diagnosticsRefreshTimer.Elapsed += DiagnosticsRefreshTimer_Elapsed;
            _diagnosticsRefreshTimer.Start();
        }

        [ObservableProperty]
        private string workingSet;

        [ObservableProperty]
        private string pageCount;

        private void DiagnosticsRefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ProcessMemoryUsageReport memoryUsageRun = ProcessDiagnosticInfo.GetForCurrentProcess().MemoryUsage.GetReport();
            m_dispatcherService.DispatcherQueue.TryEnqueue(() =>
            {
                WorkingSet = (memoryUsageRun.WorkingSetSizeInBytes / 1024 / 1024).ToString() + " MB";
                PageCount = memoryUsageRun.PrivatePageCount.ToString("N0");
            });
        }
    }
}
