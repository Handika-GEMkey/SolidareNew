using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
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
        private String id_donatur;

        public String Id_donatur
        {
            get { return id_donatur; }
            set { id_donatur = value; RaisePropertyChanged(""); }
        }

        private bool status;
        private void PushToServer(object obj)
        {
            try
            {
                status = true;
                RestRequest request = new RestRequest(URL.BASE3 + "api/setting/changepwd.php?id_donatur=" + Navigation.navIdLogin, Method.POST);

                if (Password == null || Confirmpassword == null )
                {
                    MessageBox.Show("All Textbox Must Be Filled");
                    status = false;
                }
                else
                {
                    request.AddHeader("content-type", "multipart/form-data");
                    
                    if (Password == Confirmpassword)
                    {
                        request.AddParameter("password", Password);
                    }
                    else
                    {
                        MessageBox.Show("Confirm new password that you entered is not same with your new password, please try again!");
                        status = false;
                    }
                    

                    //calling server with restClient
                    RestClient restClient = new RestClient();
                    if (status == true)
                    {
                        restClient.ExecuteAsync(request, (response) =>
                        {
                            ShellToast toast = new ShellToast();
                            toast.Title = "Status Upload";

                            //Try Catch because cannot Ggt json eesponse if upload photo
                            try
                            {
                                JObject jRoot = JObject.Parse(response.Content);
                                String result = jRoot.SelectToken("result").ToString();
                                if (result == "failed")
                                {
                                    MessageBox.Show("Failed to display!");
                                }
                                else
                                {
                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        if (result.Equals("sukses"))
                                        {
                                            MessageBox.Show("Your password has been updated");

                                        }
                                        else
                                        {
                                            MessageBox.Show("Something Wrong");
                                        }
                                    }
                                    else
                                    {
                                        //error ocured during upload
                                        MessageBox.Show("Failed to post, Please check your Internet connection.");
                                    }
                                }
                            }
                            catch
                            {
                                
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

        private String password;

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        private String confirmpassword;

        public String Confirmpassword
        {
            get { return confirmpassword; }
            set { confirmpassword = value; }
        }
    }


}
