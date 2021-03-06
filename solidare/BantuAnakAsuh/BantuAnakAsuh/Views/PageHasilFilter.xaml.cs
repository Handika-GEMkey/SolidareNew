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
    public partial class PageHasilFilter : PhoneApplicationPage
    {
        public PageHasilFilter()
        {
            InitializeComponent();
            ViewModelHasilFilter vmhf = new ViewModelHasilFilter();
            this.DataContext = vmhf;
            Navigation.From_Page = "Fillter";
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageFilter.xaml", UriKind.Relative));
        }

        private void apbarHome_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageDonasi.xaml", UriKind.Relative));
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
        }

    }
}