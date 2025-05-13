////////////////////////////////////////////////
// Comment Remover - App.xaml.cs              //
// Language: C#                               //
// Author: Jacob Waters                       //
// Github: github.com/jpwaters09              //
// Copyright (c) 2025 Jacob Waters            //
// Contact me: jpwaters09.business@gmail.com  //
////////////////////////////////////////////////

using System;
using Microsoft.UI.Xaml;

namespace Comment_Remover
{
    public partial class App : Application
    {
        public static Window? m_window { get; private set; }

        public App()
        {
            this.InitializeComponent();

            m_window = new MainWindow();
            m_window.Activate();
        }

        public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }
    }
}
