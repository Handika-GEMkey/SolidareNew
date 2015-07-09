using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
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
    class ViewModelRapot : ViewModelBase
    {
        private String Result;
        ModelRapot modelRapot = new ModelRapot();
        ModelProfileDonatur mProfileDonatur = new ModelProfileDonatur();
        public bool konek = true;

        private ObservableCollection<ModelProfileDonatur> collectionAnakAsuh = new ObservableCollection<ModelProfileDonatur>();
        public ObservableCollection<ModelProfileDonatur> CollectionAnakAsuh
        {
            get { return collectionAnakAsuh; }
            set
            {
                if (this.collectionAnakAsuh != value)
                {
                    collectionAnakAsuh = value;
                    RaisePropertyChanged("");
                }
            }
        }

        private ObservableCollection<ModelRapot> collectionRapot = new ObservableCollection<ModelRapot>();
        public ObservableCollection<ModelRapot> CollectionRapot
        {
            get { return collectionRapot; }
            set
            {
                if (this.collectionRapot != value)
                {
                    collectionRapot = value;
                    RaisePropertyChanged("");
                }
            }
        }

        public ViewModelRapot()
        {
            this.LoadUrlReport();
        }

        private void LoadUrlReport()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/report/report.php", Method.POST);
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);
                request.AddParameter("id_fosterchildren", Navigation.id_fosterchildren);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {

                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    if (result == "failed")
                    {
                        MessageBox.Show("Failed to display!");
                    }
                    else
                    {
                        JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                        foreach (JObject item in JItem)
                        {
                            modelRapot.report_date = item["report_date"].ToString();
                            modelRapot.report_file = URL.BASE3 + "modul/mod_Laporan/laporan/" + item["report_file"].ToString();

                            if ((modelRapot.semester = item["semester"].ToString()) == "1")
                            {
                                modelRapot.semester = item["semester"].ToString() + "st Semester";
                            }
                            else if ((modelRapot.semester = item["semester"].ToString()) == "2")
                            {
                                modelRapot.semester = item["semester"].ToString() + "nd Semester";
                            }
                            else if ((modelRapot.semester = item["semester"].ToString()) == "3")
                            {
                                modelRapot.semester = item["semester"].ToString() + "rd Semester";
                            }
                            else
                            {
                                modelRapot.semester = item["semester"].ToString() + "th Semester";
                            }
                        }
                        collectionRapot.Add(modelRapot);
                    }
                });
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }
        }

        //private void sendReportComplete(object sender, UploadStringCompletedEventArgs e)
        //{
        //    try
        //    {
        //        JObject jresult = JObject.Parse(e.Result);
        //        JArray JItem = JArray.Parse(jresult.SelectToken("item").ToString());
        //        Result = jresult["result"].ToString();

        //        if (Result.Equals("success"))
        //        {

        //            modelRapot.id_fosterchildren = item["id_fosterchildren"].ToString();
        //            modelRapot.id_report = item["id_report"].ToString();
        //            modelRapot.name = item["name"].ToString();
        //            modelRapot.study_level = item["study_level"].ToString();
        //            modelRapot.school = item["school"].ToString();
        //            modelRapot.grade = item["grade"].ToString();

        //            modelRapot.report_date = JItem["report_date"].ToString();
        //            modelRapot.report_file = URL.BASE3 + "modul/mod_Laporan/laporan/" + JItem["report_file"].ToString();
        //            modelRapot.semester = JItem["semester"].ToString();

        //            foreach (var item in JItem)
        //            {
        //                modelRapot.report_file = URL.BASE3 + "modul/mod_Laporan/laporan/" + jresult["report_file"].ToString();

        //                if ((modelRapot.semester = item["semester"].ToString()) == "1")
        //                {
        //                    modelRapot.semester = item["semester"].ToString() + "st Semester";
        //                }
        //                else if ((modelRapot.semester = item["semester"].ToString()) == "2")
        //                {
        //                    modelRapot.semester = item["semester"].ToString() + "nd Semester";
        //                }
        //                else if ((modelRapot.semester = item["semester"].ToString()) == "3")
        //                {
        //                    modelRapot.semester = item["semester"].ToString() + "rd Semester";
        //                }
        //                else
        //                {
        //                    modelRapot.semester = item["semester"].ToString() + "th Semester";
        //                }
        //            }

        //            collectionRapot.Add(modelRapot);
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch
        //    {
        //        konek = false;
        //    }
        //}

        //private void DownloadRapot(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    try
        //    {
        //        JObject jresult = JObject.Parse(e.Result);
        //        JArray JItem = JArray.Parse(jresult.SelectToken("item").ToString());
        //        foreach (JObject item in JItem)
        //        {
        //            ModelRapot modelRapot = new ModelRapot();
        //            modelRapot.file_laporan = URL.BASE3 + "modul/mod_Laporan/laporan/" + item.SelectToken("file_laporan").ToString();
        //            if (item.SelectToken("semester_pr").ToString() == "1")
        //            {
        //                modelRapot.semester_pr = item.SelectToken("semester_pr").ToString() + "st Semester";
        //            }
        //            else if (item.SelectToken("semester_pr").ToString() == "2")
        //            {
        //                modelRapot.semester_pr = item.SelectToken("semester_pr").ToString() + "nd Semester";
        //            }
        //            else if (item.SelectToken("semester_pr").ToString() == "3")
        //            {
        //                modelRapot.semester_pr = item.SelectToken("semester_pr").ToString() + "rd Semester";
        //            }
        //            else
        //            {
        //                modelRapot.semester_pr = item.SelectToken("semester_pr").ToString() + "th Semester";
        //            }
        //            collectionRapot.Add(modelRapot);
        //        }
        //    }
        //    catch
        //    {
        //        konek = false;
        //    }
        //}

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
    }
}
