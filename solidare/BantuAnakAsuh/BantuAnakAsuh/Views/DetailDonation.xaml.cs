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
    public partial class DetailDonation : PhoneApplicationPage
    {
        public DetailDonation()
        {
            InitializeComponent();
            if (Navigation.navConfirmationStatus != "Not yet confirmed")
            {
                btn_confirm.IsEnabled = false;
                btn_confirm.Visibility = Visibility.Collapsed;
            }

        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PivotItem selectedItem = (PivotItem)e.AddedItems[0];
            String pivotTag = (String)selectedItem.Tag;
            if (pivotTag.Equals("satu"))
            {
                elip1.Visibility = Visibility.Visible;
                elip2.Visibility = Visibility.Collapsed;
               
               
                elip1_Copy.Visibility = Visibility.Collapsed;
                elip2_Copy.Visibility = Visibility.Visible;
                
                ViewModelDetailDonation detaildonation = new ViewModelDetailDonation();
                this.DataContext = detaildonation;
            }
            else if (pivotTag.Equals("dua"))
            {
                elip1.Visibility = Visibility.Collapsed;
                elip2.Visibility = Visibility.Visible;
               

                elip1_Copy.Visibility = Visibility.Visible;
                elip2_Copy.Visibility = Visibility.Collapsed;
               
                ViewModelOrganization org = new ViewModelOrganization();
                this.DataContext = org;
            }
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageKeranjangDonasi.xaml", UriKind.Relative));
        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/Views/PageKonfirmasi.xaml", UriKind.Relative));
            
            //ViewModelKonfirmasi konfirmasi = new ViewModelKonfirmasi();
            //this.DataContext = konfirmasi;
        }

    }
}