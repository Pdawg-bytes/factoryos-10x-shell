using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Helpers
{
    public class ProcessStart
    {
        public static ProcessStartInfo ProcessStartEx(string procName, bool createNoWindow, bool useShellEx)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();

            if (procName != null)
            {
                startInfo.CreateNoWindow = createNoWindow;
                startInfo.FileName = System.IO.Path.Combine(Environment.SystemDirectory, procName);

                return startInfo;
            }
            else
            {
                return null;
            }
        }
    }
}
