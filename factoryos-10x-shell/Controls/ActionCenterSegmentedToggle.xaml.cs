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
    public partial class ActionCenterSegmentedToggleControl : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(ActionCenterSegmentedToggleControl), null);

        public static readonly DependencyProperty SubtextProperty =
            DependencyProperty.Register("Subtext", typeof(string), typeof (ActionCenterSegmentedToggleControl), null);

        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ActionCenterSegmentedToggleControl), null);


        public ActionCenterSegmentedToggleControl()
        {
            this.InitializeComponent();
        }

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string Subtext
        {
            get => (string)GetValue(SubtextProperty);
            set => SetValue(SubtextProperty, value);
        }

        public bool IsChecked
        {
            get => (bool)GetValue(CheckedProperty);
            set
            {
                SetValue(CheckedProperty, value);
                if (value) { VisualStateManager.GoToState(this, "Checked", true); }
                else { VisualStateManager.GoToState(this, "Unchecked", true); }
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
