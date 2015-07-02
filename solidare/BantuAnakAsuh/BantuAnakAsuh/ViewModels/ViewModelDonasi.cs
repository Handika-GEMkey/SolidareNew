using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using BantuAnakAsuh.Views;
using Microsoft.Phone.Shell;
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
    class ViewModelDonasi : ViewModelBase
    {
        ModelLogin modelLogin = new ModelLogin();

        private ObservableCollection<ModelDonasi> collectionDonasi = new ObservableCollection<ModelDonasi>();
        public ObservableCollection<ModelDonasi> CollectionDonasi
        {
            get { return collectionDonasi; }
            set
            {
                if (this.collectionDonasi != value)
                {
                    collectionDonasi = value;
                    RaisePropertyChanged("");
                }
            }
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

        public ViewModelDonasi()
        {
            //this.LoadUrl();
            this.LoadUrlDonorProfile();
            this.LoadUrlFosterChildren();
        }

        private void LoadUrlFosterChildren()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/fosterchildren/fosterchildren.php", Method.POST);
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
                        modelAnakAsuh.pob = item["pob"].ToString();
                        modelAnakAsuh.dob = item["dob"].ToString();
                        modelAnakAsuh.gender = item["gender"].ToString();
                        modelAnakAsuh.address = item["address"].ToString();
                        modelAnakAsuh.photo = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item["photo"].ToString();
                        modelAnakAsuh.cost = item["cost"].ToString();
                        modelAnakAsuh.children_status = item["children_status"].ToString();
                        modelAnakAsuh.latitude = item["latitude"].ToString();
                        modelAnakAsuh.longitude = item["longitude"].ToString();
                        modelAnakAsuh.study_level = item["study_level"].ToString();
                        modelAnakAsuh.school = item["school"].ToString();
                        modelAnakAsuh.grade = item["grade"].ToString();
                        modelAnakAsuh.parent_name = item["parent_name"].ToString();
                        modelAnakAsuh.parent_address = item["parent_address"].ToString();
                        modelAnakAsuh.jobs = item["jobs"].ToString();
                        modelAnakAsuh.salary = item["salary"].ToString();
                        modelAnakAsuh.id_cha_org = item["id_cha_org"].ToString();
                        modelAnakAsuh.cha_org_name = item["cha_org_name"].ToString();
                        modelAnakAsuh.id_program = item["id_program"].ToString();
                        modelAnakAsuh.program_name = item["program_name"].ToString();

                        collectionAnakAsuh.Add(modelAnakAsuh);
                    }
                });
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }

        }

        private void LoadUrl()
        {
            try
            {

                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/fosterchildren/fosterchildren.php", Method.POST);

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
                    //String id_donasi = jRoot.SelectToken("id_donation").ToString();
                    //String photo_child = jRoot.SelectToken("photo").ToString();
                    JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                    foreach (JObject item in JItem)
                    {
                        ModelDonasi modelDonasi = new ModelDonasi();
                        modelDonasi.id_fosterchildren = item.SelectToken("id_fosterchildren").ToString();
                        modelDonasi.name = item.SelectToken("name").ToString();
                        modelDonasi.photo = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item.SelectToken("photo").ToString();
                        modelDonasi.study_level = item.SelectToken("study_level").ToString();
                        modelDonasi.children_status = item.SelectToken("children_status").ToString();
                        //Navigation.idDonation = id_donasi;
                        //Navigation.navPhotoChild = photo_child;
                        collectionDonasi.Add(modelDonasi);
                    }
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (result.Equals("success"))
                        {
                            NewHomepage homepage = new NewHomepage();
                            homepage.LoadingBar.Visibility = Visibility.Collapsed;
                        }
                        else
                        {

                            MessageBox.Show("Data not found");
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
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }
            finally
            {
                //ProgressVisibiliy = Visibility.Visible;
            }
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
                return new DelegateCommand(SetIdAnak, CanSetIdAnak);
            }
        }

        private bool CanSetIdAnak(object arg)
        {
            return true;
        }

        private void SetIdAnak(object obj)
        {
            ModelDonasi SelectedItem = obj as ModelDonasi;

            if (SelectedItem != null)
                Navigation.navIdAnak = SelectedItem.id_fosterchildren;

            listIndex = -1;
        }

        // Donatur 
        private String id_donatur;
        public String Id_donatur
        {
            get { return id_donatur; }
            set { id_donatur = value; RaisePropertyChanged(""); }
        }

        private String foto_donatur;
        public String Foto_donatur
        {
            get { return foto_donatur; }
            set { foto_donatur = value; RaisePropertyChanged(""); }
        }

        private String no_tlp;
        public String No_tlp
        {
            get { return no_tlp; }
            set { no_tlp = value; RaisePropertyChanged(""); }
        }

        private String nama_donatur;
        public String Nama_donatur
        {
            get { return nama_donatur; }
            set { nama_donatur = value; RaisePropertyChanged(""); }
        }

        private String email_donatur;
        public String Email_donatur
        {
            get { return email_donatur; }
            set { email_donatur = value; RaisePropertyChanged(""); }
        }


        private ObservableCollection<ModelLogin> collectionDonatur = new ObservableCollection<ModelLogin>();
        public ObservableCollection<ModelLogin> CollectionDonatur
        {
            get { return collectionDonatur; }
            set
            {
                if (this.collectionDonatur != value)
                {
                    collectionDonatur = value;
                    RaisePropertyChanged("");
                }
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
                        modelLogin.Username = item["username"].ToString();
                        modelLogin.Nama_donatur = item.SelectToken("name").ToString();
                        modelLogin.Alamat_donatur = item["address"].ToString();
                        modelLogin.Email_donatur = item["email"].ToString();
                        modelLogin.No_tlp = item["phone"].ToString();
                        modelLogin.Photo = URL.BASE3 + "modul/mod_OrangTuaAsuh/photo/" + item["photo"].ToString();
                        modelLogin.Gender = item["gender"].ToString();

                        CollectionDonatur.Add(modelLogin);
                    }
                });
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }

        }
    }
}
