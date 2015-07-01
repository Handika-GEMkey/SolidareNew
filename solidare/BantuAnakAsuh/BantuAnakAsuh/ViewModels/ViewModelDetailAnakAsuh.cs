using BantuAnakAsuh.Common;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Models;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelDetailAnakAsuh : ModelDetailAnakAsuh
    {
        
        public ViewModelDetailAnakAsuh()
        {
            this.LoadUrlFosterChildren();
        }

        private void LoadUrlFosterChildren()
        {
            try
            {
                RestRequest request = new RestRequest(URL.BASE3 + "APIv2/fosterchildren/fosterchildren.php", Method.POST);
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("id_donors", Navigation.navIdDonors);
                request.AddParameter("token", Navigation.token);
                request.AddParameter("id_fosterchildren", Navigation.navIdAnak);

                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {

                    JObject jresult = JObject.Parse(response.Content);
                    String result = jresult.SelectToken("result").ToString();
                    JArray JItem = JArray.Parse(jresult.SelectToken("item").ToString());
                    foreach (JObject item in JItem)
                    {
                        Id_fosterchildren = item["id_fosterchildren"].ToString();
                        Name = item["name"].ToString();
                        Pob = item["pob"].ToString();
                        Dob = item["dob"].ToString();
                        Gender = item["gender"].ToString();
                        Address = item["address"].ToString();
                        Photo = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item["photo"].ToString();
                        Cost = item["cost"].ToString();
                        Children_status = item["children_status"].ToString();
                        Latitude = item["latitude"].ToString();
                        Longitude = item["longitude"].ToString();
                        Study_level = item["study_level"].ToString();
                        School = item["school"].ToString();
                        Grade = item["grade"].ToString();
                        Parent_name = item["parent_name"].ToString();
                        Parent_address = item["parent_address"].ToString();
                        Jobs = item["jobs"].ToString();
                        Salary = item["salary"].ToString();
                        Id_cha_org = item["id_cha_org"].ToString();
                        Cha_org_name = item["cha_org_name"].ToString();
                        Id_program = item["id_program"].ToString();
                        Program_name = item["program_name"].ToString();
                    }
                });
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }
        }

        //private void LoadUrl()
        //{
        //    try
        //    {
        //        RestRequest request = new RestRequest(URL.BASE3 + "APIv2/fosterchildren/fosterchildren.php", Method.POST);

        //        request.AddHeader("content-type", "multipart/form-data");
        //        request.AddParameter("id_donors", "871");
        //        request.AddParameter("token", ")GYaS6^cO!NL$eQDuzFZB952f");
        //        request.AddParameter("id_fosterchildren", Navigation.navIdAnak);

        //        //calling server with restClient
        //        RestClient restClient = new RestClient();
        //        restClient.ExecuteAsync(request, (response) =>
        //        {
        //            ShellToast toast = new ShellToast();
        //            toast.Title = "Status Upload";
        //            JObject jRoot = JObject.Parse(response.Content);
        //            String result = jRoot.SelectToken("result").ToString();
        //            JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());

        //            foreach (JObject item in JItem)
        //            {
                       
        //                Id_anak_asuh = item.SelectToken("id_fosterchildren").ToString();
        //                Nama_anak_asuh = item.SelectToken("name").ToString();
        //                Status_anak = item.SelectToken("children_status").ToString();
        //                Tempat_lahir = item.SelectToken("pob").ToString();
        //                Tanggal_lahir = item.SelectToken("dob").ToString();
        //                Jk_anak_asuh = item.SelectToken("gender").ToString();
        //                Alamat = item.SelectToken("address").ToString();
        //                Jenjang_pendidikan = item.SelectToken("study_level").ToString();
        //                Kelas = item.SelectToken("grade").ToString();
        //                Nama_sekolah = item.SelectToken("school").ToString();
        //                Foto_anak = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item.SelectToken("photo").ToString();
        //                Biaya_donasi = "Rp. " + item.SelectToken("cost").ToString() + ",-";
        //                Latitude = item.SelectToken("latitude").ToString();
        //                Longitude = item.SelectToken("longitude").ToString();
        //                Nama_orangtua_asli = item.SelectToken("parent_name").ToString();
        //                Alamat_orangtua_asli = item.SelectToken("parent_address").ToString();
        //                Pekerjaan = item.SelectToken("jobs").ToString();
        //                Penghasilan = item.SelectToken("salary").ToString();
        //                Ttl = Tempat_lahir + ", " + Tanggal_lahir;
        //                Jenjang = "Jenjang " + Jenjang_pendidikan;
        //                Stat = "Status " + Status_anak;
        //                Id_pogram = item.SelectToken("id_program").ToString();
        //                Nama_Org = item.SelectToken("cha_org_name").ToString();
        //                Navigation.idProgram = Id_pogram;
        //            }
        //        });
        //    }
        //    catch { }
        //}
        }
    }
