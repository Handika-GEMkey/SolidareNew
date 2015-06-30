using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using BantuAnakAsuh.ViewModels;
using BantuAnakAsuh.Helper;
using System.Xml.Linq;
using BantuAnakAsuh.Models;
using Newtonsoft.Json.Linq;
using System.Windows.Input;
using BantuAnakAsuh.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace BantuAnakAsuh.Views
{
    public partial class PageSearch : PhoneApplicationPage
    {
        
            
        public PageSearch()
        {
            InitializeComponent();
            
            ViewModelSearch vms = new ViewModelSearch(Navigation.Cari);
            this.DataContext = vms;
            progresBarSearch.Visibility = Visibility.Collapsed;
            if (!Navigation.Cari.Equals(""))
            {
                textBlockResult.Visibility = Visibility.Visible;
                textBlockResult.Text = "Result for " + Navigation.Cari;
            }
            else
            {
                textBlockResult.Visibility = Visibility.Collapsed;
                textBlockResult.Text = "";
            }
            Navigation.From_Page = "Search";
        }

        private void textboxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textboxSearch.Text == "Cari berdasarkan nama")
            {
                textboxSearch.Text = String.Empty;
                SolidColorBrush Brush2 = new SolidColorBrush();
                Brush2.Color = Colors.DarkGray;
                textboxSearch.Foreground = Brush2;
            }
        }

        private void textboxSearch_LostFocus(object sender, RoutedEventArgs e)
        {
                if (textboxSearch.Text == String.Empty)
                {
                    textboxSearch.Text = "Cari berdasarkan nama";
                    SolidColorBrush Brush2 = new SolidColorBrush();
                    Brush2.Color = Colors.LightGray;
                    textboxSearch.Foreground = Brush2;
                }
        }

        private void apBarSeacrh_Click(object sender, EventArgs e)
        {
            Navigation.Cari = textboxSearch.Text;
            if (Navigation.Cari.Equals("Cari berdasarkan nama"))
            {
                Navigation.Cari = "";
            }
            progresBarSearch.Visibility = Visibility.Visible;
            textBlockResult.Visibility = Visibility.Collapsed;
            NavigationService.Navigate(new Uri(NavigationService.Source + "?Refresh=true", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageDonasi.xaml", UriKind.Relative));
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
        }
    }
}