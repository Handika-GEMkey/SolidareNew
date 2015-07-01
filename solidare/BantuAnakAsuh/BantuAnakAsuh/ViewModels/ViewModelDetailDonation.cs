using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using BantuAnakAsuh.Views;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelDetailDonation : ModelDetailDonation
    {



        public ViewModelDetailDonation() 
        {
            this.LoadUrl();
        }
      

        private void LoadUrl()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/donation/get_donation.php", Method.POST);

                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);

                request.AddParameter("id_donation", Navigation.idDonation);

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
                        
                        Id_donation = item.SelectToken("id_donation").ToString();
                        Id_fosterchildren = item.SelectToken("id_fosterchildren").ToString();
                        Children_name = item.SelectToken("children_name").ToString();
                        Photo = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item.SelectToken("photo").ToString();
                        Cost = item.SelectToken("cost").ToString();
                        Pre_donation_time = item.SelectToken("pre_donation_time").ToString();
                        Cha_org_name = item.SelectToken("cha_org_name").ToString();
                        Id_cha_org = item.SelectToken("id_cha_org").ToString();
                        Navigation.navId_cha_org = Id_cha_org;
                        Navigation.navPhotoChild = Photo;
                        Navigation.navNameChild = Children_name;
                        Navigation.idDonation = Id_donation;
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
