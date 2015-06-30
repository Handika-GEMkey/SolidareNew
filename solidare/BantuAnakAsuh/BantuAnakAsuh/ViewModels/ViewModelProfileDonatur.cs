using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelProfileDonatur : ViewModelBase
    {
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

        public ViewModelProfileDonatur()
        {
            this.LoadUrl();
            this.LoadUrlNews();
            this.LoadUrlAnakAsuh();
        }

        private void LoadUrl()
        {
            WebClient clientprofiledonatur = new WebClient();
            clientprofiledonatur.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadProfileDonatur);
            clientprofiledonatur.DownloadStringAsync(new Uri(URL.BASE3 + "api/donatur/donatur.php?id_donatur=" + Navigation.navIdLogin + "?nocache=" + Guid.NewGuid()));

        }

        private void LoadUrlAnakAsuh()
        {

            WebClient clientanakasuhdonatur = new WebClient();
            clientanakasuhdonatur.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadListAnak);
            clientanakasuhdonatur.DownloadStringAsync(new Uri(URL.BASE3 + "api/profile/profile.php?id_donatur=" + Navigation.navIdLogin + "?nocache=" + Guid.NewGuid()));

        }

        public bool konek = true;

        private void DownloadProfileDonatur(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JObject jresult = JObject.Parse(e.Result);
                ModelLogin modelLogin = new ModelLogin();
                modelLogin.Id_donatur = jresult.SelectToken("id_donatur").ToString();
                modelLogin.Nama_donatur = jresult.SelectToken("nama_donatur").ToString();
                modelLogin.Foto_donatur = URL.BASE3 + "modul/mod_OrangTuaAsuh/photo/" + jresult.SelectToken("foto_donatur").ToString();
                modelLogin.No_tlp = jresult.SelectToken("no_tlp").ToString();
                modelLogin.Email_donatur = jresult.SelectToken("email_donatur").ToString();
                CollectionDonatur.Add(modelLogin);
                
            }
            catch
            {
                konek = false;
            }
        }

        private void DownloadListAnak(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JObject jresult = JObject.Parse(e.Result);
                String result = jresult.SelectToken("result").ToString();
                if (result.Equals("sukses"))
                {
                    JArray JItem = JArray.Parse(jresult.SelectToken("item").ToString());
                    foreach (JObject item in JItem)
                    {
                        ModelProfileDonatur modelProfileDonatur = new ModelProfileDonatur();
                        modelProfileDonatur.id_anak_asuh = item.SelectToken("id_anak_asuh").ToString();
                        modelProfileDonatur.nama_anak_asuh = item.SelectToken("nama_anak_asuh").ToString();
                        modelProfileDonatur.foto_anak = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item.SelectToken("foto_anak").ToString();
                        modelProfileDonatur.jenjang_pendidikan = item.SelectToken("jenjang_pendidikan").ToString();
                        collectionAnakAsuh.Add(modelProfileDonatur);
                    }
                }
                else
                {
                    String hasil = jresult.SelectToken("message").ToString();
                    No_anak = "You don't have foster children.";
                }
            }
            catch
            {
                konek = false;
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
                Navigation.navIdAnak = SelectedItem.id_anak_asuh;

            listIndex = -1;
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

 

        private void LoadUrlNews()
        {
            WebClient clientlistnews = new WebClient();
            clientlistnews.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadNewsList);
            clientlistnews.DownloadStringAsync(new Uri(URL.BASE3 + "api/news/news.php"));
        }

        public static bool konek1 = true;

        private void DownloadNewsList(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JObject jresult = JObject.Parse(e.Result);
                JArray JItem = JArray.Parse(jresult.SelectToken("item").ToString());
                foreach (JObject item in JItem)
                {
                    ModelNews modelNews = new ModelNews();
                    modelNews.id_news = item.SelectToken("id_news").ToString();
                    modelNews.judul_news = item.SelectToken("judul_news").ToString();
                    modelNews.gbr_news = URL.BASE3 + "modul/mod_News/gambar/" + item.SelectToken("gbr_news").ToString();
                    modelNews.tanggal_post = item.SelectToken("tanggal_post").ToString();
                    modelNews.jam_post = item.SelectToken("jam_post").ToString();
                    collectionNews.Add(modelNews);
                }
            }
            catch
            {
                konek1 = false;
            }
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
