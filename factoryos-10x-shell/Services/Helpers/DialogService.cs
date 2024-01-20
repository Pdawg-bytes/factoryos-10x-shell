using factoryos_10x_shell.Controls;
using factoryos_10x_shell.Library.Services.Helpers;
using factoryos_10x_shell.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Shell;

namespace factoryos_10x_shell.Services.Helpers
{
    // Mostly taken from Ambie: https://github.com/jenius-apps/ambie/blob/main/src/AmbientSounds.Uwp/Services/DialogService.cs
    internal class DialogService : IDialogService
    {
        public DialogService() { }

        public static bool IsDialogOpen;

        public async Task OpenPowerDialog()
        {
            if (IsDialogOpen)
            {
                return;
            }

            IsDialogOpen = true;
            PowerDialog dialog = new PowerDialog();

            await dialog.ShowAsync();
            IsDialogOpen = false;
        }

        public async Task OpenLockDialog()
        {
            if (IsDialogOpen)
            {
                return;
            }

            IsDialogOpen = true;
            LockDialog dialog = new LockDialog();

            await dialog.ShowAsync();
            IsDialogOpen = false;
        }

        public async Task OpenDebugMenu()
        {
            if (IsDialogOpen)
            {
                return;
            }

            IsDialogOpen = true;
            DebugMenu dialog = new DebugMenu();

            await dialog.ShowAsync();
            IsDialogOpen = false;
        }
    }
}
