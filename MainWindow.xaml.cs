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

namespace Comment_Remover
{
    public sealed partial class MainWindow : Window
    {
        private static string CommentFile { get; set; }
        private static string Language { get; set; }
        public static bool SelectionChangedBlocker0 { get; set; } = false;
        public static bool SelectionChangedBlocker1 { get; set; } = false;

        public MainWindow()
        {
            this.InitializeComponent();

            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(wndId);
            appWindow.SetIcon(@"Images\Comment Remover Icon.ico");

            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(TitleBar);

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
                        Language = "Python";
                        break;

                    case ".js":
                        LangText.Text = "Language: JavaScript";
                        RemoveCommentBtn.IsEnabled = true;
                        Language = "JavaScript";
                        break;

                    case ".java":
                        LangText.Text = "Language: Java";
                        RemoveCommentBtn.IsEnabled = true;
                        Language = "Java";
                        break;

                    case ".c":
                        LangText.Text = "Language: C";
                        RemoveCommentBtn.IsEnabled = true;
                        Language = "C";
                        break;

                    case ".cpp":
                        LangText.Text = "Language: C++";
                        RemoveCommentBtn.IsEnabled = true;
                        Language = "C++";
                        break;

                    case ".cs":
                        LangText.Text = "Language: C#";
                        RemoveCommentBtn.IsEnabled = true;
                        Language = "C#";
                        break;

                    case ".asm":
                        LangText.Text = "Language: ASM";
                        RemoveCommentBtn.IsEnabled = true;
                        Language = "ASM";
                        break;

                    case ".s":
                        LangText.Text = "Language: ASM";
                        RemoveCommentBtn.IsEnabled = true;
                        Language = "ASM";
                        break;
                }
            }

            else
            {
                FilePathBox.Text = "";
                LangText.Text = "Language: None";
                Language = "";
                RemoveCommentBtn.IsEnabled = false;
            }
        }

        private async void ShowFinishedDialog()
        {
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
            bool CommentHT = false;
            bool CommentSS = false;
            bool CommentSC = false;

            if (Language == "Python")
            {
                CommentHT = true;
            }

            if (Language == "JavaScript")
            {
                CommentSS = true;
            }

            if (Language == "Java")
            {
                CommentSS = true;
            }

            if (Language == "C")
            {
                CommentSS = true;
            }

            if (Language == "C++")
            {
                CommentSS = true;
            }

            if (Language == "C#")
            {
                CommentSS = true;
            }

            if (Language == "ASM")
            {
                CommentSC = true;
            }

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

                            else if (CommentSS == true)
                            {
                                if (line[i] == '/' && line[i + 1] == '/' && !(InsideSingleQuote || InsideDoubleQuote || InsideBacktick))
                                {
                                    line = line.Substring(0, i).TrimEnd();
                                    break;
                                }
                            }

                            else if (CommentHT == true)
                            {
                                if (line[i] == '#' && !(InsideSingleQuote || InsideDoubleQuote || InsideBacktick))
                                {
                                    line = line.Substring(0, i).TrimEnd();
                                    break;
                                }
                            }

                            else if (CommentSC == true)
                            {
                                if (line[i] == ';' && !(InsideSingleQuote || InsideDoubleQuote || InsideBacktick))
                                {
                                    line = line.Substring(0, i).TrimEnd();
                                    break;
                                }
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
            progress.Text = $"0%";
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
            if (SelectionChangedBlocker0)
            {
                SelectionChangedBlocker0 = false;
            }

            else
            {
                if (ThemeSelector.SelectedIndex == 0)
                {
                    ApplicationData.Current.LocalSettings.Values["themeSetting"] = 0;
                }

                if (ThemeSelector.SelectedIndex == 1)
                {
                    ApplicationData.Current.LocalSettings.Values["themeSetting"] = 1;
                }

                if (ThemeSelector.SelectedIndex == 2)
                {
                    ApplicationData.Current.LocalSettings.Values["themeSetting"] = 2;
                }

                RestartBar.IsOpen = true;
                SelectionChangedBlocker0 = false;
            }
        }

        private void ThemeSelecterLoaded(object sender, RoutedEventArgs e)
        {
            SelectionChangedBlocker0 = true;

            object themeSetting = ApplicationData.Current.LocalSettings.Values["themeSetting"];

            if ((int)themeSetting == 0)
            {
                ThemeSelector.SelectedIndex = 0;
            }

            else
            {
                if (App.Current.RequestedTheme == ApplicationTheme.Light)
                {
                    ThemeSelector.SelectedIndex = 1;
                }

                if (App.Current.RequestedTheme == ApplicationTheme.Dark)
                {
                    ThemeSelector.SelectedIndex = 2;
                }
            }
        }

        private void BackgroundSelecterLoaded(object sender, RoutedEventArgs e)
        {
            SelectionChangedBlocker1 = true;

            object backgroundSetting = ApplicationData.Current.LocalSettings.Values["backgroundSetting"];

            BackgroundSelector.SelectedIndex = (int)backgroundSetting;
        }

        private void ChangeBackground(object sender, RoutedEventArgs e)
        {
            if (SelectionChangedBlocker1)
            {
                SelectionChangedBlocker1 = false;
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
