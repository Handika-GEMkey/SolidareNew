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
        //Encrypt ec = new Encrypt();
        //ModelLogin modelLogin = new ModelLogin();
        //private String Result;

        public PageLogin()
        {
            InitializeComponent();
        }


        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            Navigation.username = textBox_email.Text;
            //String username = ec.EncryptIt(messageUser);
            Navigation.password = passBox_password.Password;
            LayoutRoot.Opacity = 4.5;
            LoadingRing.Visibility = Visibility.Visible;
            LoadingRing.IsActive = true;
            this.DataContext = new ViewModelLogin();
            
            //String password = ec.EncryptIt(messagePwd);

            //this.LoadUrl(username, password);
        }

        //private void LoadUrl(String us, String pwd)
        //{
        //    StringBuilder parameter = new StringBuilder();
        //    parameter.AppendFormat("{0}={1}&", "username", HttpUtility.UrlEncode(us));
        //    parameter.AppendFormat("{0}={1}&", "password", HttpUtility.UrlEncode(pwd));
        //    parameter.AppendFormat("{0}={1}&", "tag", HttpUtility.UrlEncode(us));
        //    Navigation.Tag = parameter.AppendFormat("{0}={1}&", "tag", HttpUtility.UrlEncode(us)).ToString();

        //    if (us.Equals("") || pwd.Equals(""))
        //    {
        //        Result = "Kosong";
        //        MessageBox.Show("Username or Password should not be empty!");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            WebClient clientLogin = new WebClient();
        //            clientLogin.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //            clientLogin.Headers[HttpRequestHeader.ContentLength] = parameter.Length.ToString();

        //            clientLogin.UploadStringCompleted += new UploadStringCompletedEventHandler(uploadLoginComplete);
        //            clientLogin.UploadStringAsync(new Uri(URL.BASE3 + "APIv2/landing/login.php"), "POST", parameter.ToString());
        //        }
        //        catch
        //        {
        //            Result = "false";
        //        }
        //    }
        //}

        //private void uploadLoginComplete(object sender, UploadStringCompletedEventArgs e)
        //{
        //    try
        //    {
        //        JObject jresult = JObject.Parse(e.Result);
        //        Result = jresult["result"].ToString();

        //        if (Result.Equals("success"))
        //        {
        //            modelLogin.Id_donatur = jresult["id_donors"].ToString();
        //            modelLogin.FullName = jresult["fullname"].ToString();
        //            modelLogin.Token = jresult["token"].ToString();
        //            Navigation.navIdDonors = jresult["id_donors"].ToString();
        //            Navigation.token = jresult["token"].ToString();
        //            Navigation.navIdLogin = jresult["id_donors"].ToString();

        //            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
        //            {
        //                using (IsolatedStorageFileStream rawStream = isf.CreateFile("id_donors"))
        //                {
        //                    StreamWriter writer = new StreamWriter(rawStream);
        //                    writer.Write(modelLogin.Id_donatur);
        //                    writer.Close();
        //                }

        //                using (IsolatedStorageFileStream rawStream = isf.CreateFile("fullname"))
        //                {
        //                    StreamWriter writer = new StreamWriter(rawStream);
        //                    writer.Write(modelLogin.FullName);
        //                    writer.Close();
        //                }

        //                using (IsolatedStorageFileStream rawStream = isf.CreateFile("token"))
        //                {
        //                    StreamWriter writer = new StreamWriter(rawStream);
        //                    writer.Write(modelLogin.Token);
        //                    writer.Close();
        //                }
        //            }
        //            LoadingBar.Visibility = Visibility.Visible;
        //            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        //        }
        //        else
        //        {
        //            MessageBox.Show("Email and Password doesn't match!");
        //        }
        //    }
        //    catch (TimeoutException)
        //    {
        //        MessageBox.Show("Failed to login. Please check the Internet connection!");
        //        //progressBar1.Visibility = System.Windows.Visibility.Collapsed;
        //    }
        //    catch (NullReferenceException)
        //    {
        //        MessageBox.Show("Failed to login. Please check the Internet connection!");
        //        //progressBar1.Visibility = System.Windows.Visibility.Collapsed;
        //    }
        //    catch (WebException)
        //    {
        //        MessageBox.Show("Failed to login. Please check the Internet connection!");
        //        //progressBar1.Visibility = System.Windows.Visibility.Collapsed;
        //    }

        //}

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