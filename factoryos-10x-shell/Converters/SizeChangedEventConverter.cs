using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace factoryos_10x_shell.Converters
{
    internal class SizeChangedEventConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => value is FrameworkElement fe ? (fe.ActualSize) : new System.Numerics.Vector2(0,0);

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotSupportedException();
    }
}
