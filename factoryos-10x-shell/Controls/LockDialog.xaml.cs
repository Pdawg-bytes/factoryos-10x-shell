using factoryos_10x_shell.Library.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace factoryos_10x_shell.Controls
{
    public sealed partial class LockDialog : ContentDialog
    {
        public LockDialog()
        {
            this.InitializeComponent();

            this.DataContext = App.ServiceProvider.GetRequiredService<LockDialogViewModel>();
        }

        public LockDialogViewModel ViewModel => (LockDialogViewModel)this.DataContext;

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void LockButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
