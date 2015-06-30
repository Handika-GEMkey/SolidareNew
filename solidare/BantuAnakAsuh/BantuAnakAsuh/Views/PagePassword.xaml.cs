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

namespace BantuAnakAsuh.Views
{
    public partial class PagePassword : PhoneApplicationPage
    {
        public PagePassword()
        {
            InitializeComponent();
        }

        private void apBarChangePassword_Click(object sender, EventArgs e)
        {
            string password = textBoxPwd.Password;
            string cpassword = textBoxCpwd.Password;

            StringBuilder parameter = new StringBuilder();
            
            parameter.AppendFormat("{0}={1}&", "password", HttpUtility.UrlEncode(password.ToString()));
            if (password.Equals("") || cpassword.Equals(""))
            {
                MessageBox.Show("Please complete all field below.");
            }
            else
            {
                try
                {
                    WebClient clientLogin = new WebClient();
                    clientLogin.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    clientLogin.Headers[HttpRequestHeader.ContentLength] = parameter.Length.ToString();
                    
                    clientLogin.UploadStringCompleted += new UploadStringCompletedEventHandler(uploadRegisterComplete);
                    clientLogin.UploadStringAsync(new Uri(URL.BASE3 + "api/setting/changepwd.php?id_donatur=" + Navigation.navIdLogin), "POST", parameter.ToString());
                }
                catch
                {
                    MessageBox.Show("An error occurred on the connection!");
                }
            } 
        }

        private void uploadRegisterComplete(object sender, UploadStringCompletedEventArgs e)
        {
            try
            {
                string Result;
                JObject jresult = JObject.Parse(e.Result);
                Result = jresult["result"].ToString();

                if (Result.Equals("sukses"))
                {
                    MessageBox.Show("Your account password has been updated!");
                    
                    NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("An error occurred when update. Please Repeat!");
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Update failed. Please check your connection!");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Update failed. Please check your connection!");
            }
            catch (WebException)
            {
                MessageBox.Show("Update failed. Please check your connection!");
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageSetting.xaml", UriKind.Relative));
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
        }
    }
}