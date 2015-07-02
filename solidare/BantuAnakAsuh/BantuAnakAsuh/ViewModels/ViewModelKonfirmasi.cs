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
using System.Windows.Controls;


namespace BantuAnakAsuh.ViewModels
{
    class ViewModelKonfirmasi :ViewModelBase
    {
        public ViewModelKonfirmasi()
        {
          
            this.LoadUrlKonfirmasi();
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
                request.AddParameter("from_bank", From_bank);
                request.AddParameter("to_bank", Navigation.navBank);
                request.AddParameter("account_number", Account_number);
                request.AddParameter("account_name", Account_name);

                request.AddFile("photo", ReadToEnd(bitmapUserProfile), "photo" + rand.Next(0, 99999999).ToString() + ".jpg");

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
            Photo = (image);
            
        }


        public ICommand PublishCommand
        {
            get
            {
                return new DelegateCommand(PushToServer);
            }
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
                foreach (var id in collectionListBank)
                {
                    if (index == listStatusSelected)
                    {
                        Navigation.navBank = id;   
                    }
                    index++;
                }
            }
        }

         private ObservableCollection<String> collectionListBank = new ObservableCollection<String>();
        public ObservableCollection<String> CollectionListBank
        {
            get { return collectionListBank; }
            set
            {
                if (this.collectionListBank != value)
                {
                    collectionListBank = value;
                    RaisePropertyChanged("");
                }
            }
        }

        public bool konek = true;

        public void LoadUrlKonfirmasi()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/charityorganization/charity_account.php", Method.POST);

                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", "871");
                request.AddParameter("token", ")GYaS6^cO!NL$eQDuzFZB952f");
                request.AddParameter("id_cha_org", Navigation.navId_cha_org);


                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {
                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());
             
                    foreach (JObject item in JItem)
                    {


                        To_bank = item.SelectToken("bank").ToString();
                        CollectionListBank.Add(To_bank);
                    }
                     if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (result.Equals("success"))
                        {
                           

                        }

                    }
                });
            
            }
            catch
            {
                konek = false;
            }
        }



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
        

      
        private int jumlah_pembayaran;

        public int Jumlah_Pembayaran
        {
            get { return jumlah_pembayaran; }
            set { jumlah_pembayaran = value; RaisePropertyChanged(""); }
        }
        private String from_bank;

        public String From_bank
        {
            get { return from_bank; }
            set { from_bank = value; RaisePropertyChanged(""); }
        }
        private String account_number;

        public String Account_number
        {
            get { return account_number; }
            set { account_number = value; RaisePropertyChanged(""); }
        }
        private String to_bank;

        public String To_bank
        {
            get { return to_bank; }
            set { to_bank = value; RaisePropertyChanged(""); }
        }
        private String account_name;

        public String Account_name
        {
            get { return account_name; }
            set { account_name = value; RaisePropertyChanged(""); }
        }
        private BitmapImage photo;
        public BitmapImage Photo
        {
            get { return photo; }
            set
            {
                photo = value;
                RaisePropertyChanged("");
            }
        }


    }
}
