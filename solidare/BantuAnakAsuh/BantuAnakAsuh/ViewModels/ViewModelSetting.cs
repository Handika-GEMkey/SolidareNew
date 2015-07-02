using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelSetting : ViewModelBase
    {
        private String id_donatur;
        private Stream bitmapFotoDonatur;
        private Random rand = new Random();

        private void GetImageFromGallery(object obj)
        {
            PhotoChooserTask photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Show();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
            
        }

        private void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            BitmapImage image = new BitmapImage();
            image.SetSource(e.ChosenPhoto);
            bitmapFotoDonatur = e.ChosenPhoto;
            Photo = (image);
        }

        public ICommand GetImage
        {
            get
            {
                return new DelegateCommand(GetImageFromGallery);
            }
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
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/donors/setting_profile.php", Method.POST);

                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);
                request.AddParameter("name", Name);
                request.AddParameter("address", Address);
                request.AddParameter("email", Email);
                request.AddParameter("phone", Phone);
                request.AddParameter("gender", Gender);
                if (Photo != null)
                {
                    request.AddParameter("photo", Photo);
                }
                else { 
                    request.AddFile("photo", ReadToEnd(bitmapFotoDonatur), "photo" + rand.Next(0, 99999999).ToString() + ".jpg"); 
                }
                

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {
                    JObject jRoot = JObject.Parse(response.Content);
                    String jresult = jRoot.SelectToken("result").ToString();
                    String jmessage = jRoot.SelectToken("message").ToString();

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
                                //MessageBox.Show("Success");
                                var frame = App.Current.RootVisual as PhoneApplicationFrame;
                                frame.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
                            }
                        }
                        else
                        {
                            if (MessageBoxResult.OK == MessageBox.Show("Foster Children's profile upload failed"))
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
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }
            finally
            {
                //ProgressVisibiliy = Visibility.Visible;
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

        public ViewModelSetting()
        {
            //this.LoadUrl();
            this.LoadProfile();
        }

        private void LoadProfile()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/donors/profile.php", Method.POST);
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {
                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                    foreach (JObject item in JItem)
                    {
                        
                        Name = item.SelectToken("name").ToString();
                        Address = item["address"].ToString();
                        Email = item["email"].ToString();
                        Phone = item["phone"].ToString();
                        Photo = new BitmapImage(new Uri(URL.BASE3 + "modul/mod_OrangTuaAsuh/photo/" + item["photo"].ToString()));
                        Gender = item["gender"].ToString();

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

        //private void LoadUrl()
        //{
        //    WebClient clientdetailanak = new WebClient();
        //    clientdetailanak.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadDetailAnak);
        //    clientdetailanak.DownloadStringAsync(new Uri(URL.BASE3 + "api/donatur/donatur.php?id_donatur=" + Navigation.navIdLogin+"?nocache="+Guid.NewGuid()));
        //}

        //public bool konek = true;
        //private void DownloadDetailAnak(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    try
        //    {
        //        JObject jresult = JObject.Parse(e.Result);
        //        FotoDonatur = new BitmapImage(new Uri(URL.BASE3 + "modul/mod_OrangTuaAsuh/photo/" + jresult.SelectToken("foto_donatur").ToString()));
        //        Id_donatur = jresult.SelectToken("id_donatur").ToString();
        //        Username = jresult.SelectToken("username").ToString();
        //        Password = jresult.SelectToken("password").ToString();
        //        Email = jresult.SelectToken("email_donatur").ToString();
        //        No_tlp = jresult.SelectToken("no_tlp").ToString();
        //        Alamat_donatur = jresult.SelectToken("alamat_donatur").ToString();
        //    }
        //    catch
        //    {
        //        konek = false;
        //    }
        //}

        private String id_donors;

        public String Id_donors
        {
            get { return id_donors; }
            set { id_donors = value; RaisePropertyChanged(""); }
        }
        private String token;

        public String Token
        {
            get { return token; }
            set { token = value; RaisePropertyChanged(""); }
        }
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(""); }
        }
        private String address;

        public String Address
        {
            get { return address; }
            set { address = value; RaisePropertyChanged(""); }
        }
        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(""); }
        }
        private String phone;

        public String Phone
        {
            get { return phone; }
            set { phone = value; RaisePropertyChanged(""); }
        }
        private String gender;

        public String Gender
        {
            get { return gender; }
            set { gender = value; RaisePropertyChanged(""); }
        }
        private BitmapImage photo;

        public BitmapImage Photo
        {
            get { return photo; }
            set { photo = value; RaisePropertyChanged(""); }
        }

        private String Foto;

        public String Foto1
        {
            get { return Foto; }
            set { Foto = value; RaisePropertyChanged(""); }
        }

    }
}
