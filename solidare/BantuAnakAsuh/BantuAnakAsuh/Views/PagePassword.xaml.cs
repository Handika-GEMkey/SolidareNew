using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;
using Microsoft.Phone.Tasks;
using System.Text;
using BantuAnakAsuh.Helper;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Globalization;
using BantuAnakAsuh.ViewModels;

namespace BantuAnakAsuh.Views
{
    public partial class PagePassword : PhoneApplicationPage
    {
        Encrypt ec = new Encrypt();
        public PagePassword()
        {
            
            ViewModelPassword vmp = new ViewModelPassword();
            this.DataContext = vmp;
            InitializeComponent();
        }

        private void apBarChangePassword_Click(object sender, EventArgs e)
        {
            Navigation.cur_password = textBoxPwd.Password;
            Navigation.new_password = textBoxCpwd.Password;
            var vm = (ViewModelPassword)DataContext;
            vm.PublishCommand.Execute(null);
            //PRing = true;
            //string cur_password = textBoxPwd.Password;
            //string new_password = textBoxCpwd.Password;
            //String ecur_pwd = ec.EncryptIt(cur_password);
            //String enew_pwb = ec.EncryptIt(new_password);

            //StringBuilder parameter = new StringBuilder();
            //parameter.AppendFormat("{0}={1}&", "id_donors", HttpUtility.UrlEncode(Navigation.navIdDonors));
            //parameter.AppendFormat("{0}={1}&", "token", HttpUtility.UrlEncode(Navigation.token));
            //parameter.AppendFormat("{0}={1}&", "cur_password", HttpUtility.UrlEncode(cur_password.ToString()));
            //parameter.AppendFormat("{0}={1}&", "new_password", HttpUtility.UrlEncode(new_password.ToString()));
            
            //if (ecur_pwd.Equals("") || enew_pwb.Equals(""))
            //{
            //    MessageBox.Show("Please complete all field below.");
            //}
            //else
            //{
            //    try
            //    {
            //        WebClient clientLogin = new WebClient();
            //        clientLogin.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            //        clientLogin.Headers[HttpRequestHeader.ContentLength] = parameter.Length.ToString();

            //        clientLogin.UploadStringCompleted += new UploadStringCompletedEventHandler(uploadChangePasswordComplete);
            //        clientLogin.UploadStringAsync(new Uri(URL.BASE3 + "APIv2/donors/change_password.php"), "POST", parameter.ToString());
            //    }
            //    catch
            //    {
            //        PRing = false;
            //        MessageBox.Show("An error occurred on the connection!");
            //    }
            //} 
        }

        //private void uploadChangePasswordComplete(object sender, UploadStringCompletedEventArgs e)
        //{
        //    try
        //    {
        //        string Result,Message;
        //        JObject jresult = JObject.Parse(e.Result);
        //        Result = jresult["result"].ToString();
        //        Message = jresult["message"].ToString();
        //        if (Result.Equals("success"))
        //        {
        //            PRing = false;
        //            MessageBox.Show("Your " + Message.ToLower());
                    
        //            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        //        }
        //        else
        //        {
        //            PRing = false;
        //            MessageBox.Show(Message+", please input again!");
        //        }
        //    }
        //    catch (TimeoutException)
        //    {
        //        PRing = false;
        //        MessageBox.Show("Update failed. Please check your connection!");
        //    }
        //    catch (NullReferenceException)
        //    {
        //        PRing = false;
        //        MessageBox.Show("Update failed. Please check your connection!");
        //    }
        //    catch (WebException)
        //    {
        //        PRing = false;
        //        MessageBox.Show("Update failed. Please check your connection!");
        //    }
        //}

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageSetting.xaml", UriKind.Relative));
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageSetting.xaml", UriKind.Relative));
        }

        
    }
}