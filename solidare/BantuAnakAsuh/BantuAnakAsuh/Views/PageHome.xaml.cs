using System;
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
    public partial class PageHome : PhoneApplicationPage
    {
        public PageHome()
        {
            InitializeComponent();
            ViewModelDonasi vmd = new ViewModelDonasi();
            this.DataContext = vmd;
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
        }

        private void apbarNearby_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageNearby.xaml", UriKind.Relative));
        }

        private void apbarFilter_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageFilter.xaml", UriKind.Relative));
        }

        private void apbarSearch_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageSearch.xaml", UriKind.Relative));
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //this.NavigationService.RemoveBackEntry();
            //base.OnBackKeyPress(e);
            //e.Cancel = true;
            System.Windows.Application.Current.Terminate();
           
        }
    }
}