using factoryos_10x_shell.Library.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace factoryos_10x_shell.Controls.DesktopControls
{
    public sealed partial class MainDesktop : Page
    {
        public static Frame TaskbarFrameP { get; private set; }
        public static Frame StartFrameP { get; private set; }

        public static Storyboard OpenStartStoryboard { get; private set; }
        public static Storyboard CloseStartStoryboard { get; private set; }

        public static TranslateTransform StartTransform { get; private set; }

        public MainDesktop()
        {
            this.InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<MainDesktopViewModel>();

            // Frame init
            TaskbarFrameP = TaskbarFrame;
            TaskbarFrameP.Navigate(typeof(Default10xBar));

            StartFrameP = StartMenuFrame;
            StartFrameP.Navigate(typeof(StartMenu));

            StartTransform = StartMenuTransform;

            InitOpenBoard();
            InitCloseBoard();

            App.MediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/BootUp.wav"));
            App.MediaPlayer.Play();
        }

        public MainDesktopViewModel ViewModel => (MainDesktopViewModel)this.DataContext;

        private static void InitOpenBoard()
        {
            DoubleAnimation slideInAnimation = new DoubleAnimation
            {
                From = 800,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(0.3)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            Storyboard.SetTarget(slideInAnimation, StartTransform);
            Storyboard.SetTargetProperty(slideInAnimation, "Y");

            OpenStartStoryboard = new Storyboard();
            OpenStartStoryboard.Children.Add(slideInAnimation);
        }

        private static void InitCloseBoard()
        {
            DoubleAnimation slideOutAnimation = new DoubleAnimation
            {
                From = 0,
                To = 800,
                Duration = new Duration(TimeSpan.FromSeconds(0.20)),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            Storyboard.SetTarget(slideOutAnimation, StartTransform);
            Storyboard.SetTargetProperty(slideOutAnimation, "Y");

            CloseStartStoryboard = new Storyboard();
            CloseStartStoryboard.Children.Add(slideOutAnimation);
        }
    }
}
