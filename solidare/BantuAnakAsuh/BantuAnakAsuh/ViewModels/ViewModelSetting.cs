using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
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
            FotoDonatur = (image);
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

        private bool status;
        private void PushToServer(object obj)
        {
            try
            {
                status = true;
                RestRequest request = new RestRequest(URL.BASE3 + "api/setting/setting.php?id_donatur=" + Navigation.navIdLogin, Method.POST);

                if (Username == null || Email == null || No_tlp == null || Alamat_donatur == null)
                {
                    MessageBox.Show("All Textbox Must Be Filled");
                    status = false;
                }
                else
                {
                    request.AddHeader("content-type", "multipart/form-data");
                    request.AddParameter("username", Username);
                    //if (NewPassword == ConPassword)
                    //{
                    //    request.AddParameter("password", NewPassword);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Confirm new password that you entered is not same with your new password, please try again!");
                    //    status = false;
                    //}
                    request.AddParameter("alamat_donatur", Alamat_donatur);
                    request.AddParameter("email_donatur", Email);
                    request.AddParameter("no_tlp", No_tlp);
                    
                    if (bitmapFotoDonatur != null)
                    {
                        request.AddFile("foto_donatur", ReadToEnd(bitmapFotoDonatur), "photo" + rand.Next(0, 99999999).ToString() + ".jpg");
                    }

                    //calling server with restClient
                    RestClient restClient = new RestClient();
                    if (status == true)
                    {
                        restClient.ExecuteAsync(request, (response) =>
                        {
                            ShellToast toast = new ShellToast();
                            toast.Title = "Status Upload";

                            //Try Catch because cannot Ggt json eesponse if upload photo
                            try
                            {
                                JObject jRoot = JObject.Parse(response.Content);
                                String result = jRoot.SelectToken("result").ToString();
                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    if (result.Equals("sukses"))
                                    {
                                        MessageBox.Show("Your profile has been updated");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Something Wrong");
                                    }
                                }
                                else
                                {
                                    //error ocured during upload
                                    MessageBox.Show("Failed to post, Please check your Internet connection.");
                                }
                            }
                            catch
                            {
                                if (bitmapFotoDonatur != null)
                                {
                                    MessageBox.Show("Profile Updated");
                                }
                                else
                                {
                                    MessageBox.Show("Something Wrong");
                                }
                            }
                        });
                    }
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable." + ec.Message);
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

        public String Id_donatur
        {
            get { return id_donatur; }
            set { id_donatur = value; RaisePropertyChanged(""); }
        }

        private String username;

        public String Username
        {
            get { return username; }
            set { username = value; RaisePropertyChanged(""); }
        }

        private String password;

        public String Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(""); }
        }

        //private String newpassword;

        //public String NewPassword
        //{
        //    get { return newpassword; }
        //    set { newpassword = value; RaisePropertyChanged(""); }
        //}

        //private String conpassword;

        //public String ConPassword
        //{
        //    get { return conpassword; }
        //    set { conpassword = value; RaisePropertyChanged(""); }
        //}

        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(""); }
        }

        private String no_tlp;

        public String No_tlp
        {
            get { return no_tlp; }
            set { no_tlp = value; RaisePropertyChanged(""); }
        }

        private String alamat_donatur;

        public String Alamat_donatur
        {
            get { return alamat_donatur; }
            set { alamat_donatur = value; RaisePropertyChanged(""); }
        }

        private String foto_donatur;

        public String Foto_donatur
        {
            get { return foto_donatur; }
            set { foto_donatur = value; RaisePropertyChanged(""); }
        }

        private BitmapImage fotoDonatur;
        public BitmapImage FotoDonatur
        {
            get { return fotoDonatur; }
            set
            {
                fotoDonatur = value;
                RaisePropertyChanged("");
            }
        }


        public ViewModelSetting()
        {
            this.LoadUrl();
        }

        private void LoadUrl()
        {
            WebClient clientdetailanak = new WebClient();
            clientdetailanak.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadDetailAnak);
            clientdetailanak.DownloadStringAsync(new Uri(URL.BASE3 + "api/donatur/donatur.php?id_donatur=" + Navigation.navIdLogin+"?nocache="+Guid.NewGuid()));
        }

        public bool konek = true;
        private void DownloadDetailAnak(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JObject jresult = JObject.Parse(e.Result);
                FotoDonatur = new BitmapImage(new Uri(URL.BASE3 + "modul/mod_OrangTuaAsuh/photo/" + jresult.SelectToken("foto_donatur").ToString()));
                Id_donatur = jresult.SelectToken("id_donatur").ToString();
                Username = jresult.SelectToken("username").ToString();
                Password = jresult.SelectToken("password").ToString();
                Email = jresult.SelectToken("email_donatur").ToString();
                No_tlp = jresult.SelectToken("no_tlp").ToString();
                Alamat_donatur = jresult.SelectToken("alamat_donatur").ToString();
            }
            catch
            {
                konek = false;
            }
        }



    }
}
