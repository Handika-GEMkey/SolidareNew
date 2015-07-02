﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BantuAnakAsuh.ViewModels;
using BantuAnakAsuh.Helper;

namespace BantuAnakAsuh.Views
{
    public partial class PageDetailDonatur : PhoneApplicationPage
    {
        public PageDetailDonatur()
        {
            InitializeComponent();
            ViewModelProfileDonatur vmpd = new ViewModelProfileDonatur();
            this.DataContext = vmpd;
            Navigation.From_Page = "PageDetailDonatur";
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageProfileDonatur.xaml", UriKind.Relative));
        }

        private void apbarSetting_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageSetting.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageProfileDonatur.xaml", UriKind.Relative));
        }
    }
}