﻿#pragma checksum "C:\Users\Lenovo\Documents\GitHub\SolidareNew\solidare\BantuAnakAsuh\BantuAnakAsuh\Views\PageKonfirmasi.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5175982132BCBAFAF31A1DCF5B3155DC"
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
    
    
    public partial class PageKonfirmasi : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid home;
        
        internal System.Windows.Controls.Image buttonMenu;
        
        internal System.Windows.Controls.TextBox txt_from_bank;
        
        internal Microsoft.Phone.Controls.ListPicker listStatus;
        
        internal System.Windows.Controls.TextBox BankAccountNumber;
        
        internal System.Windows.Controls.TextBox BankTujuan;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/BantuAnakAsuh;component/Views/PageKonfirmasi.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.home = ((System.Windows.Controls.Grid)(this.FindName("home")));
            this.buttonMenu = ((System.Windows.Controls.Image)(this.FindName("buttonMenu")));
            this.txt_from_bank = ((System.Windows.Controls.TextBox)(this.FindName("txt_from_bank")));
            this.listStatus = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("listStatus")));
            this.BankAccountNumber = ((System.Windows.Controls.TextBox)(this.FindName("BankAccountNumber")));
            this.BankTujuan = ((System.Windows.Controls.TextBox)(this.FindName("BankTujuan")));
        }
    }
}
