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
    public partial class PageRapotAnak : PhoneApplicationPage
    {
        public PageRapotAnak()
        {
            InitializeComponent();
            ViewModelRapot vmr = new ViewModelRapot();
            this.DataContext = vmr;
        }

        private void apbarHome_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        }

        private void img_download_report_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            //HyperlinkButton h = (HyperlinkButton)sender;
            //string strTest = h.NavigateUri.AbsoluteUri;
            //if (strTest == "http://www.microsoft.com/")
            //{
            //    h.NavigateUri = URL.BASE3 + "modul/mod_Laporan/laporan/" + modelRapot.report_file;
            //}
        }
    }
}