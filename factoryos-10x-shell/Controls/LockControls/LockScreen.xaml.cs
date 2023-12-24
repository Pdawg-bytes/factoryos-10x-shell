using factoryos_10x_shell.Controls.DesktopControls;
using factoryos_10x_shell.Helpers;
using factoryos_10x_shell.Library.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace factoryos_10x_shell.Controls.LockControls
{
    public sealed partial class LockScreen : Page
    {
        public LockScreen()
        {
            this.InitializeComponent();

            this.DataContext = App.ServiceProvider.GetRequiredService<LockScreenViewModel>();

            InputBox.Focus(FocusState.Keyboard);
        }

        public LockScreenViewModel ViewModel => (LockScreenViewModel)this.DataContext;

        private void OtherObject_GotFocus(object sender, RoutedEventArgs e)
        {
            InputBox.Focus(FocusState.Keyboard);
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content.ToString();

            if (int.TryParse(digit, out _)) { InputBox.Password += digit; }
            else if (InputBox.Password.Length > 0) { InputBox.Password = InputBox.Password.Substring(0, InputBox.Password.Length - 1); }
        }
    }
}
