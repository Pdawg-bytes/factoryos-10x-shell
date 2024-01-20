using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace factoryos_10x_shell.Controls
{
    public sealed partial class ActionCenterSegmentedToggleControl : UserControl
    {
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(ActionCenterSegmentedToggleControl), null);

        public static readonly DependencyProperty SubtextProperty =
            DependencyProperty.Register("Subtext", typeof(string), typeof (ActionCenterSegmentedToggleControl), null);


        public ActionCenterSegmentedToggleControl()
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

        private void ToggleSection_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleSection.IsChecked == true)
            {
                VisualStateManager.GoToState(this, "Checked", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Unchecked", true);
            }
        }
    }
}
