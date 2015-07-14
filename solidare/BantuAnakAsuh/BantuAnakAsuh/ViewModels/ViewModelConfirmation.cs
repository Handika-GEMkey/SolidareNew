using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BantuAnakAsuh.Models;
using RestSharp;
using Newtonsoft.Json.Linq;
using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using System.Windows;


namespace BantuAnakAsuh.ViewModels
{
    class ViewModelConfirmation : ViewModelBase
    {

        public ViewModelConfirmation()
        {
            LoadUrl();

        }


        private ObservableCollection<ModelNotificationConfirmation> collectionNotifConf = new ObservableCollection<ModelNotificationConfirmation>();
        public ObservableCollection<ModelNotificationConfirmation> CollectionNotifConf
        {
            get { return collectionNotifConf; }
            set
            {
                if (this.collectionNotifConf != value)
                {
                    collectionNotifConf = value;
                    RaisePropertyChanged("");
                }
            }
        }

        private void LoadUrl()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/notification/notification.php", Method.POST);
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {

                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();

                    if (result == "success")
                    {
                        JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                        foreach (JObject item in JItem)
                        {
                            ModelNotificationConfirmation modelNotifConf = new ModelNotificationConfirmation();
                            modelNotifConf.Message = item["message"].ToString();
                           
                            collectionNotifConf.Add(modelNotifConf);
                        }
                    }
                    else
                    {

                       
                    }
                });
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("An error occured!");
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }
            catch
            {
                
            }

        }
    }
}
