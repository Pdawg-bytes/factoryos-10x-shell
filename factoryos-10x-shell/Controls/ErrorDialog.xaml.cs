using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace factoryos_10x_shell.Controls
{
    public sealed partial class ErrorDialog : ContentDialog
    {
        public ErrorDialog(string errorContent)
        {
            this.InitializeComponent();
            if (errorContent != string.Empty)
            {
                ErrorText.Text = errorContent;
            }
            else
            {
                ErrorText.Text = "No error message was returned. Please contact the developers of 10X shell.";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
