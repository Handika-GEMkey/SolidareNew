using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.IO;
using BantuAnakAsuh.Helper;

namespace BantuAnakAsuh.Views
{
    public partial class PageStart4 : PhoneApplicationPage
    {
        string id;
        public PageStart4()
        {
            this.cekLogin();
            InitializeComponent();
        }

        private void cekLogin()
        {
            id = "";
            this.LoadID();

            if (!id.Equals(""))
            {
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    NavigationService.Navigate(new Uri("/Views/PageDonasi.xaml", UriKind.Relative));
                    //this.Loaded += StartedPage_Loaded;
                });
            }
        }

        public void LoadID()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    using (IsolatedStorageFileStream rawStream = isf.OpenFile("id_donatur", System.IO.FileMode.Open))
                    {
                        StreamReader reader = new StreamReader(rawStream);

                        id = reader.ReadToEnd();
                        Navigation.navIdLogin = id;
                        reader.Close();
                    }
                }
                catch
                {
                    //data tidak ditemukan
                }
            }
        }

        private void buttonLogin_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageLogin.xaml", UriKind.Relative));
        }

        private void buttonRegister_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageRegister.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Terminate();
        }
    }
}