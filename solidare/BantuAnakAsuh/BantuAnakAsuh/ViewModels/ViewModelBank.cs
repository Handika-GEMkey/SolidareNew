﻿using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelBank : ModelBank
    {
        private ObservableCollection<ModelBank> collectionBank = new ObservableCollection<ModelBank>();
        public ObservableCollection<ModelBank> CollectionBank
        {
            get { return collectionBank; }
            set
            {
                if (this.collectionBank != value)
                {
                    collectionBank = value;
                    RaisePropertyChanged("");
                }
            }
        }


        public ViewModelBank() 
        {
            this.LoadUrl();
        }


        private void LoadUrl()
        {
            Photo = Navigation.navPhotoChild;
            Children_name = Navigation.navNameChild;
            Id_donation = Navigation.idDonation;
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/charityorganization/charity_account.php", Method.POST);

                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", "871");
                request.AddParameter("token", ")GYaS6^cO!NL$eQDuzFZB952f");

                request.AddParameter("id_cha_org", Navigation.navId_cha_org);

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
                        ViewModelBank bank = new ViewModelBank();
                        bank.Id_account = item.SelectToken("id_account").ToString();
                        bank.Bank = item.SelectToken("bank").ToString();
                        bank.Account_number = item.SelectToken("account_number").ToString();
                        bank.Account_name = item.SelectToken("account_name").ToString();
                        CollectionBank.Add(bank);
                    }
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (result.Equals("success"))
                        {
                           

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

    }
}
