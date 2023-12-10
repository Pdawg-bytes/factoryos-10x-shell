using factoryos_10x_shell.Library.Services.Environment;
using Windows.System;

namespace factoryos_10x_shell.Services.Environment
{
    public class DispatcherService : IDispatcherService
    {
        public DispatcherService()
        {
            DispatcherQueue = DispatcherQueue.GetForCurrentThread();
        }

        public DispatcherQueue DispatcherQueue { get; set; }
    }
}
