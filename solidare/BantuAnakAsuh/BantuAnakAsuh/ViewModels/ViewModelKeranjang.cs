using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelKeranjang : ViewModelBase
    {
        private ObservableCollection<ModelKeranjang> collectionKeranjang = new ObservableCollection<ModelKeranjang>();
        public ObservableCollection<ModelKeranjang> CollectionKeranjang
        {
            get { return collectionKeranjang; }
            set
            {
                if (this.collectionKeranjang != value)
                {
                    collectionKeranjang = value;
                    RaisePropertyChanged("");
                }
            }

        }

        private String total_donasi;

        public String Total_donasi
        {
            get { return total_donasi; }
            set { total_donasi = value; RaisePropertyChanged(""); }
        }

        public ViewModelKeranjang()
        {
            this.LoadUrl();
        }

        private void LoadUrl()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/donation/donation_list.php", Method.POST);

                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);
                request.AddParameter("id_fosterchildren", Navigation.navIdAnak);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {
                    ShellToast toast = new ShellToast();
                    toast.Title = "Status Upload";
                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    String message = jRoot.SelectToken("message").ToString();
                    if (result == "failed")
                    {
                        MessageBox.Show(message);
                    }
                    else
                    {
                        ModelKeranjang modelKeranjang = new ModelKeranjang();
                        modelKeranjang.id_donation = jRoot.SelectToken("id_donation").ToString();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (result.Equals("success"))
                            {

                                if (MessageBox.Show(message) == MessageBoxResult.OK)
                                {
                                    var frame = App.Current.RootVisual as PhoneApplicationFrame;
                                    frame.Navigate(new Uri("/Views/PageKeranjangDonasi.xaml", UriKind.Relative));
                                }
                            }
                            else
                            {
                                MessageBox.Show("Request has done, check your Donation List");
                            }
                        }
                        else
                        {
                            //error ocured during upload
                            toast.Content = "Your posting failed. Please check the Internet connection.";
                            toast.Show();

                            //progressBar1.Visibility = System.Windows.Visibility.Visible;

                        }
                    }
                });
            }
            catch { 
            }
        }

    }
}
