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
using System.IO.IsolatedStorage;
using BantuAnakAsuh.Models;

using System.Windows.Controls;
using System.Globalization;
using Microsoft.Phone.Controls;


namespace BantuAnakAsuh.ViewModels
{
    class ViewModelRekomendasi : ViewModelBase
    {
        //PageRekomendasi pr = new PageRekomendasi();
        ModelRekomendasi mr = new ModelRekomendasi();

        private Stream bitmapUserProfile;
        private Random rand = new Random();
        private int jenisKelaminSelected = -1;
        private int listStatusSelected = -1;
        //- image upload
        private Stream bitmapFotoDepanRumah;
        private Stream bitmapFotoDalamRumah;

        public ICommand GetImageCommand { get { return new DelegateCommand(GetCaptureImage); } }

        public ICommand GetImageCommandA { get { return new DelegateCommand(GetCaptureImageA); } }

        public ICommand GetImageCommandB { get { return new DelegateCommand(GetCaptureImageB); } }

        public ICommand GetImageCommand1 { get { return new DelegateCommand(GetCaptureImage1); } }

        public ICommand GetImageCommandA1 { get { return new DelegateCommand(GetCaptureImageA1); } }

        public ICommand GetImageCommandB1 { get { return new DelegateCommand(GetCaptureImageB1); } }

