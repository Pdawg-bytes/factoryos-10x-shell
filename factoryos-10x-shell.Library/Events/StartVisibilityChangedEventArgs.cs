using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.Events
{
    public class StartVisibilityChangedEventArgs : EventArgs
    {
        public StartVisibilityChangedEventArgs(bool previousVisibility, bool currentVisibility) 
        {
            PreviousVisibility = previousVisibility;
            CurrentVisibility = currentVisibility;
        }

        public bool PreviousVisibility { get; set; }
        public bool CurrentVisibility { get; set;}
    }
}
