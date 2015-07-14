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

namespace BantuAnakAsuh.Views
{
    public partial class PageDetailAnak : PhoneApplicationPage
    {
        public PageDetailAnak()
        {
            InitializeComponent();
            ViewModelDetailAnakAsuh vmdaa = new ViewModelDetailAnakAsuh();
            this.DataContext = vmdaa;
        }

        private void appBarLetter_Click(object sender, EventArgs e)
        {

        }

        private void appBarReport_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageRapotAnak.xaml", UriKind.Relative));
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageProfileDonatur.xaml", UriKind.Relative));
        }
    }
}