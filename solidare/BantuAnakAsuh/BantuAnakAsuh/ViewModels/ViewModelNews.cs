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
    class ViewModelNews : ViewModelBase
    {
        private String Result;
        public bool konek = true;
        ModelNews modelNews = new ModelNews();

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
            this.LoadUrlNews();
        }

        private void LoadUrlNews()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/news/news.php", Method.POST);
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);
                request.AddParameter("id_news", Navigation.navIdNews);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {

                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    if (result == "failed")
                    {
                        MessageBox.Show("Failed to display!");
                    }
                    else
                    {
                        JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                        foreach (JObject item in JItem)
                        {
                            Id_news = item["id_news"].ToString();
                            Title = item["title"].ToString();
                            Photo = URL.BASE3 + "modul/mod_News/gambar/" + item["photo"].ToString();
                            Post_date = item["post_date"].ToString();
                            Description = item["description"].ToString();
                            Post_by = item["post_by"].ToString();

                        }
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

        private String id_news;

        public String Id_news
        {
            get { return id_news; }
            set { id_news = value; RaisePropertyChanged(""); }
        }
        private String title;

        public String Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(""); }
        }
        private String photo;

        public String Photo
        {
            get { return photo; }
            set { photo = value; RaisePropertyChanged(""); }
        }
        private String post_date;

        public String Post_date
        {
            get { return post_date; }
            set { post_date = value; RaisePropertyChanged(""); }
        }
        private String description;

        public String Description
        {
            get { return description; }
            set { description = value; RaisePropertyChanged(""); }
        }
        private String post_by;

        public String Post_by
        {
            get { return post_by; }
            set { post_by = value; RaisePropertyChanged(""); }
        }

    }
}