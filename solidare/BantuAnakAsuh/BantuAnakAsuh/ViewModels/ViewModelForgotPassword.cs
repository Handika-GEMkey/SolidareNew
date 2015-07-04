using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using Microsoft.Phone.Controls;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelForgotPassword : ViewModelBase
    {
        ModelPassword mp = new ModelPassword();
        public void LoadUrlDonorProfile()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/landing/forgot.php", Method.POST);
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("email", Navigation.email);
                request.AddParameter("phone", Navigation.phone);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {

                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    String message = jRoot.SelectToken("message").ToString();
                    if (result.Equals("success"))
                    {
                        MessageBox.Show(message);
                        var frame = App.Current.RootVisual as PhoneApplicationFrame;
                        frame.Navigate(new Uri("/Views/PageLogin.xaml", UriKind.Relative));
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
