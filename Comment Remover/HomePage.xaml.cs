////////////////////////////////////////////////
// Comment Remover - HomePage.xaml.cs         //
// Language: C#                               //
// Author: Jacob Waters                       //
// Github: github.com/jpwaters09              //
// Copyright (c) 2025 Jacob Waters            //
// Contact me: jpwaters09.business@gmail.com  //
////////////////////////////////////////////////

using System;
using System.IO;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Media;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Comment_Remover
{
    public sealed partial class HomePage : Page
    {
        private static string? CommentFile { get; set; }

        private static bool CommentHT { get; set; } = false;
        private static bool CommentSS { get; set; } = false;
        private static bool CommentSC { get; set; } = false;
        private static bool CommentHH { get; set; } = false;
        private static bool CommentCC { get; set; } = false;

        public HomePage()
        {
            this.InitializeComponent();
        }

        private async void OpenFile(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();

            var hWnd = WindowNative.GetWindowHandle(App.m_window);

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
            openPicker.FileTypeFilter.Add(".lua");
            openPicker.FileTypeFilter.Add(".sh");
            openPicker.FileTypeFilter.Add(".bat");
            openPicker.FileTypeFilter.Add(".go");
            openPicker.FileTypeFilter.Add(".rs");
            openPicker.FileTypeFilter.Add(".rb");
            openPicker.FileTypeFilter.Add(".swift");

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
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".js":
                        LangText.Text = "Language: JavaScript";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".java":
                        LangText.Text = "Language: Java";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".c":
                        LangText.Text = "Language: C";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".cpp":
                        LangText.Text = "Language: C++";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".cs":
                        LangText.Text = "Language: C#";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".asm":
                        LangText.Text = "Language: ASM";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = false;
                        CommentSC = true;
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".s":
                        LangText.Text = "Language: ASM";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = false;
                        CommentSC = true;
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".lua":
                        LangText.Text = "Language: Lua";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = false;
                        CommentSC = false;
                        CommentHH = true;
                        CommentCC = false;
                        break;

                    case ".sh":
                        LangText.Text = "Language: Shell";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = true;
                        CommentSS = false;
                        CommentSC = false;
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".bat":
                        LangText.Text = "Language: Batch";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = false;
                        CommentSC = false;
                        CommentHH = false;
                        CommentCC = true;
                        break;

                    case ".go":
                        LangText.Text = "Language: Go";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".rs":
                        LangText.Text = "Language: Rust";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".rb":
                        LangText.Text = "Language: Ruby";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = true;
                        CommentSS = false;
                        CommentSC = false;
                        CommentHH = false;
                        CommentCC = false;
                        break;

                    case ".swift":
                        LangText.Text = "Language: Swift";
                        RemoveCommentBtn.IsEnabled = true;
                        CommentHT = false;
                        CommentSS = true;
                        CommentSC = false;
                        CommentHH = false;
                        CommentCC = false;
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
                CommentHH = false;
                CommentCC = false;
                RemoveCommentBtn.IsEnabled = false;
            }
        }

        private async void ShowFinishedDialog()
        {
            SystemSounds.Asterisk.Play();

            ContentDialog FinishedDialog = new ContentDialog()
            {
                Title = "Removed Comments",
                CloseButtonText = "Ok",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                XamlRoot = App.m_window.Content.XamlRoot
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

                            else if (CommentHH && line[i] == '-' && line[i + 1] == '-' && !(InsideSingleQuote || InsideDoubleQuote || InsideBacktick))
                            {
                                line = line.Substring(0, i).TrimEnd();
                                break;
                            }

                            else if (CommentCC && line[i] == ':' && line[i + 1] == ':' && !(InsideSingleQuote || InsideDoubleQuote || InsideBacktick))
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
            LangText.Text = "Language: None";
            FilePathBox.Text = "";
            ShowFinishedDialog();
        }
    }
}
