using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using Microsoft.Phone.Controls;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelHasilFilter : ViewModelBase
    {
        
        private ObservableCollection<ModelDetailAnakAsuh> collectionFillter = new ObservableCollection<ModelDetailAnakAsuh>();
        public ObservableCollection<ModelDetailAnakAsuh> CollectionFillter
        {
            get { return collectionFillter; }
            set
            {
                if (this.collectionFillter != value)
                {
                    collectionFillter = value;
                    RaisePropertyChanged("");
                }
            }
        }

        public ViewModelHasilFilter()
        {
            this.LoadUrl();
        }

        private void LoadUrl()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/fosterchildren/fosterchildren.php", Method.POST);
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);
                
                if (!Navigation.Fill_Status.Equals("Choose Status"))
                {
                    request.AddParameter("children_status", Navigation.Fill_Status);
                }
                if (!Navigation.Fill_Jk.Equals("Choose Gender"))
                {
                    request.AddParameter("gender", Navigation.Fill_Jk);
                }
                if (!Navigation.Fill_Jenjang.Equals("Choose Study Level"))
                {
                    request.AddParameter("study_level", Navigation.Fill_Jenjang);
                }
                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {

                    JObject jresult = JObject.Parse(response.Content);
                    String result = jresult.SelectToken("result").ToString();
                    if (result == "failed")
                    {
                        MessageBox.Show("Failed to display!");
                        var frame = App.Current.RootVisual as PhoneApplicationFrame;
                        frame.Navigate(new Uri("/Views/PageFilter.xaml", UriKind.Relative));
                    }
                    else
                    {
                        JArray JItem = JArray.Parse(jresult.SelectToken("item").ToString());
                        foreach (JObject item in JItem)
                        {
                            ModelDetailAnakAsuh mAnakAsuh = new ModelDetailAnakAsuh();
                            mAnakAsuh.Id_fosterchildren = item["id_fosterchildren"].ToString();
                            mAnakAsuh.Name = item["name"].ToString();
                            mAnakAsuh.Photo = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item["photo"].ToString();
                            mAnakAsuh.Gender = item["gender"].ToString();
                            mAnakAsuh.Children_status = item["children_status"].ToString();
                            mAnakAsuh.Study_level = item["study_level"].ToString();
                            mAnakAsuh.Id_program = item["id_program"].ToString();
                            Navigation.idProgram = mAnakAsuh.Id_program;
                            Navigation.id_fosterchildren = mAnakAsuh.Id_fosterchildren;
                            collectionFillter.Add(mAnakAsuh);
                        }
                    }
                });
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Failed to display!");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Failed to display!");
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }

        }
        
        private int listIndex = -1;
        public int ListIndex
        {
            get { return listIndex; }
            set
            {
                if (listIndex != value)
                    listIndex = value;
                RaisePropertyChanged("");
            }
        }

        public ICommand SetFilter
        {
            get
            {
                return new DelegateCommand(SetIdAnak, CanSetIdAnak);
            }
        }

        private bool CanSetIdAnak(object arg)
        {
            return true;
        }

        private void SetIdAnak(object obj)
        {
            ModelDetailAnakAsuh SelectedItem = obj as ModelDetailAnakAsuh;

            if (SelectedItem != null)
                Navigation.id_fosterchildren = SelectedItem.Id_fosterchildren;

            listIndex = -1;
        }
    }
}
