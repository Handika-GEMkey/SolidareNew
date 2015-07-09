using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using BantuAnakAsuh.Views;
using Microsoft.Phone.Controls;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelLogin : ViewModelBase
    {
        Encrypt et = new Encrypt();
        ModelLogin modelLogin = new ModelLogin();
        
        private String Result;
        
        public ViewModelLogin()
        {
            this.LoadUrlDonorProfile();
            
        }

        private void LoadUrlDonorProfile()
        {
            String usr = et.EncryptIt(Navigation.username);
            String pwd = et.EncryptIt(Navigation.password);
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/landing/login.php", Method.POST);
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("username", usr);
                request.AddParameter("password", pwd);
                request.AddParameter("channel", usr);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {

                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    //String message = jRoot.SelectToken("message").ToString();
                    if (result.Equals("success"))
                    {
                        //MessageBox.Show(message);
                        modelLogin.Id_donatur = jRoot["id_donors"].ToString();
                        modelLogin.FullName = jRoot["fullname"].ToString();
                        modelLogin.Token = jRoot["token"].ToString();
                        Navigation.navIdDonors = jRoot["id_donors"].ToString();
                        Navigation.token = jRoot["token"].ToString();
                        Navigation.navIdLogin = jRoot["id_donors"].ToString();

                        using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                        {
                            using (IsolatedStorageFileStream rawStream = isf.CreateFile("id_donors"))
                            {
                                StreamWriter writer = new StreamWriter(rawStream);
                                writer.Write(modelLogin.Id_donatur);
                                writer.Close();
                            }

                            using (IsolatedStorageFileStream rawStream = isf.CreateFile("fullname"))
                            {
                                StreamWriter writer = new StreamWriter(rawStream);
                                writer.Write(modelLogin.FullName);
                                writer.Close();
                            }

                            using (IsolatedStorageFileStream rawStream = isf.CreateFile("token"))
                            {
                                StreamWriter writer = new StreamWriter(rawStream);
                                writer.Write(modelLogin.Token);
                                writer.Close();
                            }
                        }
                        var frame = App.Current.RootVisual as PhoneApplicationFrame;
                        frame.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));

                    }
                    else
                    {
                        MessageBox.Show("Email and password does not match, try again!");
                        
                    }
                });
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }
        }
        
    }
}
