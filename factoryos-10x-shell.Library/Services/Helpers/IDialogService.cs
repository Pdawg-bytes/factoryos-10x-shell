using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace factoryos_10x_shell.Library.Services.Helpers
{
    /// <summary>
    /// Interface for opening dialogs throughout the app.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Opens the power options dialog.
        /// </summary>
        Task OpenPowerDialog();

        /// <summary>
        /// Opens the lock screen dialog.
        /// </summary>
        Task OpenLockDialog();

        /// <summary>
        /// Opens the debugging menu
        /// </summary>
        Task OpenDebugMenu();
    }
}
