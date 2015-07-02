using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BantuAnakAsuh.ViewModels;
using System.Text;
using BantuAnakAsuh.Models;
using BantuAnakAsuh.Helper;
using Newtonsoft.Json.Linq;
using Microsoft.Phone.Tasks;
using System.IO;
using System.Net.Http;

namespace BantuAnakAsuh.Views
{
    public partial class PageSetting : PhoneApplicationPage
    {
        PhotoChooserTask photoChooserTask;
        public PageSetting()
        {
            InitializeComponent();
            ViewModelSetting vmSetting = new ViewModelSetting();
            this.DataContext = vmSetting;

            /*photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);


            statusUpload = false;*/


        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));

        }

        private void buttonChangePassword_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PagePassword.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageProfileDonatur.xaml", UriKind.Relative));
        }

        private void saveChanges_Click(object sender, EventArgs e)
        {
            var vm = (ViewModelSetting)DataContext;
            vm.PublishCommand.Execute(null);
        }

        /*private void apbarDoneSetting_Click(object sender, EventArgs e)
        {
            if ((!(textUsername.Text).Equals("")) && (!(textEmail.Text).Equals("")) && (!(textPhone.Text).Equals("")) && (!(textAlamat.Text).Equals("")))
            {
                ModelSetting modelsetting = new ModelSetting();
                modelsetting.id_donatur = Navigation.navIdLogin;
                modelsetting.username = textUsername.Text;
                modelsetting.email_donatur = textEmail.Text;
                modelsetting.no_tlp = textPhone.Text;
                modelsetting.alamat_donatur = textAlamat.Text;

                if(statusUpload)
                {
                    UploadPhoto();
                }
                

                if (!textNewPassword.Equals(""))
                {
                    if ((textNewPassword.Password).Equals(textConfirmPassword.Password))
                    {
                        modelsetting.password = textNewPassword.Password;
                        UpdateSetting(modelsetting);
                    }
                    else
                    {
                        MessageBox.Show("Comfirm New Password yang anda masukan tidak sama dengan password baru anda, silahkan ulangi lagi !");
                    }
                }
                else
                {
                    modelsetting.password = textPassword.Password;
                    UpdateSetting(modelsetting);
                }
            }
            else
            {
                MessageBox.Show("Anda belum melengkapi data setting yang akan diubah, silahkan lengkapi terlebih dulu !");
            }
            statusUpload = false;
        }

        private String Result;

        void UpdateSetting(ModelSetting modelsettingku)
        {
            StringBuilder parameter = new StringBuilder();
            parameter.AppendFormat("{0}={1}&", "username", HttpUtility.UrlEncode(modelsettingku.username));
            parameter.AppendFormat("{0}={1}&", "password", HttpUtility.UrlEncode(modelsettingku.password));
            parameter.AppendFormat("{0}={1}&", "email_donatur", HttpUtility.UrlEncode(modelsettingku.email_donatur));
            parameter.AppendFormat("{0}={1}&", "no_tlp", HttpUtility.UrlEncode(modelsettingku.no_tlp));
            parameter.AppendFormat("{0}={1}&", "alamat_donatur", HttpUtility.UrlEncode(modelsettingku.alamat_donatur));
            //parameter.AppendFormat("{0}={1}&", "foto_donatur", HttpUtility.UrlEncode(modelsettingku.foto_donatur));


            try
            {
                WebClient clientLogin = new WebClient();
                clientLogin.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                clientLogin.Headers[HttpRequestHeader.ContentLength] = parameter.Length.ToString();

                clientLogin.UploadStringCompleted += new UploadStringCompletedEventHandler(uploadSettingComplete);
                clientLogin.UploadStringAsync(new Uri(URL.BASE + "api/setting/setting.php?id_donatur="+modelsettingku.id_donatur), "POST", parameter.ToString());
            }
            catch
            {
                Result = "false";
            }
        }

        private void uploadSettingComplete(object sender, UploadStringCompletedEventArgs e)
        {
            try
            {
                JObject jresult = JObject.Parse(e.Result);
                Result = jresult["result"].ToString();

                if (Result.Equals("sukses"))
                {
                    NavigationService.Navigate(new Uri("/Views/PageProfileDonatur.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Pengubahan pengaturan anda gagal, silahkan ulangi !");
                }

            }
            catch
            {
                throw;
            }
            
        }



        // Photo ===================================================
        private MemoryStream photoStream;
        private string fileName;
        private bool statusUpload;

        private void buttonChangePhoto_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            photoChooserTask.Show();
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                photoStream = new MemoryStream();
                e.ChosenPhoto.CopyTo(photoStream);
                fileName = e.OriginalFileName;
                statusUpload = true;
                
            }
        }

        private async void UploadPhoto()
        {
            try
            {

                // Make sure there is a picture selected
                if (photoStream != null)
                {
                    // initialize the client
                    // need to make sure the server accepts network IP-based requests
                    // ensure correct IP and correct port address
                    var fileUploadUrl = @URL.BASE + "api/setting/setting.php?id_donatur=" + Navigation.navIdLogin;
                    var client = new HttpClient();

                    // Reset the photoStream position
                    // If you don't reset the position, the content lenght sent will be 0
                    photoStream.Position = 0;

                    // This is the postdata
                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.Add(new StreamContent(photoStream), "foto_donatur", fileName);

                    // upload the file sending the form info and ensure a result.
                    // it will throw an exception if the service doesn't return a valid successful status code
                    await client.PostAsync(fileUploadUrl, content)
                        .ContinueWith((postTask) =>
                        {
                            postTask.Result.EnsureSuccessStatusCode();
                        });


                    ModelSetting modelsetting = new ModelSetting();
                    modelsetting.id_donatur = Navigation.navIdLogin;
                    modelsetting.username = textUsername.Text;
                    modelsetting.email_donatur = textEmail.Text;
                    modelsetting.no_tlp = textPhone.Text;
                    modelsetting.alamat_donatur = textAlamat.Text;

                    if (!textNewPassword.Equals(""))
                    {
                        if ((textNewPassword.Password).Equals(textConfirmPassword.Password))
                        {
                            modelsetting.password = textNewPassword.Password;
                            UpdateSetting(modelsetting);
                        }
                        else
                        {
                            MessageBox.Show("Comfirm New Password yang anda masukan tidak sama dengan password baru anda, silahkan ulangi lagi !");
                        }
                    }
                    else
                    {
                        modelsetting.password = textPassword.Password;
                        UpdateSetting(modelsetting);
                    }
                }
                else
                {
                    MessageBox.Show("Anda belum melengkapi data setting yang akan diubah, silahkan lengkapi terlebih dulu !");
                }
                statusUpload = false;
                

            }
            catch
            {
            }
        }*/


    }
}