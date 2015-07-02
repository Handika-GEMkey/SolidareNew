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
    class ViewModelProfileDonatur : ViewModelBase
    {
        ModelLogin modelLogin = new ModelLogin();

        Encrypt ec = new Encrypt();

        private String Result;
        public bool konek = true;

        private String no_anak;

        public String No_anak
        {
            get { return no_anak; }
            set { no_anak = value; RaisePropertyChanged(""); }
        }

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

        private ObservableCollection<ModelNews> collectionNews = new ObservableCollection<ModelNews>();
        public ObservableCollection<ModelNews> CollectionNews
        {
            get { return collectionNews; }
            set
            {
                if (this.collectionNews != value)
                {
                    collectionNews = value;
                    RaisePropertyChanged("");
                }
            }

        }
        
        public ViewModelProfileDonatur()
        {
            this.LoadUrlDonorProfile();
            this.LoadUrlFosterChildren();
            this.LoadUrlNews();
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
                        Title_name_profile = modelLogin.Nama_donatur.ToUpper().ToString()+" PROFILE";
                        Photo_donors = modelLogin.Photo.ToString();
                    }
                });
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }

        }

        private String title_name_profile;

        public String Title_name_profile
        {
            get { return title_name_profile; }
            set { title_name_profile = value; RaisePropertyChanged(""); }
        }

        private String photo_donors;

        public String Photo_donors
        {
            get { return photo_donors; }
            set { photo_donors = value; RaisePropertyChanged(""); }
        }

        private void LoadUrlNews()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/news/news.php", Method.POST);
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
                        ModelNews modelNews = new ModelNews();
                        modelNews.id_news = item["id_news"].ToString();
                        modelNews.title = item["title"].ToString();
                        modelNews.photo = URL.BASE3 + "modul/mod_News/gambar/" + item["photo"].ToString();
                        modelNews.post_date = item["post_date"].ToString();
                        modelNews.description = item["description"].ToString();
                        modelNews.post_by = item["post_by"].ToString();

                        collectionNews.Add(modelNews);
                    }

                });
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }

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
                        Navigation.idProgram = modelAnakAsuh.id_cha_org.ToString();
                        collectionAnakAsuh.Add(modelAnakAsuh);
                    }
                });
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }

        }

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
                Navigation.navIdAnak = SelectedItem.id_fosterchildren;

            listIndex = -1;
        }

        private int listIndex1 = -1;
        public int ListIndex1
        {
            get { return listIndex1; }
            set
            {
                if (listIndex1 != value)
                    listIndex = value;
                RaisePropertyChanged("");
            }
        }

        public ICommand SetNews
        {
            get
            {
                return new DelegateCommand(SetIdNews, CanSetIdNews);
            }
        }

        private bool CanSetIdNews(object arg)
        {
            return true;
        }

        private void SetIdNews(object obj)
        {
            ModelNews SelectedItem = obj as ModelNews;

            if (SelectedItem != null)
                Navigation.navIdNews = SelectedItem.id_news;

            listIndex1 = -1;
        }

    }
}
