using factoryos_10x_shell.Library.Services.Managers;
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
        public static Storyboard OpenStartStoryboard { get; private set; }
        public static Storyboard CloseStartStoryboard { get; private set; }

        private readonly IStartManagerService m_startManager;

        public MainDesktop()
        {
            this.InitializeComponent();

            DataContext = App.ServiceProvider.GetRequiredService<MainDesktopViewModel>();

            m_startManager = App.ServiceProvider.GetRequiredService<IStartManagerService>();
            m_startManager.StartVisibilityChanged += StartManager_StartVisibilityChanged;

            TaskbarFrame.Navigate(typeof(Default10xBar));
            StartMenuFrame.Navigate(typeof(StartMenu));

            InitOpenBoard();
            InitCloseBoard();

            App.MediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Sounds/BootUp.wav"));
            App.MediaPlayer.Play();
        }

        public MainDesktopViewModel ViewModel => (MainDesktopViewModel)this.DataContext;

        private void InitOpenBoard()
        {
            DoubleAnimation slideInAnimation = new DoubleAnimation
            {
                From = 800,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(0.3)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            Storyboard.SetTarget(slideInAnimation, StartMenuTransform);
            Storyboard.SetTargetProperty(slideInAnimation, "Y");

            OpenStartStoryboard = new Storyboard();
            OpenStartStoryboard.Children.Add(slideInAnimation);
        }

        private void InitCloseBoard()
        {
            DoubleAnimation slideOutAnimation = new DoubleAnimation
            {
                From = 0,
                To = 800,
                Duration = new Duration(TimeSpan.FromSeconds(0.20)),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            Storyboard.SetTarget(slideOutAnimation, StartMenuTransform);
            Storyboard.SetTargetProperty(slideOutAnimation, "Y");

            CloseStartStoryboard = new Storyboard();
            CloseStartStoryboard.Children.Add(slideOutAnimation);
        }


        private void StartManager_StartVisibilityChanged(object sender, Library.Events.StartVisibilityChangedEventArgs e)
        {
            if (e.CurrentVisibility) { OpenStartStoryboard.Begin(); }
            else { CloseStartStoryboard.Begin(); }
        }
    }
}
