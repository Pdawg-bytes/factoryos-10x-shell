using factoryos_10x_shell.Library.Services.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Services.Environment
{
    internal class EnvironmentService : IEnvironmentService
    {
        public EnvironmentService()
        {
            MachineName = System.Environment.MachineName;
        }

        public string MachineName { get; set; }
    }
}
