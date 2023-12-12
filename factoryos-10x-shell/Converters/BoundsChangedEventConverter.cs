using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using factoryos_10x_shell.Controls.DesktopControls;

namespace factoryos_10x_shell.Converters
{
    public class BoundsChangedEventConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) =>
            value is FrameworkElement fe ? fe.TransformToVisual(null).TransformBounds(new Rect(0, 0, fe.ActualWidth, fe.ActualHeight)) : new Rect(0, 0, 0, 0);

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotSupportedException();
    }
}
