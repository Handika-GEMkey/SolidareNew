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
using BantuAnakAsuh.Helper;
using System.Text;
using Newtonsoft.Json.Linq;
using BantuAnakAsuh.Models;
using System.IO.IsolatedStorage;
using System.IO;

namespace BantuAnakAsuh.Views
{
    public partial class PageLogin : PhoneApplicationPage
    {
        ModelLogin modelLogin = new ModelLogin();
        private String Result;
        
        public PageLogin()
        {
            InitializeComponent();
        }


        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            this.LoadUrl(textBox_email.Text, passBox_password.Password);
        }

        private void LoadUrl(String us, String pwd)
        {
            StringBuilder parameter = new StringBuilder();
            parameter.AppendFormat("{0}={1}&", "username", HttpUtility.UrlEncode(us));
            parameter.AppendFormat("{0}={1}&", "password", HttpUtility.UrlEncode(pwd));

            if (us.Equals("") || pwd.Equals(""))
            {
                Result = "Kosong";
                MessageBox.Show("Username or Password should not be empty!");
            }
            else
            {
                try
                {
                    WebClient clientLogin = new WebClient();
                    clientLogin.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    clientLogin.Headers[HttpRequestHeader.ContentLength] = parameter.Length.ToString();

                    clientLogin.UploadStringCompleted += new UploadStringCompletedEventHandler(uploadLoginComplete);
                    clientLogin.UploadStringAsync(new Uri(URL.BASE3 + "api/login/login.php"), "POST", parameter.ToString());
                }
                catch
                {
                    Result = "false";
                }
            }
        }

        private void uploadLoginComplete(object sender, UploadStringCompletedEventArgs e)
        {
            try
            {
                JObject jresult = JObject.Parse(e.Result);
                Result = jresult["result"].ToString();

                if (Result.Equals("sukses"))
                {
                    modelLogin.Id_donatur = jresult["id_donatur"].ToString();
                    modelLogin.Username = jresult["username"].ToString();
                    modelLogin.Nama_donatur = jresult["nama_donatur"].ToString();
                    modelLogin.Email_donatur = jresult["email_donatur"].ToString();
                    modelLogin.No_tlp = jresult["no_tlp"].ToString();
                    modelLogin.Jk_donatur = jresult["jk_donatur"].ToString();
                    modelLogin.Tgl_register = jresult["tgl_register"].ToString();
                    modelLogin.Status_donatur = jresult["status_donatur"].ToString();

                    using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (IsolatedStorageFileStream rawStream = isf.CreateFile("id_donatur"))
                        {
                            StreamWriter writer = new StreamWriter(rawStream);
                            writer.Write(modelLogin.Id_donatur);
                            writer.Close();
                        }

                        using (IsolatedStorageFileStream rawStream = isf.CreateFile("username"))
                        {
                            StreamWriter writer = new StreamWriter(rawStream);
                            writer.Write(modelLogin.Username);
                            writer.Close();
                        }

                        using (IsolatedStorageFileStream rawStream = isf.CreateFile("nama_donatur"))
                        {
                            StreamWriter writer = new StreamWriter(rawStream);
                            writer.Write(modelLogin.Nama_donatur);
                            writer.Close();
                        }

                        using (IsolatedStorageFileStream rawStream = isf.CreateFile("email_donatur"))
                        {
                            StreamWriter writer = new StreamWriter(rawStream);
                            writer.Write(modelLogin.Email_donatur);
                            writer.Close();
                        }

                        using (IsolatedStorageFileStream rawStream = isf.CreateFile("no_tlp"))
                        {
                            StreamWriter writer = new StreamWriter(rawStream);
                            writer.Write(modelLogin.No_tlp);
                            writer.Close();
                        }

                        using (IsolatedStorageFileStream rawStream = isf.CreateFile("jk_donatur"))
                        {
                            StreamWriter writer = new StreamWriter(rawStream);
                            writer.Write(modelLogin.Jk_donatur);
                            writer.Close();
                        }

                        using (IsolatedStorageFileStream rawStream = isf.CreateFile("tgl_register"))
                        {
                            StreamWriter writer = new StreamWriter(rawStream);
                            writer.Write(modelLogin.Tgl_register);
                            writer.Close();
                        }

                        using (IsolatedStorageFileStream rawStream = isf.CreateFile("status_donatur"))
                        {
                            StreamWriter writer = new StreamWriter(rawStream);
                            writer.Write(modelLogin.Status_donatur);
                            writer.Close();
                        }
                    }

                    NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Email and Password doesn't match!");
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Failed to login. Please check the Internet connection!");
                //progressBar1.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Failed to login. Please check the Internet connection!");
                //progressBar1.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (WebException)
            {
                MessageBox.Show("Failed to login. Please check the Internet connection!");
                //progressBar1.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageStart.xaml", UriKind.Relative));
        }

        private void textForgot_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageForgotPass.xaml", UriKind.Relative));
        }

    }
}