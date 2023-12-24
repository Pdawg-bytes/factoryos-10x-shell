using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.Services.Environment
{
    public interface IEnvironmentService
    {
        /// <summary>
        /// The name of the machine that is running the app.
        /// </summary>
        public string MachineName { get; set; }
    }
}
