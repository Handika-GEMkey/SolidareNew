﻿#pragma checksum "C:\Users\Lenovo\Documents\GitHub\SolidareNew\solidare\BantuAnakAsuh\BantuAnakAsuh\Views\PageProfileAnak.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6F880EC46118FC79B274E49352DB63DD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BantuAnakAsuh.Views;
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
    
    
    public partial class PageProfileAnak : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Media.ImageBrush imgAnak;
        
        internal System.Windows.Controls.TextBlock id_anak_asuh;
        
        internal System.Windows.Controls.TextBlock tbNama;
        
        internal System.Windows.Controls.TextBlock tbStatus;
        
        internal System.Windows.Controls.TextBlock tbTtl;
        
        internal System.Windows.Controls.TextBlock tbJk;
        
        internal System.Windows.Controls.TextBlock tbAlamat;
        
        internal System.Windows.Controls.TextBlock tbBiayaDonasi;
        
        internal System.Windows.Controls.Grid home;
        
        internal System.Windows.Controls.Image buttonMenu;
        
        internal BantuAnakAsuh.Views.DonationPopUp Donationpop;
        
        internal System.Windows.Media.CompositeTransform NavDrawTransform1;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/BantuAnakAsuh;component/Views/PageProfileAnak.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.imgAnak = ((System.Windows.Media.ImageBrush)(this.FindName("imgAnak")));
            this.id_anak_asuh = ((System.Windows.Controls.TextBlock)(this.FindName("id_anak_asuh")));
            this.tbNama = ((System.Windows.Controls.TextBlock)(this.FindName("tbNama")));
            this.tbStatus = ((System.Windows.Controls.TextBlock)(this.FindName("tbStatus")));
            this.tbTtl = ((System.Windows.Controls.TextBlock)(this.FindName("tbTtl")));
            this.tbJk = ((System.Windows.Controls.TextBlock)(this.FindName("tbJk")));
            this.tbAlamat = ((System.Windows.Controls.TextBlock)(this.FindName("tbAlamat")));
            this.tbBiayaDonasi = ((System.Windows.Controls.TextBlock)(this.FindName("tbBiayaDonasi")));
            this.home = ((System.Windows.Controls.Grid)(this.FindName("home")));
            this.buttonMenu = ((System.Windows.Controls.Image)(this.FindName("buttonMenu")));
            this.Donationpop = ((BantuAnakAsuh.Views.DonationPopUp)(this.FindName("Donationpop")));
            this.NavDrawTransform1 = ((System.Windows.Media.CompositeTransform)(this.FindName("NavDrawTransform1")));
        }
    }
}

