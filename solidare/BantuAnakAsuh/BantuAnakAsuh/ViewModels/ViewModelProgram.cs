using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelProgram : ModelProgram
    {
        //private ObservableCollection<ModelProgram> collectionProgram = new ObservableCollection<ModelProgram>();
        //public ObservableCollection<ModelProgram> CollectionProgram
        //{
        //    get { return collectionProgram; }
        //    set
        //    {
        //        if (this.collectionProgram != value)
        //        {
        //            collectionProgram = value;
        //            RaisePropertyChanged("");
        //        }
        //    }
        //}
        public ViewModelProgram()
        {
            LoadUrl();
        }

        private void LoadUrl()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/charityorganization/charity_program.php", Method.POST);

                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);
                request.AddParameter("id_program", Navigation.idProgram);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {
                    ShellToast toast = new ShellToast();
                    toast.Title = "Status Upload";
                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());

                    foreach (JObject item in JItem)
                    {
                       
                        Id_program = item.SelectToken("id_program").ToString();
                        Program_name = item.SelectToken("program_name").ToString();
                        Description = item.SelectToken("description").ToString();
                        Navigation.navIdAnak = Navigation.id_fosterchildren;
                    }
                });
            }
            catch { }
        }
    }
}
