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
using BantuAnakAsuh.Models;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;

namespace BantuAnakAsuh.Views
{
    public partial class PageProfileAnak : PhoneApplicationPage
    {

        bool isOpenNavDraw;
        double marginLeft;

        public PageProfileAnak()
        {
            InitializeComponent();
            ViewModelDetailAnakAsuh vmdaa = new ViewModelDetailAnakAsuh();
            this.DataContext = vmdaa;
           
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {

            NavigationService.Navigate(new Uri("/Views/NewHomePage.xaml", UriKind.Relative));
           
           

        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        }

        private void apbarHome_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageDonasi.xaml", UriKind.Relative));
        }

        private void btnLihatKeranjang_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageKeranjangDonasi.xaml", UriKind.Relative));
        }

        private void btnAddDonasi_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
           
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Donationpop.SlideNavBarOpen.Begin();
            marginLeft = 0;
            Donationpop.Margin = new Thickness(0, 0, 0, 0);
            isOpenNavDraw = true;
            Donationpop.white_wall.Visibility = Visibility.Visible;
            
        }

        
    }
}