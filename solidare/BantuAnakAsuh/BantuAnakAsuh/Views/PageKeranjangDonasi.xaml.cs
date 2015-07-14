using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BantuAnakAsuh.Models;
using System.Text;
using BantuAnakAsuh.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO.IsolatedStorage;
using System.IO;
using BantuAnakAsuh.Helper;

namespace BantuAnakAsuh.Views
{
    public partial class PageKeranjangDonasi : PhoneApplicationPage, INotifyCollectionChanged
    {
        ViewModelGetKeranjang vmk;
        public PageKeranjangDonasi()
        {
            InitializeComponent();
            vmk = new ViewModelGetKeranjang();
            this.DataContext = vmk;
        }
        

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        }

        int idhapus;
        private void Delete_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //int zxc = ListAnak.SelectedIndex;
            //vmk = new ViewModelKeranjang();
            //IList<ModelGetKeranjang> Anaklist = this.GetKeranjang();
            //StringBuilder details = new StringBuilder();
            //int i = 0;
            //foreach (ModelGetKeranjang mgk in Anaklist)
            //{
            //    if (i == zxc)
            //    {
            //        idhapus = mgk.id;
            //    }
            //    i++;
            //}

            //using (ViewModelGetKeranjang vmgk = new ViewModelGetKeranjang("Data Source=isostore:/ListAnak.sdf"))
            //{
            //    IQueryable<ModelGetKeranjang> query = from std in vmgk.Anak where std.id == idhapus select std;
            //    ModelGetKeranjang delete = query.FirstOrDefault();
            //    vmgk.Anak.DeleteOnSubmit(delete);
            //    vmgk.SubmitChanges();
            //    MessageBox.Show("Deleted Successfully");
                
            //    NavigationService.Navigate(new Uri("/Views/PageKeranjangDonasi.xaml", UriKind.Relative));
            //    vmk = new ViewModelKeranjang();
            //    this.DataContext = vmk;
            //}

            //var fullist = vmk.CollectionKeranjang;
            //object obj = ListAnak.SelectedItem;
            //if (obj is ModelKeranjang)
            //{
            //    fullist.Remove((ModelKeranjang)obj);
            //    ListAnak.ItemsSource = fullist;

            //    int count = Convert.ToInt16(Navigation.countKeranjang);

            //    Navigation.countKeranjang = (count - 1).ToString();
            //    using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            //    {
            //        using (IsolatedStorageFileStream rawStream = isf.CreateFile("countDonasi"))
            //        {
            //            StreamWriter writer = new StreamWriter(rawStream);
            //            writer.Write(Navigation.countKeranjang);
            //            writer.Close();
            //        }
            //    }
            //    NavigationService.Navigate(new Uri("/Views/PageKeranjangDonasi.xaml", UriKind.Relative));
            //}
            
            
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

      

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/DetailDonation.xaml", UriKind.Relative));
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/coba.xaml", UriKind.Relative));
        }
    }
}