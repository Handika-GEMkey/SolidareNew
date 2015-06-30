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
using BantuAnakAsuh.ViewModels;
using BantuAnakAsuh.Models;

namespace BantuAnakAsuh.Views
{
    public partial class PageMenu : PhoneApplicationPage
    {
       
        ModelLogin modelLogin = new ModelLogin();
        public PageMenu()
        {
            InitializeComponent();
            ViewModelProfileDonatur vmpd = new ViewModelProfileDonatur();
            this.DataContext = vmpd;
        }

        //private void LoadDonaturfromIsolatedStorage()
        //{
        //    using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
        //    {
        //        using (IsolatedStorageFileStream rawStream = isf.OpenFile())
        //        {
        //            StreamWriter writer = new StreamWriter(rawStream);
        //            writer.Write(modelLogin.Id_donatur);
        //            writer.Close();
        //        }
        //    }
        //}
        private void stackBerita_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageNews.xaml", UriKind.Relative));
        }

        private void stackDonasi_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageDonasi.xaml", UriKind.Relative));
        }

        private void stackKonfirmasi_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageKonfirmasi.xaml", UriKind.Relative));
        }

        private void stackLaporan_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageLaporanTransaksi.xaml", UriKind.Relative));
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
        }

        private void stackTentang_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageTentang.xaml", UriKind.Relative));
        }

        private void stackKonsultasi_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageKonsultasi.xaml", UriKind.Relative));
        }

        private void stackRapor_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageRapotAnak.xaml", UriKind.Relative));
        }

        private void stackLogout_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream rawStream = isf.CreateFile("id_donatur"))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(string.Empty);
                    writer.Close();
                }

                using (IsolatedStorageFileStream rawStream = isf.CreateFile("username"))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(string.Empty);
                    writer.Close();
                }

                using (IsolatedStorageFileStream rawStream = isf.CreateFile("nama_donatur"))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(string.Empty);
                    writer.Close();
                }

                using (IsolatedStorageFileStream rawStream = isf.CreateFile("email_donatur"))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(string.Empty);
                    writer.Close();
                }

                using (IsolatedStorageFileStream rawStream = isf.CreateFile("no_tlp"))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(string.Empty);
                    writer.Close();
                }

                using (IsolatedStorageFileStream rawStream = isf.CreateFile("jk_donatur"))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(string.Empty);
                    writer.Close();
                }

                using (IsolatedStorageFileStream rawStream = isf.CreateFile("tgl_register"))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(string.Empty);
                    writer.Close();
                }

                using (IsolatedStorageFileStream rawStream = isf.CreateFile("status_donatur"))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(string.Empty);
                    writer.Close();
                }
            }
            NavigationService.Navigate(new Uri("/Views/PageStart.xaml", UriKind.Relative));
        }

        private void stackProfile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageProfileDonatur.xaml", UriKind.Relative));
        }

        private void stackGBSA_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageProfilGBSA.xaml", UriKind.Relative));
        }
        private void stackRekomendasi_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageRekomendasi.xaml", UriKind.Relative));
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //this.NavigationService.RemoveBackEntry();
            //base.OnBackKeyPress(e);
            //e.Cancel = true;
            
            string caption = "Exit?";
            string message = "Are you sure you want to exit?";
            e.Cancel = MessageBoxResult.Cancel == MessageBox.Show(message, caption, MessageBoxButton.OKCancel);
            if (e.Cancel!=true)
            {
                System.Windows.Application.Current.Terminate();
            }
            

        }
        private void stackRekomendasiLingkungan_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageRekomendasiLingkungan.xaml", UriKind.Relative));
        }
        private void stackDonationCart_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageKeranjangDonasi.xaml", UriKind.Relative));
        }

        
    }
}