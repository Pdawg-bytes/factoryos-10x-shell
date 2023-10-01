using factoryos_10x_shell.Controls.DesktopControls;
using factoryos_10x_shell.Helpers;
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

            PinSecurityManager.SetPin(new int[4] { 8, 7, 9, 0});

            InputBox.Focus(FocusState.Keyboard);
        }

        private void InputBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            string labelText = String.Empty;
            for (int i = 0; i <= InputBox.Text.Length - 1; i++)
            {
                labelText += "•";
            }
            PinLabel.Text = labelText;
            bool isCorrect = false;
            // Try to parse the entered text as an integer array
            try
            {
                int[] enteredPin = InputBox.Text.Select(c => int.Parse(c.ToString())).ToArray();
                isCorrect = PinSecurityManager.CheckPin(enteredPin);
            }
            catch
            {
                // Handle non-digit input here, such as removing the last character
                InputBox.Text = InputBox.Text.Substring(0, InputBox.Text.Length - 1);
            }
            if (isCorrect && InputBox.Text.Length == 4)
            {
                MainPage.DesktopFrameP.Navigate(typeof(MainDesktop));
            }
            else if (InputBox.Text.Length == 4 || InputBox.Text.Length == 0)
            {
                InputBox.Text = String.Empty;
                PinLabel.Text = "PIN";
            }
        }

        private void OtherObject_GotFocus(object sender, RoutedEventArgs e)
        {
            InputBox.Focus(FocusState.Keyboard);
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content.ToString();

            if (int.TryParse(digit, out _)) { InputBox.Text += digit; }
            else if (InputBox.Text.Length > 0) { InputBox.Text = InputBox.Text.Substring(0, InputBox.Text.Length - 1); }
        }
    }
}
