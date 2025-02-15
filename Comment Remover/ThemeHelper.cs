////////////////////////////////////////////////
// Comment Remover - ThemeHelper.cs           //
// Language: C#                               //
// Author: Jacob Waters                       //
// Github: github.com/jpwaters09              //
// Copyright (c) 2025 Jacob Waters            //
// Contact me: jpwaters.github@gmail.com      //
////////////////////////////////////////////////

using Microsoft.UI.Xaml;
using System.Collections.Generic;
using Windows.Storage;

namespace Comment_Remover
{
    internal class ThemeHelper
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
    }
}
