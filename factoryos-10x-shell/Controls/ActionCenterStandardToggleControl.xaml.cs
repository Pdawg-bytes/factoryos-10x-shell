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
    public sealed partial class ActionCenterStandardToggleControl : UserControl
    {
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(ActionCenterSegmentedToggleControl), null);

        public static readonly DependencyProperty SubtextProperty =
            DependencyProperty.Register("Subtext", typeof(string), typeof(ActionCenterSegmentedToggleControl), null);

        public ActionCenterStandardToggleControl()
        {
            this.InitializeComponent();
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string Subtext
        {
            get { return (string)GetValue(SubtextProperty); }
            set { SetValue(SubtextProperty, value); }
        }
    }
}
