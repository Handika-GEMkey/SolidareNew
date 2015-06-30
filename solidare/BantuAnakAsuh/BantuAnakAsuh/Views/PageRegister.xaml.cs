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
    public partial class PageRegister : PhoneApplicationPage
    {
        String[] listJKArray = { "Choose your gender", "Male", "Female" };

        public PageRegister()
        {
            InitializeComponent();
            this.listJenisKelamin.ItemsSource = listJKArray;
        }

        public int pwd;
        private void apBarRegister_Click(object sender, EventArgs e)
        {
            
            Random rnd = new Random();
            pwd = rnd.Next(00000000, 99999999);

            string nama_donatur = textBoxFirstName.Text;
            if (textBoxLastName.Text.Equals(""))
            {
                nama_donatur = textBoxFirstName.Text+ " " +textBoxLastName.Text;
            }

            StringBuilder parameter = new StringBuilder();
            parameter.AppendFormat("{0}={1}&", "username", HttpUtility.UrlEncode(textBoxUsername.Text));
            parameter.AppendFormat("{0}={1}&", "password", HttpUtility.UrlEncode(pwd.ToString()));
            parameter.AppendFormat("{0}={1}&", "nama_donatur", HttpUtility.UrlEncode(nama_donatur));
            parameter.AppendFormat("{0}={1}&", "tgl_register", HttpUtility.UrlEncode(DateTime.UtcNow.Date.ToString("yyyy-MM-dd")));
            parameter.AppendFormat("{0}={1}&", "jk_donatur", HttpUtility.UrlEncode(listJenisKelamin.SelectedItem.ToString()));
            parameter.AppendFormat("{0}={1}&", "email_donatur", HttpUtility.UrlEncode(textBoxEmail.Text));
            parameter.AppendFormat("{0}={1}&", "no_tlp", HttpUtility.UrlEncode(textBoxPhone.Text));
            //parameter.AppendFormat("{0}={1}&", "gcmid", HttpUtility.UrlEncode(null));

            if (textBoxUsername.Text.Equals("") || textBoxPhone.Text.Equals("") || nama_donatur.Equals("") || listJenisKelamin.SelectedItem.ToString().Equals("") || textBoxEmail.Text.Equals(""))
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
                    clientLogin.UploadStringAsync(new Uri(URL.BASE3 + "api/register/register.php"), "POST", parameter.ToString());
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
                    MessageBox.Show("Registration success!");
                    SmsComposeTask SMSCompose = new SmsComposeTask();
                    SMSCompose.To = textBoxPhone.Text;
                    SMSCompose.Body = "Donors account registration. And this is your Donors Account Password: " + pwd.ToString();
                    SMSCompose.Show();
                    NavigationService.Navigate(new Uri("/Views/PageLogin.xaml", UriKind.Relative));
                }
                else

                {
                    MessageBox.Show("An error occurred when registration. Please Repeat!");
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Login failed. Please check your connection!");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Login failed. Please check your connection!");
            }
            catch (WebException)
            {
                MessageBox.Show("Login failed. Please check your connection!");
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageStart.xaml", UriKind.Relative));
        }

    }
}