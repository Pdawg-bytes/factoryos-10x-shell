using factoryos_10x_shell.Library.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace factoryos_10x_shell.Services.Managers
{
    internal class StartManagerService : IStartManagerService
    {
        public StartManagerService() 
        {
            IsStartOpen = false;
        }

        private bool _isStartOpen;
        public bool IsStartOpen
        {
            get => _isStartOpen; 
            set => _isStartOpen = value; 
        }

        public void RequestStartVisibilityChange(bool visible)
        {
            IsStartOpen = visible;
        }

        public event EventHandler StartVisibilityChanged;

        private Rect _currentStartBounds;
        public Rect CurrentStartBounds 
        {
            get => _currentStartBounds;
            set => _currentStartBounds = value;
        }
    }
}
