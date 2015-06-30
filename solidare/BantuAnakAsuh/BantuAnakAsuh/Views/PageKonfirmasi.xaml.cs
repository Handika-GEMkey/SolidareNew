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
using System.Globalization;
using BantuAnakAsuh.Helper;
using System.IO.IsolatedStorage;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BantuAnakAsuh.Views
{
    public partial class PageKonfirmasi : PhoneApplicationPage
    {
        public PageKonfirmasi()
        {
            InitializeComponent();
            this.DataContext = new ViewModelKonfirmasi();
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
        }

        public void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");

            //if (TextBoxJmlPmbyrn.Text.Length <= 2)
            //{
            //    TextBoxJmlPmbyrn.Text = Convert.ToDecimal(0).ToString("c");
            //}
            //else
            //{
            //    TextBoxJmlPmbyrn.Text = int.Parse(TextBoxJmlPmbyrn.Text, System.Globalization.NumberStyles.Currency).ToString("c");

            //    TextBoxJmlPmbyrn.SelectionStart = TextBoxJmlPmbyrn.Text.Length;

            //    //Int16 temp = Convert.ToInt16(TextBoxJmlPmbyrn.Text);

            //    //ViewModelKonfirmasi vKonfirmasi = new ViewModelKonfirmasi();
            //    //vKonfirmasi.Jumlah_Pembayaran = temp;
            //}

        }

        private void LoadUrl()
        {
            //LoadID();
            WebClient clientlistdonasi = new WebClient();
            clientlistdonasi.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadDonasiList);
            clientlistdonasi.DownloadStringAsync(new Uri(URL.BASE3 + "api/konfirmasi/getorder.php?id_order=" + listStatus.SelectedItem.ToString()));
        }


        #region load id donatur
        //public void LoadID()
        //{
        //    using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
        //    {
        //        try
        //        {
        //            using (IsolatedStorageFileStream rawStream = isf.OpenFile("id_order", System.IO.FileMode.Open))
        //            {
        //                StreamReader reader = new StreamReader(rawStream);

        //                listStatus.SelectedItem = reader.ReadToEnd();
        //                Navigation.navIdOrder = listStatus.SelectedItem.ToString();
        //                reader.Close();
        //            }
        //        }
        //        catch
        //        {
        //            //data tidak ditemukan
        //        }
        //    }
        //}
        #endregion

        public static bool konek = true;

        private void DownloadDonasiList(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JObject jresult = JObject.Parse(e.Result);
                JArray JItem = JArray.Parse(jresult.SelectToken("item").ToString());
                foreach (JObject item in JItem)
                {

                    TextBoxJmlPmbyrn.Text = item.SelectToken("biaya_donasi").ToString();
                    BankTujuan.Text = item.SelectToken("bank").ToString();
                    BankAccountNumber.Text = item.SelectToken("no_rek").ToString();
                }
            }
            catch
            {
                konek = false;
            }
        }

        private void listStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (listStatus.SelectedItem!=null)
            {
                LoadUrl();    
            }
            
            //this.DataContext = new ViewModelKonfirmasi();
        }
    }
}