using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BantuAnakAsuh.Helper;

namespace BantuAnakAsuh.Views
{
    public partial class PageFilter : PhoneApplicationPage
    {
        String[] listStatusArray = { "Choose Status", "Orphan", "Orphans", "Poor children" };
        String[] listJKArray = { "Choose Your Gender", "Male", "Female" };
        String[] listPendidikanArray = { "Choose Education Major", "SD", "SMP", "SMA", "SMK" };

        public PageFilter()
        {
            InitializeComponent();
            this.listStatus.ItemsSource = listStatusArray;
            this.listPendidikan.ItemsSource = listPendidikanArray;
            this.listJenisKelamin.ItemsSource = listJKArray;
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        }

        private void apbarFilter_Click(object sender, EventArgs e)
        {
            
            if (listStatus.SelectedItem.ToString().Equals("Pilih Status Anak") && listJenisKelamin.SelectedItem.ToString().Equals("Pilih Jenis Kelamin") & listPendidikan.SelectedItem.ToString().Equals("Pilih Jenjang Pendidikan"))
            {
                MessageBox.Show("Filter criteria is not appropriate.");
            }
            else
            {
                Navigation.Fill_Status = listStatus.SelectedItem.ToString();
                Navigation.Fill_Jk = listJenisKelamin.SelectedItem.ToString();
                Navigation.Fill_Jenjang = listPendidikan.SelectedItem.ToString();
                NavigationService.Navigate(new Uri("/Views/PageHasilFilter.xaml", UriKind.Relative));
            }
            
        }
    }
}