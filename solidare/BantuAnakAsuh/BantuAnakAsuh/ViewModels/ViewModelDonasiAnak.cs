using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BantuAnakAsuh.Common;
using System.Windows.Input;
using RestSharp;
using BantuAnakAsuh.Helper;
using Microsoft.Phone.Shell;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Data.Linq.Mapping;
using BantuAnakAsuh.Views;
using System.Windows.Navigation;
using BantuAnakAsuh.Models;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelDonasiAnak : ViewModelBase
    {
        private ObservableCollection<ModelDonasi> collectionAnakAsuh = new ObservableCollection<ModelDonasi>();
        public ObservableCollection<ModelDonasi> CollectionAnakAsuh
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
        
        private void LoadUrlAnak()
        {
            WebClient clientlistdonasi = new WebClient();
            clientlistdonasi.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadDonasiList);
            clientlistdonasi.DownloadStringAsync(new Uri(URL.BASE + "api/anakasuh/anakasuh.php"));
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
                    modelDonasi.foto_anak = URL.BASE + "modul/mod_AnakAsuh/photo/" + item.SelectToken("foto_anak").ToString();
                    modelDonasi.jenjang_pendidikan = "Jenjang " + item.SelectToken("jenjang_pendidikan").ToString();
                    modelDonasi.status_anak = "Status " + item.SelectToken("status_anak").ToString();
                    collectionAnakAsuh.Add(modelDonasi);
                    //ModelKeranjang modelKeranjang = new ModelKeranjang();
                    //modelKeranjang.id = item.SelectToken("id_anak_asuh").ToString();
                    //modelKeranjang.nama_anak = item.SelectToken("nama_anak_asuh").ToString();
                    
                    //collectionAnakAsuh.Add(modelKeranjang);
                }
            }
            catch
            {
                konek = false;
            }
        }

        public ViewModelDonasiAnak()
        {
            LoadUrlAnak();
        }

        public ICommand PublishCommand
        {
            get
            {
                return new DelegateCommand(PushToServer);
            }
        }

        private void PushToServer(object obj)
        {

            try
            {
                List<ModelKeranjang> mKeranjang = new List<ModelKeranjang>();
                if (mKeranjang.Count != 0 || mKeranjang.Count != null)
                {
                    String idAnak = "";
                    RestRequest request = new RestRequest(URL.BASE + "api/keranjangdonasi/keranjangdonasi.php", Method.POST);
                    request.AddHeader("content-type", "multipart/form-data");
                    int count = 1;
                    foreach (var item in mKeranjang)
                    {
                        
                        if (count > 1)
                        {
                            idAnak = idAnak + "," + item.id; 
                        }
                        else
                        {
                            idAnak = item.id;
                        }
                        count++;
                    }
                    request.AddParameter("id_anakasuh", Navigation.navIdAnak);
                    request.AddParameter("idDonatur", Navigation.navIdLogin);


                    //calling server with restClient
                    RestClient restClient = new RestClient();
                    restClient.ExecuteAsync(request, (response) =>
                    {
                        ShellToast toast = new ShellToast();
                        toast.Title = "Status Upload";
                        JObject jRoot = JObject.Parse(response.Content);
                        String result = jRoot.SelectToken("result").ToString();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (result.Equals("sukses"))
                            {
                                MessageBox.Show("Donasi anak berhasil!");
                            }
                            else
                            {

                                MessageBox.Show("gagal");
                            }
                        }
                        else
                        {
                            //error ocured during upload
                            MessageBox.Show("Donasi anak Gagal. Silahkan Periksa Koneksi Internet Anda.");
                        }
                    });
                }
                else
                {
                    MessageBox.Show("Keranjang donasi masih kosong!");
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show("Gagal menampilkan, Koneksi internet tidak stabil");
            }
            finally
            {
                //ProgressVisibiliy = Visibility.Visible;
            }
        }
    }
}
