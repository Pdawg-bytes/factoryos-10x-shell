using factoryos_10x_shell.Library.Events;
using factoryos_10x_shell.Library.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Services.Managers
{
    internal class ActionCenterManagerService : IActionCenterManagerService
    {
        public ActionCenterManagerService()
        {
            IsActionCenterOpen = false;
        }

        private bool _isActionCenterOpen;
        public bool IsActionCenterOpen
        {
            get => _isActionCenterOpen;
            set => _isActionCenterOpen = value;
        }

        private bool _isToggleListExpanded;
        public bool IsToggleListExpanded
        {
            get => _isToggleListExpanded;
            set => _isToggleListExpanded = value;
        }


        public void RequestActionVisibilityChange(bool visible)
        {
            IsActionCenterOpen = visible;
            ActionVisibilityChanged?.Invoke(this, new ActionCenterVisibilityChangedEventArgs(!visible, visible));
        }

        public event EventHandler<ActionCenterVisibilityChangedEventArgs> ActionVisibilityChanged;
    }
}