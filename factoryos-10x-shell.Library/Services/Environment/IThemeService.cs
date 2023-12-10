using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace factoryos_10x_shell.Library.Services.Environment
{
    /// <summary>
    /// Defines a global theme service that sends updates when a theme change occurs.
    /// </summary>
    public interface IThemeService
    {
        /// <summary>
        /// An event that is raised when the global system theme changes.
        /// </summary>
        public event EventHandler GlobalThemeChanged;

        /// <summary>
        /// The current theme of the app.
        /// </summary>
        public ApplicationTheme CurrentTheme { get; set; }
    }
}
