using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Text;
using BantuAnakAsuh.Helper;
using Newtonsoft.Json.Linq;
using BantuAnakAsuh.Models;
using BantuAnakAsuh.ViewModels;

namespace BantuAnakAsuh.Views
{
    public partial class PageForgotPass : PhoneApplicationPage
    {
        ModelPassword mp = new ModelPassword();
        public PageForgotPass()
        {
            InitializeComponent();
            
            //this.DataContext = new ViewModelForgotPassword();
        }

        private void apBarResetPassword_Click(object sender, EventArgs e)
        {
            Navigation.email = textBox_email.Text.ToString();
            Navigation.phone = textBox_phone.Text.ToString();
            ViewModelForgotPassword vp = new ViewModelForgotPassword();
            vp.LoadUrlDonorProfile();
            //this.LoadUrl(textBox_email.Text, textBox_phone.Text);
        }

        //private void LoadUrl(string email_donatur, string phone)
        //{
        //    StringBuilder parameter = new StringBuilder();
        //    parameter.AppendFormat("{0}={1}&", "email_donatur", HttpUtility.UrlEncode(email_donatur));
        //    parameter.AppendFormat("{0}={1}&", "phone", HttpUtility.UrlEncode(phone));
           
        //    if (email_donatur.Equals(""))
        //    {
        //        MessageBox.Show("Input your email!");
        //    }
        //    else if (phone.Equals(""))
        //    {
        //        MessageBox.Show("Input your phone number!");
        //    }
        //    else
        //    {

        //        try
        //        {
        //            WebClient clientLogin = new WebClient();
        //            clientLogin.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //            clientLogin.Headers[HttpRequestHeader.ContentLength] = parameter.Length.ToString();

        //            clientLogin.UploadStringCompleted += new UploadStringCompletedEventHandler(uploadResetComplete);
        //            clientLogin.UploadStringAsync(new Uri(URL.BASE3 + "APIv2/landing/forgot.php"), "POST", parameter.ToString());
        //        }
        //        catch
        //        {
        //            MessageBox.Show("An error occured!");
        //        }
        //    }
        //}

        //private void uploadResetComplete(object sender, UploadStringCompletedEventArgs e)
        //{
        //    try
        //    {
        //        JObject jresult = JObject.Parse(e.Result);
        //        string Result = jresult["result"].ToString();

        //        if (Result.Equals("success"))
        //        {
        //            MessageBox.Show("Please check your email!");
        //            NavigationService.Navigate(new Uri("/Views/PageStart4.xaml", UriKind.Relative));
        //        }
        //        else
        //        {
        //            MessageBox.Show("Your email is not registered");
        //        }
        //    }
        //    catch (TimeoutException)
        //    {
        //        MessageBox.Show("Login failed. Please check your internet connection");
        //    }
        //    catch (NullReferenceException)
        //    {
        //        MessageBox.Show("Login failed. Please check your username or password");
        //    }
        //    catch (WebException)
        //    {
        //        MessageBox.Show("Login failed. Please check your internet connection");
        //    }

        //}

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageLogin.xaml", UriKind.Relative));
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageLogin.xaml", UriKind.Relative));
        }
    }
}