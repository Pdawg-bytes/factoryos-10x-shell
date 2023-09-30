using factoryos_10x_shell.Controls.LockControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace factoryos_10x_shell.Controls
{
    public sealed partial class LockDialog : ContentDialog
    {
        public LockDialog()
        {
            this.InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void LockButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.DesktopFrameP.Navigate(typeof(LockScreen));
            this.Hide();
        }
    }
}
