﻿#pragma checksum "I:\baa baru\BAA apps Imagine Cup\BantuAnakAsuh\BantuAnakAsuh\Views\PageNews.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6D3080368B871D8669CF7FEFD3C10E17"
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
using Microsoft.Phone.Shell;
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
    
    
    public partial class PageNews : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Image buttonMenu;
        
        internal System.Windows.Controls.ListBox ListNews;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton apbarHome;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/BantuAnakAsuh;component/Views/PageNews.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.buttonMenu = ((System.Windows.Controls.Image)(this.FindName("buttonMenu")));
            this.ListNews = ((System.Windows.Controls.ListBox)(this.FindName("ListNews")));
            this.apbarHome = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("apbarHome")));
        }
    }
}

