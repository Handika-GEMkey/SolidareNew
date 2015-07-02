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
using System.Text.RegularExpressions;

namespace BantuAnakAsuh.Views
{
    public partial class PageRegister : PhoneApplicationPage
    {
        String[] listJKArray = { "Choose your gender", "Male", "Female" };
        String[] listIdCard = { "Choose your id card", "Identity card", "Drive license", "Passport" };

        public PageRegister()
        {
            InitializeComponent();
            this.listJenisKelamin.ItemsSource = listJKArray;
            this.listCardId.ItemsSource = listIdCard;
        }

        public string pwd;
        private void apBarRegister_Click(object sender, EventArgs e)
        {
            string username = Regex.Replace(textBoxUsername.Text, @"[^\w\s]", string.Empty);
            string password = passBox_password.Password;
            if (password.Length >= 8)  // Must be above 8 characters
            {
                pwd = password.ToString();
            }
            else
            {
                MessageBox.Show("Password minimum length is 8 character, please input again!");
            }
            
            //Random rnd = new Random();
            //pwd = rnd.Next(00000000, 99999999);

            string nama_donatur = textBoxFirstName.Text;
            if (textBoxLastName.Text.Equals(""))
            {
                nama_donatur = textBoxFirstName.Text + " " + textBoxLastName.Text;
            }
            else
            {
                nama_donatur = textBoxFirstName.Text + " " + textBoxLastName.Text;
            }

            StringBuilder parameter = new StringBuilder();
            parameter.AppendFormat("{0}={1}&", "username", HttpUtility.UrlEncode(username));
            parameter.AppendFormat("{0}={1}&", "password", HttpUtility.UrlEncode(pwd));
            parameter.AppendFormat("{0}={1}&", "name", HttpUtility.UrlEncode(nama_donatur));
            parameter.AppendFormat("{0}={1}&", "address", HttpUtility.UrlEncode(textBoxAddress.Text));
            parameter.AppendFormat("{0}={1}&", "email", HttpUtility.UrlEncode(textBoxEmail.Text));
            parameter.AppendFormat("{0}={1}&", "phone", HttpUtility.UrlEncode(textBoxPhone.Text));
            parameter.AppendFormat("{0}={1}&", "gender", HttpUtility.UrlEncode(listJenisKelamin.SelectedItem.ToString()));
            parameter.AppendFormat("{0}={1}&", "card_type", HttpUtility.UrlEncode(listCardId.SelectedItem.ToString()));
            parameter.AppendFormat("{0}={1}&", "id_number", HttpUtility.UrlEncode(textBoxIdNumber.Text));

            if (textBoxUsername.Text.Equals("") || textBoxPhone.Text.Equals("") ||
                nama_donatur.Equals("") || listJenisKelamin.SelectedItem.ToString().Equals("") ||
                textBoxEmail.Text.Equals("") || textBoxAddress.Equals("") ||
                textBoxIdNumber.Equals("") || listCardId.SelectedItem.ToString().Equals(""))
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
                    clientLogin.UploadStringAsync(new Uri(URL.BASE3 + "APIv2/landing/register.php"), "POST", parameter.ToString());
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
                string Result,message;
                JObject jresult = JObject.Parse(e.Result);
                Result = jresult["result"].ToString();
                message = jresult["message"].ToString();

                if (Result.Equals("success"))
                {
                    MessageBox.Show(message);
                    //SmsComposeTask SMSCompose = new SmsComposeTask();
                    //SMSCompose.To = textBoxPhone.Text;
                    //SMSCompose.Body = "Donors account registration. And this is your Donors Account Password: " + pwd.ToString();
                    //SMSCompose.Show();
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