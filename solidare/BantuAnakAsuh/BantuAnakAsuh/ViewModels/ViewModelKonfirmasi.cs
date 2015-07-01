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
using BantuAnakAsuh.Models;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Controls;


namespace BantuAnakAsuh.ViewModels
{
    class ViewModelKonfirmasi :ViewModelBase
    {
        ModelBank mBank = new ModelBank();
        public ViewModelKonfirmasi()
        {
            Id_Order = Navigation.navIdDonors;
            Dari_Bank = mBank.Bank;
            //this.LoadUrlKonfirmasi();
        }

        private void PushToServer(object obj)
        {
            
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/donation/confirmation.php", Method.POST);

                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);
                request.AddParameter("id_donation", Navigation.idDonation);
                request.AddParameter("photo", mBank.Photo);
                request.AddParameter("from_bank", mBank.Bank);
                request.AddParameter("to_bank", mBank.To_bank);
                request.AddParameter("accont_number", mBank.Account_number);
                request.AddParameter("account_name", mBank.Account_name);

                //request.AddParameter("id_cha_org", Navigation.navId_cha_org);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {
                    ShellToast toast = new ShellToast();
                    toast.Title = "Status Upload";
                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    String jmessage = jRoot.SelectToken("message").ToString();
                    JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (result.Equals("success"))
                        {
                            toast.Show();
                            MessageBox.Show(jmessage);
                            var frame = App.Current.RootVisual as PhoneApplicationFrame;
                            frame.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
                        }
                        else
                        {

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
        
        
        private Stream bitmapUserProfile;
        private Random rand = new Random();
        public ICommand GetImageCommand1
        {
            get
            {
                return new DelegateCommand(GetCaptureImage1);
            }
        }
        private void GetCaptureImage1(object obj)
        {
            CameraCaptureTask camera = new CameraCaptureTask();
            camera.Show();

            camera.Completed += new EventHandler<PhotoResult>(camera_Completed);
        }
        public ICommand GetImageCommand
        {
            get
            {
                return new DelegateCommand(GetCaptureImage);
            }
        }

        private void GetCaptureImage(object obj)
        {
            PhotoChooserTask _photoChooserTask = new PhotoChooserTask();
            _photoChooserTask.PixelWidth = Convert.ToInt32(300);//Here best use screen width 
            _photoChooserTask.PixelHeight = Convert.ToInt32(300);//Here best use screen Heigh 

            _photoChooserTask.Completed += new EventHandler<PhotoResult>(camera_Completed);
            _photoChooserTask.Show(); 
        }

        void camera_Completed(object sender, PhotoResult e)
        {
            BitmapImage image = new BitmapImage();
            bitmapUserProfile = e.ChosenPhoto;
            image.SetSource(e.ChosenPhoto);

            FotoKejahatan = (image);
            
        }


        public ICommand PublishCommand
        {
            get
            {
                return new DelegateCommand(PushToServer);
            }
        }

        private void LoadUrlKonfirmasi()
        {

            WebClient clientkonfirmasi = new WebClient();
            clientkonfirmasi.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadListOrder);
            clientkonfirmasi.DownloadStringAsync(new Uri(URL.BASE3 + "api/konfirmasi/getorder.php?id_donatur=" + Navigation.navIdLogin));

        }

        private int listStatusSelected = -1;
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
                foreach (var id in collectionListOrder)
                {
                    if (index == listStatusSelected)
                    {
                        Id_Order = id;   
                    }
                    index++;
                }
            }
        }

        private ObservableCollection<string> collectionListOrder = new ObservableCollection<string>();
        public ObservableCollection<string> CollectionListOrder
        {
            get { return collectionListOrder; }
            set
            {
                if (this.collectionListOrder != value)
                {
                    collectionListOrder = value;
                    RaisePropertyChanged("");
                }
            }
        }

        public bool konek = true;

        private void DownloadListOrder(object sender, DownloadStringCompletedEventArgs e)
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
                        
                        string idorder = item.SelectToken("id_order").ToString();
                        //string biaya = item.SelectToken("biaya_donasi").ToString();
                        //string bank = item.SelectToken("bank").ToString();
                        //string nomor_rekening = item.SelectToken("no_rek").ToString();

                        collectionListOrder.Add(idorder);
                    }
                    
                }
                else
                {
                    String hasil = jresult.SelectToken("message").ToString();
                    
                }
            }
            catch
            {
                konek = false;
            }
        }

        //private void PushToServer(object obj)
        //{
        //    try
        //    {

        //        RestRequest request = new RestRequest(URL.BASE3 + "api/konfirmasi/konfirmasi.php", Method.POST);

        //        request.AddHeader("content-type", "multipart/form-data");
        //        request.AddParameter("id_order", Id_Order);
        //        request.AddParameter("jumlah_pembayaran", Jumlah_Pembayaran);
        //        request.AddParameter("dari_bank", Dari_Bank);
        //        request.AddParameter("nomor_rekening", Nomor_Rekening);
        //        request.AddParameter("bank_tujuan", Bank_Tujuan);
        //        request.AddParameter("pemilik_rekening", Pemilik_Rekening);

        //        request.AddFile("url_img_post", ReadToEnd(bitmapUserProfile), "photo" + rand.Next(0, 99999999).ToString() + ".jpg");

        //        //calling server with restClient
        //        RestClient restClient = new RestClient();
        //        restClient.ExecuteAsync(request, (response) =>
        //        {
        //            ShellToast toast = new ShellToast();
        //            toast.Title = "Status Upload";
        //            JObject jRoot = JObject.Parse(response.Content);
        //            String result = jRoot.SelectToken("result").ToString();
                       
        //           if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //                {
        //                    if (result.Equals("sukses"))
        //                    {
        //                        MessageBox.Show("Confirmation Success");
        //                    }
        //                    else
        //                    {
                                
        //                        MessageBox.Show("Failed");
        //                    }
        //            }
        //            else
        //            {
        //                //error ocured during upload

        //                toast.Content = "Your posting failed. Please check the Internet connection.";
        //                toast.Show();
        //                //progressBar1.Visibility = System.Windows.Visibility.Visible;

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




        public byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = stream.Position;
            stream.Position = 0;

            try
            {
                byte[] readBuffer = new byte[4096];

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

























        // inilisialisasi
        

        private String id_order;

        public String Id_Order
        {
            get { return id_order; }
            set { id_order = value; RaisePropertyChanged(""); }
        }
        private int jumlah_pembayaran;

        public int Jumlah_Pembayaran
        {
            get { return jumlah_pembayaran; }
            set { jumlah_pembayaran = value; RaisePropertyChanged(""); }
        }
        private String dari_bank;

        public String Dari_Bank
        {
            get { return dari_bank; }
            set { dari_bank = value; RaisePropertyChanged(""); }
        }
        private String nomor_rekening;

        public String Nomor_Rekening
        {
            get { return nomor_rekening; }
            set { nomor_rekening = value; RaisePropertyChanged(""); }
        }
        private String bank_tujuan;

        public String Bank_Tujuan
        {
            get { return bank_tujuan; }
            set { bank_tujuan = value; RaisePropertyChanged(""); }
        }
        private String pemilik_rekening;

        public String Pemilik_Rekening
        {
            get { return pemilik_rekening; }
            set { pemilik_rekening = value; RaisePropertyChanged(""); }
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


    }
}
