using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelAnakAsuh : ViewModelBase
    {
        private int listIndex = -1;

        public ViewModelAnakAsuh()
        {
            this.LoadUrlDonorProfile();
            this.LoadUrlFosterChildren();
            
        }

        private ObservableCollection<ModelProfileDonatur> collectionAnakAsuh = new ObservableCollection<ModelProfileDonatur>();
        public ObservableCollection<ModelProfileDonatur> CollectionAnakAsuh
        {
            get { return collectionAnakAsuh; }
            set
            {
                if (this.collectionAnakAsuh != value)
                {
                    collectionAnakAsuh = value;
                    RaisePropertyChanged("");
                }
            }
        }

        private void LoadUrlFosterChildren()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/donors/donors_fosterchildren.php", Method.POST);
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {

                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                    foreach (JObject item in JItem)
                    {
                        ModelProfileDonatur modelAnakAsuh = new ModelProfileDonatur();
                        modelAnakAsuh.id_fosterchildren = item["id_fosterchildren"].ToString();
                        modelAnakAsuh.name = item["name"].ToString();
                        modelAnakAsuh.study_level = item["study_level"].ToString();
                        modelAnakAsuh.children_status = item["children_status"].ToString();
                        modelAnakAsuh.cha_org_name = item["cha_org_name"].ToString();
                        Navigation.id_fosterchildren = modelAnakAsuh.id_fosterchildren.ToString();
                        collectionAnakAsuh.Add(modelAnakAsuh);
                    }
                });
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }

        }

        private void LoadUrlDonorProfile()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/donors/profile.php", Method.POST);
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {

                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                    foreach (JObject item in JItem)
                    {
                        Name = item.SelectToken("name").ToString();
                        Photo_donors = URL.BASE3 + "modul/mod_OrangTuaAsuh/photo/" + item["photo"].ToString();
                        
                    }
                });
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }

        }
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private String photo_donors;
        public String Photo_donors
        {
            get { return photo_donors; }
            set { photo_donors = value; RaisePropertyChanged(""); }
        }

        public ICommand SetAnakAsuh
        {
            get
            {
                return new DelegateCommand(SetIdAnak, CanSetIdAnak);
            }
        }

        private bool CanSetIdAnak(object arg)
        {
            return true;
        }

        private void SetIdAnak(object obj)
        {
            ModelProfileDonatur SelectedItem = obj as ModelProfileDonatur;

            if (SelectedItem != null)
                Navigation.id_fosterchildren = SelectedItem.id_fosterchildren;

            listIndex = -1;
        }
    }
}
