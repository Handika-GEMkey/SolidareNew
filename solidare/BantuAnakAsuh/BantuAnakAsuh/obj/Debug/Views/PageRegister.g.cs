﻿#pragma checksum "C:\Users\handika\Documents\GitHub\SolidareNew\solidare\BantuAnakAsuh\BantuAnakAsuh\Views\PageRegister.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7D3B9151E9EF73A4178DC86A7B3B4342"
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
    
    
    public partial class PageRegister : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBox textBoxUsername;
        
        internal System.Windows.Controls.TextBox textBoxFirstName;
        
        internal System.Windows.Controls.TextBox textBoxLastName;
        
        internal System.Windows.Controls.PasswordBox passBox_password;
        
        internal Microsoft.Phone.Controls.ListPicker listJenisKelamin;
        
        internal System.Windows.Controls.TextBox textBoxAddress;
        
        internal System.Windows.Controls.TextBox textBoxEmail;
        
        internal System.Windows.Controls.TextBox textBoxPhone;
        
        internal Microsoft.Phone.Controls.ListPicker listCardId;
        
        internal System.Windows.Controls.TextBox textBoxIdNumber;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton apBarRegister;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/BantuAnakAsuh;component/Views/PageRegister.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.textBoxUsername = ((System.Windows.Controls.TextBox)(this.FindName("textBoxUsername")));
            this.textBoxFirstName = ((System.Windows.Controls.TextBox)(this.FindName("textBoxFirstName")));
            this.textBoxLastName = ((System.Windows.Controls.TextBox)(this.FindName("textBoxLastName")));
            this.passBox_password = ((System.Windows.Controls.PasswordBox)(this.FindName("passBox_password")));
            this.listJenisKelamin = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("listJenisKelamin")));
            this.textBoxAddress = ((System.Windows.Controls.TextBox)(this.FindName("textBoxAddress")));
            this.textBoxEmail = ((System.Windows.Controls.TextBox)(this.FindName("textBoxEmail")));
            this.textBoxPhone = ((System.Windows.Controls.TextBox)(this.FindName("textBoxPhone")));
            this.listCardId = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("listCardId")));
            this.textBoxIdNumber = ((System.Windows.Controls.TextBox)(this.FindName("textBoxIdNumber")));
            this.apBarRegister = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("apBarRegister")));
        }
    }
}

