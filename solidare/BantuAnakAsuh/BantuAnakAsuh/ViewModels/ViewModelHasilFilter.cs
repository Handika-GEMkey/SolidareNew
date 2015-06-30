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
    class ViewModelHasilFilter : ViewModelBase
    {
        private ObservableCollection<ModelDonasi> collectionFillter = new ObservableCollection<ModelDonasi>();
        public ObservableCollection<ModelDonasi> CollectionFillter
        {
            get { return collectionFillter; }
            set
            {
                if (this.collectionFillter != value)
                {
                    collectionFillter = value;
                    RaisePropertyChanged("");
                }
            }
        }

        public ViewModelHasilFilter()
        {
            this.LoadUrl();
        }

        private void LoadUrl()
        {
            int parameter = 0;
            var nav = new List<string>();
            string setfill="";
            if (!Navigation.Fill_Status.Equals("Pilih Status Anak"))
            {
                parameter = parameter + 1;
                nav.Add("status_anak=" + Navigation.Fill_Status);
            }

            if (!Navigation.Fill_Jk.Equals("Pilih Jenis Kelamin"))
            {
                parameter = parameter + 1;
                nav.Add("jk_anak_asuh=" + Navigation.Fill_Jk);
            }

            if (!Navigation.Fill_Jenjang.Equals("Pilih Jenjang Pendidikan"))
            {
                parameter = parameter + 1;
                nav.Add("jenjang_pendidikan=" + Navigation.Fill_Jenjang);
            }

            int i = 0;
            foreach (var item in nav)
            {
                i++;
                if (i == 1)
                    setfill = "?" + item;
                else
                    setfill = setfill + "&" + item;
            }

            WebClient clientlistdonasi = new WebClient();
            clientlistdonasi.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadDonasiList);
            clientlistdonasi.DownloadStringAsync(new Uri(URL.BASE3 + "api/filtering/filtering.php"+setfill));
        }

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
                    collectionFillter.Add(modelDonasi);
                }
            }
            catch
            {
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

        public ICommand SetFilter
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
