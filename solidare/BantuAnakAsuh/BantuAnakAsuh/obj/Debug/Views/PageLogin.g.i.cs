﻿#pragma checksum "C:\Users\Lenovo\Documents\GitHub\SolidareNew\solidare\BantuAnakAsuh\BantuAnakAsuh\Views\PageLogin.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B0D0011FC8584C01FB60611B9EEAAB68"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace BantuAnakAsuh.Views {
    
    
    public partial class PageLogin : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBox textBox_email;
        
        internal System.Windows.Controls.PasswordBox passBox_password;
        
        internal System.Windows.Controls.TextBlock textForgot;
        
        internal System.Windows.Controls.ProgressBar LoadingBar;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/BantuAnakAsuh;component/Views/PageLogin.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.textBox_email = ((System.Windows.Controls.TextBox)(this.FindName("textBox_email")));
            this.passBox_password = ((System.Windows.Controls.PasswordBox)(this.FindName("passBox_password")));
            this.textForgot = ((System.Windows.Controls.TextBlock)(this.FindName("textForgot")));
            this.LoadingBar = ((System.Windows.Controls.ProgressBar)(this.FindName("LoadingBar")));
        }
    }
}

