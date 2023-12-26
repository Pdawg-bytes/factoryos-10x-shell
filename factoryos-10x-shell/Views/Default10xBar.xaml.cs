using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using factoryos_10x_shell.Library.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace factoryos_10x_shell.Views
{
    public sealed partial class Default10xBar : Page
    {
        public Default10xBar()
        {
            this.InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<Default10xBarViewModel>();
        }

        public Default10xBarViewModel ViewModel => (Default10xBarViewModel)this.DataContext;
    }
}
