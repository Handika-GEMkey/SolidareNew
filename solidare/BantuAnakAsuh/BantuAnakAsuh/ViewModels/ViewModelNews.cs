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
    class ViewModelNews : ViewModelBase
    {
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

        public ViewModelNews()
        {
            this.LoadUrl();
        }

        private void LoadUrl()
        {
            WebClient clientlistnews = new WebClient();
            clientlistnews.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadNewsList);
            clientlistnews.DownloadStringAsync(new Uri(URL.BASE3 + "api/news/news.php"));
        }

        public static bool konek = true;

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

            listIndex = -1;
        }
     
    }
}