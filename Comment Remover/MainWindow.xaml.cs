////////////////////////////////////////////////
// Comment Remover - MainWindow.xaml.cs       //
// Language: C#                               //
// Author: Jacob Waters                       //
// Github: github.com/jpwaters09              //
// Copyright (c) 2025 Jacob Waters            //
// Contact me: jpwaters.github@gmail.com      //
////////////////////////////////////////////////

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Storage.Pickers;
using WinRT.Interop;
using Windows.ApplicationModel;
using System.Diagnostics;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Storage;
using Microsoft.UI.Xaml.Media;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Media;

namespace Comment_Remover
{
    public sealed partial class MainWindow : Window
    {
        static public List<Window> ActiveWindows { get { return _activeWindows; } }
        static private List<Window> _activeWindows = new List<Window>();

        public static ElementTheme RootTheme
        {
            get
            {
                foreach (Window window in ActiveWindows)
                {
                    if (window.Content is FrameworkElement rootElement)
                    {
                        return rootElement.RequestedTheme;
                    }
                }

                return ElementTheme.Default;
            }
            set
            {
                foreach (Window window in ActiveWindows)
                {
                    if (window.Content is FrameworkElement rootElement)
                    {
                        rootElement.RequestedTheme = value;
                    }
                }

                ApplicationData.Current.LocalSettings.Values["AppTheme"] = value.ToString();
            }
        }

        private static string CommentFile { get; set; }

        private static bool CommentHT {  get; set; } = false;
        private static bool CommentSS { get; set; } = false;
        private static bool CommentSC { get; set; } = false;

        public static bool SelectionChangedBlocker { get; set; } = false;

        public MainWindow()
        {
            this.InitializeComponent();
            ActiveWindows.Add(this);

            string savedTheme = ApplicationData.Current.LocalSettings.Values["AppTheme"]?.ToString();

            if (savedTheme != null)
            {
                RootTheme = Comment_Remover.App.GetEnum<ElementTheme>(savedTheme);
            }

            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(TitleBar);

            UpdateTitleBarColors();

            if (this.Content is FrameworkElement rootElement)
            {
                rootElement.ActualThemeChanged += RootElement_ActualThemeChanged;
            }

            var packageVersion = Package.Current.Id.Version;

            VersionText.Text = $"v{packageVersion.Major}.{packageVersion.Minor}.{packageVersion.Build}";
            VersionText1.Text = $"v{packageVersion.Major}.{packageVersion.Minor}.{packageVersion.Build}";

            object backgroundSetting = ApplicationData.Current.LocalSettings.Values["backgroundSetting"];

            if (backgroundSetting != null)
            {
                if ((int)backgroundSetting == 1)
                {
                    SystemBackdrop = new DesktopAcrylicBackdrop();
                }
            }

            else
            {
                ApplicationData.Current.LocalSettings.Values["backgroundSetting"] = 0;
            }
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

        private async void OpenFile(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();

            var hWnd = WindowNative.GetWindowHandle(this);

            InitializeWithWindow.Initialize(openPicker, hWnd);

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.FileTypeFilter.Add(".py");
            openPicker.FileTypeFilter.Add(".js");
            openPicker.FileTypeFilter.Add(".java");
            openPicker.FileTypeFilter.Add(".c");
            openPicker.FileTypeFilter.Add(".cpp");
            openPicker.FileTypeFilter.Add(".cs");
            openPicker.FileTypeFilter.Add(".asm");
            openPicker.FileTypeFilter.Add(".S");

            var file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                CommentFile = file.Path;
                FilePathBox.Text = file.Path;

                var fileType = file.FileType.ToLower();

                switch (fileType)
                {
                    case ".py":
                        LangText.Text = "Language: Python";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = true;
                        CommentSS = false;
                        CommentSC = false;
                        break;

                    case ".js":
                        LangText.Text = "Language: JavaScript";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        break;

                    case ".java":
                        LangText.Text = "Language: Java";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        break;

                    case ".c":
                        LangText.Text = "Language: C";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        break;

                    case ".cpp":
                        LangText.Text = "Language: C++";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        break;

                    case ".cs":
                        LangText.Text = "Language: C#";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        break;

                    case ".asm":
                        LangText.Text = "Language: ASM";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = false;
                        CommentSC = true;
                        break;

                    case ".s":
                        LangText.Text = "Language: ASM";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = false;
                        CommentSC = true;
                        break;
                }
            }

            else
            {
                FilePathBox.Text = "";
                LangText.Text = "Language: None";
                CommentHT = false;
                CommentSS = false;
                CommentSC = false;
                RemoveCommentBtn.IsEnabled = false;
            }
        }

        private async void ShowFinishedDialog()
        {
            SystemSounds.Asterisk.Play();
            
            ContentDialog FinishedDialog = new ContentDialog()
            {
                Title = "Removed Comments!",
                CloseButtonText = "Ok",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                XamlRoot = this.Content.XamlRoot
            };

            await FinishedDialog.ShowAsync();
        }

