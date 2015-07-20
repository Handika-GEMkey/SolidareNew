using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelPassword : ViewModelBase
    {
        string current_pwd, new_pwd;
        private bool status;

        public ICommand PublishCommand
        {
            get
            {
                return new DelegateCommand(PushToServer);
            }
        }

        private void PushToServer(object obj)
        {
            PRing = true;
            current_pwd = Navigation.cur_password.ToString();
            new_pwd = Navigation.new_password.ToString();
            try
            {
                PRing = true;
                status = true;
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/donors/change_password.php", Method.POST);

                if (current_pwd.Equals("") || new_pwd.Equals(""))
                {
                    MessageBox.Show("Please complete all field below.");
                }
                else
                {
                    request.AddHeader("content-type", "multipart/form-data");

                    request.AddParameter("id_donors", Navigation.navIdDonors);
                    request.AddParameter("token", Navigation.token);
                    request.AddParameter("cur_password", current_pwd);
                    request.AddParameter("new_password", new_pwd);

                    //calling server with restClient
                    RestClient restClient = new RestClient();
                    if (status == true)
                    {
                        PRing = true;
                        restClient.ExecuteAsync(request, (response) =>
                        {
                            ShellToast toast = new ShellToast();
                            toast.Title = "Status Upload";

                            //Try Catch because cannot Ggt json eesponse if upload photo
                            try
                            {
                                JObject jRoot = JObject.Parse(response.Content);
                                String result = jRoot.SelectToken("result").ToString();
                                String message = jRoot.SelectToken("message").ToString();
                                if (result == "failed")
                                {
                                    PRing = false;
                                    MessageBox.Show(message);
                                }
                                else
                                {
                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        if (result.Equals("success"))
                                        {
                                            PRing = false;
                                            MessageBox.Show("Your " + message.ToLower());
                                            var frame = App.Current.RootVisual as PhoneApplicationFrame;
                                            frame.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));

                                            //using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                                            //{
                                            //    using (IsolatedStorageFileStream rawStream = isf.CreateFile("password"))
                                            //    {
                                            //        StreamWriter writer = new StreamWriter(rawStream);
                                            //        writer.Write(Navigation.new_password);
                                            //        writer.Close();
                                            //    }
                                            //}
                                        }
                                        else
                                        {
                                            PRing = false;
                                            MessageBox.Show(message + " please try again!");
                                        }
                                    }
                                    else
                                    {
                                        //error ocured during upload
                                        PRing = false;
                                        MessageBox.Show("Failed to update your password, Please check your Internet connection.");
                                    }
                                }
                            }
                            catch
                            {
                                PRing = false;
                                MessageBox.Show("Failed to display, the Internet connection is unstable.");
                            }
                        });
                    }
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable." + ec.Message);
            }
            finally
            {
                //ProgressVisibiliy = Visibility.Visible;
            }
        }

        private Boolean pRing;

        public Boolean PRing
        {
            get { return pRing; }
            set { pRing = value; RaisePropertyChanged(""); }
        }

    }


}
