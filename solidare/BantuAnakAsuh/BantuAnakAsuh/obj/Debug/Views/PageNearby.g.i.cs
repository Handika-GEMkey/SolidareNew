﻿#pragma checksum "C:\Users\Adi Gumelar\Documents\Alix Documents\NewSolidareApps\SolidareNew\solidare\BantuAnakAsuh\BantuAnakAsuh\Views\PageNearby.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1A853161955ACA1F4612EB9068C45AF1"
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
using Microsoft.Phone.Maps.Controls;
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
    
    
    public partial class PageNearby : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid home;
        
        internal System.Windows.Controls.Image buttonMenu;
        
        internal Microsoft.Phone.Maps.Controls.Map MapVieMode;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/BantuAnakAsuh;component/Views/PageNearby.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.home = ((System.Windows.Controls.Grid)(this.FindName("home")));
            this.buttonMenu = ((System.Windows.Controls.Image)(this.FindName("buttonMenu")));
            this.MapVieMode = ((Microsoft.Phone.Maps.Controls.Map)(this.FindName("MapVieMode")));
            this.apbarHome = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("apbarHome")));
        }
    }
}