        private async void RemoveComments(object sender, RoutedEventArgs e)
        {
            static bool IsCharEscaped(string line, int index)
            {
                int backslashes = 0;

                while (index > 0 && line[index - 1] == '\\')
                {
                    backslashes++;
                    index--;
                }

                return backslashes % 2 == 1;
            }

            ProgressContainer.Visibility = Visibility.Visible;
            RemoveCommentBtn.IsEnabled = false;
            FileSelect.IsEnabled = false;

            string CommentsRemoved = @"";
            bool writeToFile = true;

            await Task.Run(() =>
            {
                string[] lines = File.ReadAllLines(CommentFile);

                try
                {
                    for (int idx = 0; idx < lines.Length; idx++)
                    {
                        string line = lines[idx];

                        bool InsideSingleQuote = false;
                        bool InsideDoubleQuote = false;
                        bool InsideBacktick = false;

                        int i = 0;

                        while (i < line.Length)
                        {
                            if (line[i] == '\'' && !IsCharEscaped(line, i) && !InsideDoubleQuote && !InsideBacktick)
                            {
                                InsideSingleQuote = !InsideSingleQuote;
                            }

                            else if (line[i] == '"' && !IsCharEscaped(line, i) && !InsideSingleQuote && !InsideBacktick)
                            {
                                InsideDoubleQuote = !InsideDoubleQuote;
                            }

                            else if (line[i] == '`' && !IsCharEscaped(line, i) && !InsideSingleQuote && !InsideDoubleQuote)
                            {
                                InsideBacktick = !InsideBacktick;
                            }

                            else if (CommentSS && line[i] == '/' && line[i + 1] == '/' && !(InsideSingleQuote || InsideDoubleQuote || InsideBacktick))
                            {
                                line = line.Substring(0, i).TrimEnd();
                                break;
                            }

                            else if (CommentHT && line[i] == '#' && !(InsideSingleQuote || InsideDoubleQuote || InsideBacktick))
                            {
                                line = line.Substring(0, i).TrimEnd();
                                break;
                            }

                            else if (CommentSC && line[i] == ';' && !(InsideSingleQuote || InsideDoubleQuote || InsideBacktick))
                            {
                                line = line.Substring(0, i).TrimEnd();
                                break;
                            }

                            i++;
                        }

                        CommentsRemoved += line.TrimEnd();

                        if (idx < lines.Length - 1)
                        {
                            CommentsRemoved += "\n";
                        }


                        DispatcherQueue.TryEnqueue(() =>
                        {
                            int percentage = (int)Math.Round((((double)(idx + 1) / lines.Length) * 100));

                            progressBar.Value = percentage;
                            progress.Text = $"{percentage}% ";
                        });
                    }
                }

                catch (Exception)
                {
                    writeToFile = false;

                    DispatcherQueue.TryEnqueue(() =>
                    {
                        progressBar.Value = 100;
                        progress.Text = $"100%";
                    });
                }

                if (writeToFile)
                {
                    using (StreamWriter wfile = new StreamWriter(CommentFile))
                    {
                        wfile.Write(CommentsRemoved);
                    }
                }
            });

            ProgressContainer.Visibility = Visibility.Collapsed;

            progressBar.Value = 0;
            progress.Text = "0%";
            FileSelect.IsEnabled = true;
            ShowFinishedDialog();
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            SettingsBtn.Visibility = Visibility.Collapsed;
            MainContent.Visibility = Visibility.Collapsed;
            BackBtn.Visibility = Visibility.Visible;
            SettingsPage.Visibility = Visibility.Visible;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            SettingsBtn.Visibility = Visibility.Visible;
            MainContent.Visibility = Visibility.Visible;
            BackBtn.Visibility = Visibility.Collapsed;
            SettingsPage.Visibility = Visibility.Collapsed;
        }

        private void SendEmail(object sender, RoutedEventArgs e)
        {
            var packageVersion = Package.Current.Id.Version;

            string email = "jpwaters.github@gmail.com";
            string subject = Uri.EscapeDataString($"Comment Remover: v{packageVersion.Major}.{packageVersion.Minor}.{packageVersion.Build}");
            string mailto = $"mailto:{email}?subject={subject}";

            Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            var selectedTheme = ((ComboBoxItem)ThemeSelector.SelectedItem)?.Tag?.ToString();

            if (selectedTheme != null)
            {
                RootTheme = App.GetEnum<ElementTheme>(selectedTheme);
            }
        }

        private void ThemeSelecterLoaded(object sender, RoutedEventArgs e)
        {
            var currentTheme = RootTheme;
            switch (currentTheme)
            {
                case ElementTheme.Light:
                    ThemeSelector.SelectedIndex = 1;
                    break;

                case ElementTheme.Dark:
                    ThemeSelector.SelectedIndex = 2;
                    break;

                case ElementTheme.Default:
                    ThemeSelector.SelectedIndex = 0;
                    break;
            }
        }

        private void BackgroundSelecterLoaded(object sender, RoutedEventArgs e)
        {
            SelectionChangedBlocker = true;

            object backgroundSetting = ApplicationData.Current.LocalSettings.Values["backgroundSetting"];

            BackgroundSelector.SelectedIndex = (int)backgroundSetting;
        }

        private void ChangeBackground(object sender, RoutedEventArgs e)
        {
            if (SelectionChangedBlocker)
            {
                SelectionChangedBlocker = false;
            }

            else
            {
                if (BackgroundSelector.SelectedIndex == 0)
                {
                    ApplicationData.Current.LocalSettings.Values["backgroundSetting"] = 0;
                    SystemBackdrop = new MicaBackdrop();
                }

                if (BackgroundSelector.SelectedIndex == 1)
                {
                    ApplicationData.Current.LocalSettings.Values["backgroundSetting"] = 1;
                    SystemBackdrop = new DesktopAcrylicBackdrop();
                }
            }
        }
    }
}
