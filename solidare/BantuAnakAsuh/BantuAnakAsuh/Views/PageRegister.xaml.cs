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
        String[] listJKArray = { "Choose your gender", "Male", "Female", "Other" };
        String[] listIdCard = { "Choose your ID Card", "Identity card", "Drive license", "Passport" };

        public void Reload()
        {
            tb_password.Password = "";
            tb_confirm_pwd.Password = "";
            InitializeComponent();
        }

        public PageRegister()
        {
            InitializeComponent();
            this.listJenisKelamin.ItemsSource = listJKArray;
            this.listCardId.ItemsSource = listIdCard;
        }

        public string pwd, gender,valid_email,valid_username;
        private void apBarRegister_Click(object sender, EventArgs e)
        {
            string nama_donatur = textBoxFirstName.Text;
            //if (textBoxLastName.Text.Equals(""))
            //{
            //    nama_donatur = textBoxFirstName.Text + " " + textBoxLastName.Text;
            //}
            //else
            //{
            //    nama_donatur = textBoxFirstName.Text + " " + textBoxLastName.Text;
            //}

            StringBuilder parameter = new StringBuilder();
            parameter.AppendFormat("{0}={1}&", "username", HttpUtility.UrlEncode(valid_username));
            parameter.AppendFormat("{0}={1}&", "password", HttpUtility.UrlEncode(pwd));
            parameter.AppendFormat("{0}={1}&", "name", HttpUtility.UrlEncode(nama_donatur));
            parameter.AppendFormat("{0}={1}&", "address", HttpUtility.UrlEncode(textBoxAddress.Text));
            parameter.AppendFormat("{0}={1}&", "email", HttpUtility.UrlEncode(valid_email));
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
                Reload();
            }
            else
            {
                LayoutRoot.Opacity = 4.5;
                LoadingRing.Visibility = Visibility.Visible;
                LoadingRing.IsActive = true;

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
                

                string Result, message;
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
                    //LoadingBar.Visibility = Visibility.Visible;
                    
                    NavigationService.Navigate(new Uri("/Views/PageLogin.xaml", UriKind.Relative));
                    LoadingRing.Visibility = Visibility.Collapsed;
                    LoadingRing.IsActive = false;
                }
                else
                {
                    MessageBox.Show("An error occurred when registration. Please Repeat!");
                    Reload();
                    LoadingRing.Visibility = Visibility.Collapsed;
                    LoadingRing.IsActive = false;
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

        #region validation username,password,email
        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
            { return (true); }
            else
            {

                return (false);
            }
        }

        private void tb_password_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            string password = tb_password.Password;
            string confirm_password = tb_confirm_pwd.Password;

            if (password.Length < 8)
            {
                txt_notif_pwd.Text = "Password minimum length is 8 character";
                txt_notif_pwd.Visibility = Visibility.Visible;
                
            }
            else
            {
                txt_notif_pwd.Visibility = Visibility.Collapsed;
                pwd = password.ToString();

            }
        }

        private void tb_confirm_pwd_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            string confirm_password = tb_confirm_pwd.Password;
            if(pwd != confirm_password)
            {
                txt_notif_confirm_pwd.Text = "Password does not match, try again!";
                txt_notif_confirm_pwd.Visibility = Visibility.Visible;

            }else
            {
                txt_notif_confirm_pwd.Visibility = Visibility.Collapsed;
                pwd = confirm_password;
            }
        }

        private void textBoxEmail_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bool cek_email = isValidEmail(textBoxEmail.Text);
            if (cek_email == true)
            {
                string vemail = textBoxEmail.Text;
                txt_notif_email.Visibility = Visibility.Collapsed;
                valid_email = vemail;
            }
            else
            {
                txt_notif_email.Text = "Your email is incorrect, try again!";
                txt_notif_email.Visibility = Visibility.Visible;
            }
        }

        private void textBoxUsername_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Regex re = new Regex(@"^[A-Za-z0-9\[\]/!$%^&*()\-_+{};:'£@#.?]*$");
            string a = textBoxUsername.Text;
            if (re.IsMatch(a))
            {
                valid_username = a;
                txt_notif_username.Visibility = Visibility.Collapsed;
            }
            else { 
                txt_notif_username.Text = "Username shouldn't be any white space"; 
                txt_notif_username.Visibility = Visibility.Visible; 
            }
        }
        #endregion

    }
}
