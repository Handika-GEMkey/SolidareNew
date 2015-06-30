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
    class ViewModelRapot : ViewModelBase
    {
        private ObservableCollection<ModelRapot> collectionRapot = new ObservableCollection<ModelRapot>();
        public ObservableCollection<ModelRapot> CollectionRapot
        {
            get { return collectionRapot; }
            set
            {
                if (this.collectionRapot != value)
                {
                    collectionRapot = value;
                    RaisePropertyChanged("");
                }
            }
        }

        public ViewModelRapot()
        {
            this.LoadUrl();
        }

        private void LoadUrl()
        {
            WebClient clientlistrapot = new WebClient();
            clientlistrapot.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadRapot);
            clientlistrapot.DownloadStringAsync(new Uri(URL.BASE3 + "api/rapotanakasuh/rapotanakasuh.php?id_donatur=" + Navigation.navIdLogin));
            string id = Navigation.navIdLogin;
        }

        public static bool konek = true;

        private void DownloadRapot(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JObject jresult = JObject.Parse(e.Result);
                JArray JItem = JArray.Parse(jresult.SelectToken("item").ToString());
                foreach (JObject item in JItem)
                {
                    ModelRapot modelRapot = new ModelRapot();
                    modelRapot.file_laporan = URL.BASE3 + "modul/mod_Laporan/laporan/" + item.SelectToken("file_laporan").ToString();
                    if (item.SelectToken("semester_pr").ToString() == "1")
                    {
                        modelRapot.semester_pr = item.SelectToken("semester_pr").ToString() + "st Semester";
                    }
                    else if (item.SelectToken("semester_pr").ToString() == "2")
                    {
                        modelRapot.semester_pr = item.SelectToken("semester_pr").ToString() + "nd Semester";
                    }
                    else if (item.SelectToken("semester_pr").ToString() == "3")
                    {
                        modelRapot.semester_pr = item.SelectToken("semester_pr").ToString() + "rd Semester";
                    }
                    else
                    {
                        modelRapot.semester_pr = item.SelectToken("semester_pr").ToString() + "th Semester";
                    }
                    collectionRapot.Add(modelRapot);
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
    }
}
