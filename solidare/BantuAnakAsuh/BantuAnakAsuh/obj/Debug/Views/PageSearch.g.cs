﻿#pragma checksum "C:\Users\Adi Gumelar\Documents\Alix Documents\NewSolidareApps\SolidareNew\solidare\BantuAnakAsuh\BantuAnakAsuh\Views\PageSearch.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "89256C64828E29B909D6BD37505C1FC5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
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
    
    
    public partial class PageSearch : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBox textboxSearch;
        
        internal System.Windows.Controls.TextBlock textBlockResult;
        
        internal System.Windows.Controls.ProgressBar progresBarSearch;
        
        internal System.Windows.Controls.ListBox ListSearch;
        
        internal System.Windows.Controls.Image buttonMenu;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton apBarSeacrh;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/BantuAnakAsuh;component/Views/PageSearch.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.textboxSearch = ((System.Windows.Controls.TextBox)(this.FindName("textboxSearch")));
            this.textBlockResult = ((System.Windows.Controls.TextBlock)(this.FindName("textBlockResult")));
            this.progresBarSearch = ((System.Windows.Controls.ProgressBar)(this.FindName("progresBarSearch")));
            this.ListSearch = ((System.Windows.Controls.ListBox)(this.FindName("ListSearch")));
            this.buttonMenu = ((System.Windows.Controls.Image)(this.FindName("buttonMenu")));
            this.apBarSeacrh = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("apBarSeacrh")));
        }
    }
}

