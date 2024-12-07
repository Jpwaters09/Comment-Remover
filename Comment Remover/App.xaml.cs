////////////////////////////////////////////////
// Comment Remover - App.xaml.cs              //
// Language: C#                               //
// Author: Jacob Waters                       //
// Github: github.com/jpwaters09              //
// Copyright (c) 2024 Jacob Waters            //
// Contact me: jpwaters.github@gmail.com      //
////////////////////////////////////////////////

using Microsoft.UI.Xaml;
using System;

namespace Comment_Remover
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}
