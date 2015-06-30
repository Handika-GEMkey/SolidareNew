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
    class ViewModelDonasi: ViewModelBase
    {
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

        public ViewModelDonasi()
        {
            this.LoadUrl();
            this.LoadUrlDonatur();
        }

        private void LoadUrl()
        {
           try
            {

                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/fosterchildren/fosterchildren.php", Method.POST);

                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", "871");
                request.AddParameter("token", ")GYaS6^cO!NL$eQDuzFZB952f");


                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {
                    ShellToast toast = new ShellToast();
                    toast.Title = "Status Upload";
                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                    foreach (JObject item in JItem)
                    {
                        ModelDonasi modelDonasi = new ModelDonasi();
                        modelDonasi.id_anak_asuh = item.SelectToken("id_fosterchildren").ToString();
                        modelDonasi.nama_anak_asuh = item.SelectToken("name").ToString();
                        modelDonasi.foto_anak = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item.SelectToken("photo").ToString();
                        modelDonasi.jenjang_pendidikan = "Jenjang " + item.SelectToken("study_level").ToString();
                        modelDonasi.status_anak = "Status" + item.SelectToken("children_status").ToString();
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
                Navigation.navIdAnak = SelectedItem.id_anak_asuh;

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

        private void LoadUrlDonatur()
        {
            WebClient clientprofiledonatur = new WebClient();
            clientprofiledonatur.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadProfileDonatur);
            clientprofiledonatur.DownloadStringAsync(new Uri(URL.BASE3 + "api/donatur/donatur.php?id_donatur=" + Navigation.navIdLogin + "?nocache=" + Guid.NewGuid()));

        }

        public bool konek1 = true;

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
                konek1 = false;
            }
        }
    }
}
