////////////////////////////////////////////////
// Comment Remover - MainWindow.xaml.cs       //
// Language: C#                               //
// Author: Jacob Waters                       //
// Github: github.com/jpwaters09              //
// Copyright (c) 2025 Jacob Waters            //
// Contact me: jpwaters.github@gmail.com      //
////////////////////////////////////////////////

using Microsoft.UI.Xaml;
using WinRT.Interop;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Storage;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel;
using System;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Composition.SystemBackdrops;

namespace Comment_Remover
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            ThemeHelper.ActiveWindows.Add(this);

            string? savedTheme = ApplicationData.Current.LocalSettings.Values["AppTheme"]?.ToString();

            if (savedTheme != null)
            {
                ThemeHelper.RootTheme = Comment_Remover.App.GetEnum<ElementTheme>(savedTheme);
            }

            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(TitleBar);

            UpdateTitleBarColors();

            if (this.Content is FrameworkElement rootElement)
            {
                rootElement.ActualThemeChanged += RootElement_ActualThemeChanged;
            }

            ContentFrame.Navigate(typeof(HomePage));
            NavBar.SelectedItem = NavBar.MenuItems[0];

            var packageVersion = Package.Current.Id.Version;

            VersionText.Text = $"v{packageVersion.Major}.{packageVersion.Minor}.{packageVersion.Build}";
        }

        private void RootElement_ActualThemeChanged(FrameworkElement sender, object args)
        {
            UpdateTitleBarColors();
        }

        private ElementTheme GetCurrentTheme()
        {
            if (this.Content is FrameworkElement rootElement)
            {
                return rootElement.ActualTheme;
            }
            return ElementTheme.Default;
        }

        private SystemBackdropTheme GetSystemBackdropTheme()
        {
            switch (GetCurrentTheme())
            {
                case ElementTheme.Dark:
                    return SystemBackdropTheme.Dark;
                case ElementTheme.Light:
                    return SystemBackdropTheme.Light;
                default:
                    return SystemBackdropTheme.Default;
            }
        }

        public void UpdateTitleBarColors()
        {
            var theme = GetCurrentTheme();
            var hwnd = WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);

            var titleBar = appWindow.TitleBar;

            if (theme == ElementTheme.Dark)
            {
                titleBar.ButtonForegroundColor = Colors.White;
            }
            else if (theme == ElementTheme.Light)
            {
                titleBar.ButtonForegroundColor = Colors.Black;
            }
            else
            {
                titleBar.ButtonForegroundColor = null;
            }
        }

        private void NavBarSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;

            if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Top)
            {
                navOptions.IsNavigationStackEnabled = false;
            }

            Type? pageType = null;

            if (args.IsSettingsSelected)
            {
                pageType = typeof(SettingsPage);
            }

            else if (NavBar.SelectedItem == NavBar.MenuItems[0])
            {
                pageType = typeof(HomePage);
            }

            ContentFrame.NavigateToType(pageType, null, navOptions);
        }
    }
}
