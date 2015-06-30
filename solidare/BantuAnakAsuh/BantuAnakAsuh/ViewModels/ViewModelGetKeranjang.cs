using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;  
using System.Data.Linq;
using BantuAnakAsuh.Models;
using RestSharp;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using BantuAnakAsuh.Helper;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using BantuAnakAsuh.Common;
using System.Windows.Input;
using BantuAnakAsuh.Views;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelGetKeranjang : ViewModelBase
    {

        private ObservableCollection<ModelGetKeranjang> collectionGetKeranjang = new ObservableCollection<ModelGetKeranjang>();
        public ObservableCollection<ModelGetKeranjang> CollectionGetKeranjang
        {
            get { return collectionGetKeranjang; }
            set
            {
                if (this.collectionGetKeranjang != value)
                {
                    collectionGetKeranjang = value;
                    RaisePropertyChanged("");
                }
            }
        }


        public ViewModelGetKeranjang() 
        {
            this.LoadUrl();
        }
      

        private void LoadUrl()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/donation/get_donation.php", Method.POST);

                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", "871");
                request.AddParameter("token", ")GYaS6^cO!NL$eQDuzFZB952f");

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {
                    ShellToast toast = new ShellToast();
                    toast.Title = "Status Upload";
                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());

                    foreach (JObject item in JItem)
                    {
                        ModelGetKeranjang modelGetKeranjang = new ModelGetKeranjang();
                        modelGetKeranjang.id_donation = item.SelectToken("id_donation").ToString();
                        modelGetKeranjang.id_fosterchildren = item.SelectToken("id_fosterchildren").ToString();
                        modelGetKeranjang.children_name = item.SelectToken("children_name").ToString();
                        modelGetKeranjang.photo = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item.SelectToken("photo").ToString();
                        modelGetKeranjang.pre_donation_time = item.SelectToken("pre_donation_time").ToString();
                        modelGetKeranjang.cha_org_name = item.SelectToken("cha_org_name").ToString();
                        collectionGetKeranjang.Add(modelGetKeranjang);

                    }
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (result.Equals("success"))
                        {
                            PageKeranjangDonasi keranjangdonasi = new PageKeranjangDonasi();
                            keranjangdonasi.LoadingBar.Visibility = Visibility.Collapsed;
                           
                        }
                        else
                        {
                           
                        }
                    
                    }
                    else
                    {
                        //error ocured during upload

                        toast.Content = "Your posting failed. Please check the Internet connection.";
                        toast.Show();
                        //progressBar1.Visibility = System.Windows.Visibility.Visible;

                    }
                });
            }
            catch { }
        }


        public static bool konek = true;


        private int listIndex = -1;
        public int ListIndex
        {
            get { return listIndex; }
            set
            {
                if (listIndex != value)
                    listIndex = value;
                RaisePropertyChanged("");
            }
        }

        public ICommand SetDonasi
        {
            get
            {
                return new DelegateCommand(SetIdDonation, CanSetIdDonation);
            }
        }

        private bool CanSetIdDonation(object arg)
        {
            return true;
        }

        private void SetIdDonation(object obj)
        {
            ModelGetKeranjang SelectedItem = obj as ModelGetKeranjang;

            if (SelectedItem != null)
                Navigation.idDonation = SelectedItem.id_donation;

            listIndex = -1;
        }

    }
}