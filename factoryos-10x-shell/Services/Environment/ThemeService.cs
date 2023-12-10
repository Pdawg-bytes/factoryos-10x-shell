using factoryos_10x_shell.Library.Services.Environment;
using Microsoft.Toolkit.Uwp.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace factoryos_10x_shell.Services.Environment
{
    internal class ThemeService : IThemeService, IDisposable
    {
        private readonly ThemeListener _themeListener;

        public ThemeService()
        {
            CurrentTheme = App.Current.RequestedTheme;
            _themeListener = new ThemeListener();
            _themeListener.ThemeChanged += ThemeListener_ThemeChanged;
        }

        private ApplicationTheme _currentTheme;
        public ApplicationTheme CurrentTheme
        {
            get => _currentTheme;
            set => _currentTheme = value;
        }

        public event EventHandler GlobalThemeChanged;

        private void ThemeListener_ThemeChanged(ThemeListener sender)
        {
            CurrentTheme = sender.CurrentTheme;
            GlobalThemeChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            _themeListener.ThemeChanged -= ThemeListener_ThemeChanged;
        }
    }
}
