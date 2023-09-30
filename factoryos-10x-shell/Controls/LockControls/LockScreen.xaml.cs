using factoryos_10x_shell.Controls.DesktopControls;
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
        private static int[] testPin = new int[4];
        private static int[] pinInput = new int[4];
        private static int pinOffset = 0;

        public LockScreen()
        {
            this.InitializeComponent();

            InputBox.Focus(FocusState.Keyboard);

            testPin[0] = 1;
            testPin[1] = 2;
            testPin[2] = 3;
            testPin[3] = 4;
        }

        private void InputBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {

        }

        private void InputBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            string labelText = String.Empty;
            for (int i = 0; i <= InputBox.Text.Length - 1; i++ )
            {
                labelText += "•";
            }
            PinLabel.Text = labelText;
            bool isCorrect = false;
            try { isCorrect = InputBox.Text.Select(c => int.Parse(c.ToString())).SequenceEqual(testPin); }
            catch { InputBox.Text = InputBox.Text.Substring(0, InputBox.Text.Length - 1); }
            if (isCorrect && InputBox.Text.Length == 4)
            {
                MainPage.DesktopFrameP.Navigate(typeof(MainDesktop));
            }
            else if (InputBox.Text.Length == 4)
            {
                InputBox.Text = String.Empty;
                PinLabel.Text = "PIN";
            }
        }

        private void OtherObject_GotFocus(object sender, RoutedEventArgs e)
        {
            InputBox.Focus(FocusState.Keyboard);
        }
    }
}