        private void GetCaptureImageB(object obj)
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.OKCancel) == MessageBoxResult.Yes)
            {
                // If yes
                CameraCaptureTask camera = new CameraCaptureTask();
                camera.Show();

                camera.Completed += new EventHandler<PhotoResult>(camera_b);
            }
            else
            {
                // If no
            }
        }

        private void GetCaptureImageA(object obj)
        {
            CameraCaptureTask camera = new CameraCaptureTask();
            camera.Show();

            camera.Completed += new EventHandler<PhotoResult>(camera_a);

        }

        private void GetCaptureImageB1(object obj)
        {
            PhotoChooserTask _photoChooserTask = new PhotoChooserTask();
            _photoChooserTask.PixelWidth = Convert.ToInt32(300);//Here best use screen width 
            _photoChooserTask.PixelHeight = Convert.ToInt32(300);//Here best use screen Heigh 

            _photoChooserTask.Completed += new EventHandler<PhotoResult>(camera_b);
            _photoChooserTask.Show();

        }

        private void GetCaptureImageA1(object obj)
        {
            PhotoChooserTask _photoChooserTask = new PhotoChooserTask();
            _photoChooserTask.PixelWidth = Convert.ToInt32(300);//Here best use screen width 
            _photoChooserTask.PixelHeight = Convert.ToInt32(300);//Here best use screen Heigh 

            _photoChooserTask.Completed += new EventHandler<PhotoResult>(camera_a);
            _photoChooserTask.Show();
        }

        string id;

        public void LoadID()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    using (IsolatedStorageFileStream rawStream = isf.OpenFile("id_donors", System.IO.FileMode.Open))
                    {
                        StreamReader reader = new StreamReader(rawStream);

                        id = reader.ReadToEnd();
                        Navigation.navIdLogin = id;
                        reader.Close();
                    }
                }
                catch
                {
                    //data tidak ditemukan
                    MessageBox.Show("An error occured while retrieving data.");
                }
            }
        }

        //--- constructor
        public ViewModelRekomendasi()
        {
            jenisKelaminCollection.Add("Choose gender");
            jenisKelaminCollection.Add("Male");
            jenisKelaminCollection.Add("Female");

            listStatusCollection.Add("Choose status");
            listStatusCollection.Add("Orphan");
            listStatusCollection.Add("Orphans");
            listStatusCollection.Add("Poor children");
        }
        
        void camera_a(object sender, PhotoResult e)
        {
            BitmapImage imageDpnRumah = new BitmapImage();

            bitmapFotoDepanRumah = e.ChosenPhoto;
            imageDpnRumah.SetSource(e.ChosenPhoto);
            FotoDepanRumah = (imageDpnRumah);
        }

        void camera_b(object sender, PhotoResult e)
        {
            BitmapImage imageDlmRumah = new BitmapImage();

            bitmapFotoDalamRumah = e.ChosenPhoto;
            imageDlmRumah.SetSource(e.ChosenPhoto);
            FotoDalamRumah = (imageDlmRumah);
        }

        private void GetCaptureImage1(object obj)
        {
            PhotoChooserTask _photoChooserTask = new PhotoChooserTask();
            _photoChooserTask.PixelWidth = Convert.ToInt32(300);//Here best use screen width 
            _photoChooserTask.PixelHeight = Convert.ToInt32(300);//Here best use screen Heigh 

            _photoChooserTask.Completed += new EventHandler<PhotoResult>(camera_Completed);
            _photoChooserTask.Show();
        }

        private void GetCaptureImage(object obj)
        {
            CameraCaptureTask camera = new CameraCaptureTask();
            camera.Show();

            camera.Completed += new EventHandler<PhotoResult>(camera_Completed);

        }


        void camera_Completed(object sender, PhotoResult e)
        {
            BitmapImage image = new BitmapImage();

            bitmapUserProfile = e.ChosenPhoto;
            image.SetSource(e.ChosenPhoto);
            FotoKejahatan = (image);

        }

        public ICommand PublishCommand1{ get { return new DelegateCommand(PushToServer1); } }

        public ICommand PublishCommand2 { get { return new DelegateCommand(PushToServer2); } }
        
        //public ICommand PublishCommand3 { get { return new DelegateCommand(PushToServer3); } }

        //private void PushToServer3(object obj)
        //{
        //    try
        //    {

        //        this.LoadID();


        //        RestRequest request = new RestRequest(URL.BASE3 + "api/environment/environment2.php?id_donatur=" + id, Method.POST);
        //        request.AddHeader("content-type", "multipart/form-data");


        //        //-- validasi deskripsi
        //        if (Deskrip2 == null)
        //        {
        //            Deskrip2 = "  ";


        //        }
        //        request.AddFile("url_img_post2", ReadToEnd(bitmapFotoDalamRumah), "photo" + rand.Next(0, 99999999).ToString() + ".jpg");
        //        request.AddParameter("deskrip2", Deskrip2);


        //        //calling server with restClient
        //        RestClient restClient = new RestClient();
        //        restClient.ExecuteAsync(request, (response) =>
        //        {

        //            JObject jRoot = JObject.Parse(response.Content);
        //            String jresult = jRoot.SelectToken("result").ToString();
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                if (jresult.Equals("sukses"))
        //                {
        //                    MessageBox.Show("Environment photo successfully uploaded.");

        //                }
        //                else
        //                {

        //                    MessageBox.Show("Failed");
        //                }

        //            }
        //            else
        //            {
        //                //error ocured during upload
        //                MessageBox.Show("Your posting failed. Please check the Internet connection.");
        //            }
        //        });
        //    }
        //    catch (Exception ec)
        //    {
        //        MessageBox.Show("Failed to display, the Internet connection is unstable.");
        //    }
        //    finally
        //    {
        //        //ProgressVisibiliy = Visibility.Visible;
        //    }
        //}

        private void PushToServer2(object obj)
        {
            try
            {

                this.LoadID();

                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/recommendation/environment.php", Method.POST);

                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdLogin);
                request.AddParameter("token", Navigation.token);
                request.AddParameter("id_recommendation", Navigation.id_recommendation);
                request.AddFile("environment_in", ReadToEnd(bitmapFotoDepanRumah), "photo" + rand.Next(0, 99999999).ToString() + ".jpg");
                request.AddFile("environment_out", ReadToEnd(bitmapFotoDepanRumah), "photo" + rand.Next(0, 99999999).ToString() + ".jpg");
                
                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {

                    JObject jRoot = JObject.Parse(response.Content);
                    String jresult = jRoot.SelectToken("result").ToString();
                    if (jresult == "failed")
                    {
                        MessageBox.Show("Failed!");
                    }
                    else
                    {
                        String jmessage = jRoot.SelectToken("message").ToString();

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (jresult.Equals("success"))
                            {
                                MessageBox.Show(jmessage);
                                var frame = App.Current.RootVisual as PhoneApplicationFrame;
                                frame.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
                            }
                            else
                            {
                                MessageBox.Show(jmessage);
                            }

                        }
                        else
                        {
                            //error ocured during upload
                            MessageBox.Show("Your posting failed. Please check the Internet connection.");
                        }
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

        int cobaValidasi;
        
        private void PushToServer1(object obj)
        {
            //Navigation.menuItem = "pivot_environment";
            try
            {
                this.LoadID();
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/recommendation/recommendation.php", Method.POST);
                
                    request.AddHeader("content-type", "multipart/form-data");
                    request.AddParameter("id_donors", Navigation.navIdLogin);
                    request.AddParameter("token", Navigation.token);
                    request.AddParameter("parent_name", Nama_orangtua_asli);
                    request.AddParameter("address", Alamat);
                    request.AddParameter("jobs", Pekerjaan);
                    request.AddParameter("salary", Penghasilan);
                    request.AddParameter("children_name", Nama_anak_asuh);
                    request.AddParameter("gender", Jk_anak_asuh);
                    request.AddFile("photo", ReadToEnd(bitmapUserProfile), "photo" + rand.Next(0, 99999999).ToString() + ".jpg");
                    request.AddParameter("children_status", Status_anak);
                    request.AddParameter("latitude", Navigation.Latitude);
                    request.AddParameter("longitude", Navigation.Longitude);
                    Navigation.menuItem = "pivot_environment";
                    #region Convert DatePicker
                    //string format = "yyyy-MM-dd";
                    //DateTime datevalue;
                    //if(DateTime.TryParseExact(Tanggal_lahir,"M/dd/yyyy h:m:s",CultureInfo.InvariantCulture,DateTimeStyles.None,  out datevalue))
                    //{
                    //    Tanggal_lahir = datevalue.ToString(format);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("An error occured, please try again!");
                    //}
                    #endregion

                    //calling server with restClient
                    RestClient restClient = new RestClient();
                    restClient.ExecuteAsync(request, (response) =>
                    {
                        JObject jRoot = JObject.Parse(response.Content);
                        String jresult = jRoot.SelectToken("result").ToString();
                        String jmessage = jRoot.SelectToken("message").ToString();
                        Navigation.id_recommendation = jRoot.SelectToken("id_recommendation").ToString();

                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            MessageBox.Show("Failed");
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            MessageBox.Show("Failed");
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (jresult.Equals("success"))
                            {
                                if (MessageBoxResult.OK == MessageBox.Show("Your " + jmessage))
                                {

                                }
                            }
                            else
                            {
                                if (MessageBoxResult.OK == MessageBox.Show("Your "+jmessage))
                                {

                                    MessageBox.Show("Upload failed, try again!");

                                }
                            }

                        }
                        else
                        {
                            //error ocured during upload
                            MessageBox.Show("Your posting failed. Please check the Internet connection.");
                        }
                    });
            }
            catch (Exception ec)
            {
                Navigation.menuItem = "pivot_failed";
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }
            finally
            {
                //ProgressVisibiliy = Visibility.Visible;
            }
        }

        private ObservableCollection<string> jenisKelaminCollection = new ObservableCollection<string>();

        public ObservableCollection<string> JenisKelaminCollection
        {
            get { return jenisKelaminCollection; }
            set
            {
                jenisKelaminCollection = value;
                RaisePropertyChanged("");
            }
        }

        private ObservableCollection<string> listStatusCollection = new ObservableCollection<string>();

        public ObservableCollection<string> ListStatusCollection
        {
            get { return listStatusCollection; }
            set { listStatusCollection = value; RaisePropertyChanged(""); }
        }

        public int ListStatusSelected
        {
            get
            {
                return listStatusSelected;
            }
            set
            {
                listStatusSelected = value;
                int index = 0;
                foreach (string jenis in listStatusCollection)
                {
                    if (index == listStatusSelected)
                    {

                        Status_anak = jenis;
                        if (Status_anak == "Choose foster children status")
                        {
                            MessageBox.Show("Oops, choose foster children status");
                        }
                    }
                    index++;
                }
            }
        }

        public int JenisKelaminSelected
        {
            get { return jenisKelaminSelected; }
            set
            {
                jenisKelaminSelected = value;
                int index = 0;
                foreach (string jenis in jenisKelaminCollection)
                {
                    if (index == jenisKelaminSelected)
                    {

                        Jk_anak_asuh = jenis;
                        if (Jk_anak_asuh == "Choose gender")
                        {
                            MessageBox.Show("Oops, choose your gender");
                        }
                    }
                    index++;

                }
            }
        }

        public byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = stream.Position;
            stream.Position = 0;

            try
            {
                byte[] readBuffer = new byte[2048];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                stream.Position = originalPosition;
            }
        }

        private String tanggal;

        public String Tanggal
        {
            get { return tanggal; }
            set { tanggal = value; RaisePropertyChanged(""); }
        }

        private String id_orangtua_asli;

        public String Id_orangtua_asli
        {
            get { return id_orangtua_asli; }
            set { id_orangtua_asli = value; RaisePropertyChanged(""); }
        }
        private String nama_anak_asuh;

        public String Nama_anak_asuh
        {
            get { return nama_anak_asuh; }
            set { nama_anak_asuh = value; RaisePropertyChanged(""); }
        }
        private String status_anak;

        public String Status_anak
        {
            get { return status_anak; }
            set { status_anak = value; RaisePropertyChanged(""); }
        }
        private String tempat_lahir;

        public String Tempat_lahir
        {
            get { return tempat_lahir; }
            set { tempat_lahir = value; RaisePropertyChanged(""); }
        }


        private String tanggal_lahir;

        public String Tanggal_lahir
        {
            get { return tanggal_lahir; }
            set { tanggal_lahir = value; RaisePropertyChanged("");
            
            }

        }
        private String jk_anak_asuh;

        public String Jk_anak_asuh
        {
            get { return jk_anak_asuh; }
            set { jk_anak_asuh = value; RaisePropertyChanged(""); }
        }
        private String anak_ke;

        public String Anak_ke
        {
            get { return anak_ke; }
            set { anak_ke = value; RaisePropertyChanged(""); }
        }
        private String alamat;

        public String Alamat
        {
            get { return alamat; }
            set { alamat = value; RaisePropertyChanged(""); }
        }
        private String kelas;
        public String Kelas
        {
            get { return kelas; }
            set { kelas = value; RaisePropertyChanged(""); }
        }
        private String nama_sekolah;

        public String Nama_sekolah
        {
            get { return nama_sekolah; }
            set { nama_sekolah = value; RaisePropertyChanged(""); }
        }
        private String foto_anak;

        public String Foto_anak
        {
            get { return foto_anak; }
            set { foto_anak = value; RaisePropertyChanged(""); }
        }

        private String latitude;

        public String Latitude
        {
            get { return latitude; }
            set { latitude = value; RaisePropertyChanged(""); }
        }
        private String longitude;

        public String Longitude
        {
            get { return longitude; }
            set { longitude = value; RaisePropertyChanged(""); }
        }
        private String nama_orangtua_asli;

        public String Nama_orangtua_asli
        {
            get { return nama_orangtua_asli; }
            set { nama_orangtua_asli = value; RaisePropertyChanged(""); }
        }
        private String alamat_orangtua_asli;

        public String Alamat_orangtua_asli
        {
            get { return alamat_orangtua_asli; }
            set { alamat_orangtua_asli = value; RaisePropertyChanged(""); }
        }
        private String pekerjaan;

        public String Pekerjaan
        {
            get { return pekerjaan; }
            set { pekerjaan = value; RaisePropertyChanged(""); }
        }
        private String penghasilan;

        public String Penghasilan
        {
            get { return penghasilan; }
            set { penghasilan = value; RaisePropertyChanged(""); }
        }


        private BitmapImage fotoKejahatan;
        public BitmapImage FotoKejahatan
        {
            get { return fotoKejahatan; }
            set
            {
                fotoKejahatan = value;
                RaisePropertyChanged("");
            }
        }

        private BitmapImage fotoDepanRumah;

        public BitmapImage FotoDepanRumah
        {
            get { return fotoDepanRumah; }
            set
            {
                fotoDepanRumah = value;
                RaisePropertyChanged("");
            }
        }

        private BitmapImage fotoDalamRumah;

        public BitmapImage FotoDalamRumah
        {
            get { return fotoDalamRumah; }
            set { fotoDalamRumah = value; RaisePropertyChanged(""); }
        }

    }
}
