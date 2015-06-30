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

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelDetailNews : ViewModelBase
    {
        private String id_news;

        public String Id_news
        {
            get { return id_news; }
            set { id_news = value; RaisePropertyChanged(""); }
        }
        private String judul_news;

        public String Judul_news
        {
            get { return judul_news; }
            set { judul_news = value; RaisePropertyChanged(""); }
        }
        private String gbr_news;

        public String Gbr_news
        {
            get { return gbr_news; }
            set { gbr_news = value; RaisePropertyChanged(""); }
        }
        private String tanggal_post;

        public String Tanggal_post
        {
            get { return tanggal_post; }
            set { tanggal_post = value; RaisePropertyChanged(""); }
        }
        private String jam_post;

        public String Jam_post
        {
            get { return jam_post; }
            set { jam_post = value; RaisePropertyChanged(""); }
        }
        private String deskripsi_news;

        public String Deskripsi_news
        {
            get { return deskripsi_news; }
            set { deskripsi_news = value; RaisePropertyChanged(""); }
        }

        public ViewModelDetailNews()
        {
            this.LoadUrl();
        }

        private void LoadUrl()
        {
            WebClient clientlistnews = new WebClient();
            clientlistnews.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadNewsList);
            clientlistnews.DownloadStringAsync(new Uri(URL.BASE3 + "api/detailnews/detailnews.php?id_news="+Navigation.navIdNews));
        }

        public bool konek = true;

        private void DownloadNewsList(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JObject jresult = JObject.Parse(e.Result);
                Id_news = jresult.SelectToken("id_news").ToString();
                Judul_news = jresult.SelectToken("judul_news").ToString();
                Gbr_news = URL.BASE3 + "modul/mod_News/gambar/" + jresult.SelectToken("gbr_news").ToString();
                Tanggal_post = jresult.SelectToken("tanggal_post").ToString();
                Jam_post = jresult.SelectToken("jam_post").ToString();
                Deskripsi_news = jresult.SelectToken("deskripsi_news").ToString();
                
            }
            catch
            {
                konek = false;
            }
        }

    }
}
