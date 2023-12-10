using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace factoryos_10x_shell.Library.Services.Environment
{
    /// <summary>
    /// Provides an abstraction of FOS-10x-Shell's internal dispatcher.
    /// </summary>
    public interface IDispatcherService
    {
        /// <summary>
        /// The dispatcher queue.
        /// </summary>
        DispatcherQueue DispatcherQueue { get; set; }
    }
}
