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
    class ViewModelSearch : ViewModelBase
    {
        private ObservableCollection<ModelDonasi> collectionSearch = new ObservableCollection<ModelDonasi>();
        public ObservableCollection<ModelDonasi> CollectionSearch
        {
            get { return collectionSearch; }
            set
            {
                if (this.collectionSearch != value)
                {
                    collectionSearch = value;
                    RaisePropertyChanged("");
                }
            }
        }

        public ViewModelSearch(string cari)
        {
            this.LoadUrl(cari);
        }

        private void LoadUrl(string nama_anak)
        {
            WebClient clientlistdonasi = new WebClient();
            clientlistdonasi.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadDonasiList);
            clientlistdonasi.DownloadStringAsync(new Uri(URL.BASE3 + "api/search/searchanak.php?nama_anak_asuh="+nama_anak));
        }

        public static bool konek = true;

        private void DownloadDonasiList(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JObject jresult = JObject.Parse(e.Result);
                JArray JItem = JArray.Parse(jresult.SelectToken("item").ToString());
                foreach (JObject item in JItem)
                {
                    ModelDonasi modelDonasi = new ModelDonasi();
                    modelDonasi.id_anak_asuh = item.SelectToken("id_anak_asuh").ToString();
                    modelDonasi.nama_anak_asuh = item.SelectToken("nama_anak_asuh").ToString();
                    modelDonasi.foto_anak = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item.SelectToken("foto_anak").ToString();
                    modelDonasi.jenjang_pendidikan = "Jenjang "+ item.SelectToken("jenjang_pendidikan").ToString();
                    modelDonasi.status_anak = "Status " + item.SelectToken("status_anak").ToString();
                    collectionSearch.Add(modelDonasi);
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

        public ICommand SetSearch
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
                Navigation.navIdAnak = SelectedItem.id_anak_asuh;

            listIndex = -1;
        }
    }
}
