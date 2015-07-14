using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BantuAnakAsuh.Models;
using System.IO.IsolatedStorage;
using System.Net.Http;
using BantuAnakAsuh.Helper;
using Newtonsoft.Json.Linq;
using BantuAnakAsuh.ViewModels;

namespace BantuAnakAsuh.Views
{
    public partial class DetailAnakAsuhNearby : PhoneApplicationPage
    {
        bool isOpenNavDraw;
        double marginLeft;
        public String sourceAnak = "";
        IsolatedStorageFile setting = IsolatedStorageFile.GetUserStoreForApplication();
        ModelDetailAnakAsuh detail_child = new ModelDetailAnakAsuh();
       

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg = "";
            if (NavigationContext.QueryString.TryGetValue("anak", out msg))
            {
                sourceAnak = msg;
            }
        }

        public DetailAnakAsuhNearby()
        {
            InitializeComponent();
            this.DataContext = detail_child;

        }

        public async void initData(string x)
        {
            
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("id_donors", Navigation.navIdDonors),
                    new KeyValuePair<string, string>("token", Navigation.token), 
                    new KeyValuePair<string, string>("id_fosterchildren", x)
                });
                var client = new HttpClient();
                var responResult = await client.PostAsync("http://solidare.azurewebsites.net/admin/APIv2/fosterchildren/detail_fosterchildren.php", formContent);
                
                var stringContent = await responResult.Content.ReadAsStringAsync();

                //ViewModelDetailNearby vd = new ViewModelDetailNearby();
                //vd.LoadUrlFosterChildren(stringContent);
                
                retrieve_kejahatan(stringContent);
                client.Dispose();
            }
            catch { }
        }

        public void retrieve_kejahatan(string x)
        {
            try
            {
                /************Binding dari Database**************/
                JObject jRoot = JObject.Parse(x);
                JArray Jitem = JArray.Parse(jRoot.SelectToken("item").ToString());
                foreach (JObject item in Jitem)
                {
                    
                    detail_child.Name = item.SelectToken("name").ToString();
                    
                    detail_child.Photo = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item["photo"].ToString();
                    detail_child.Children_status = item.SelectToken("children_status").ToString();
                    detail_child.Address = item.SelectToken("address").ToString();
                    
                    detail_child.Pob = item.SelectToken("pob").ToString();
                    detail_child.Study_level = item.SelectToken("study_level").ToString();
                    
                    detail_child.Gender = item.SelectToken("gender").ToString();
                    detail_child.Grade = item.SelectToken("grade").ToString();
                    detail_child.School = item.SelectToken("school").ToString();
                    detail_child.Cost = "Rp. " + item.SelectToken("cost").ToString() + ",-";
                    detail_child.Jobs = item.SelectToken("jobs").ToString();
                    detail_child.Salary = item.SelectToken("salary").ToString();
                    detail_child.Cha_org_name = item.SelectToken("cha_org_name").ToString();
                    detail_child.Program_name = item.SelectToken("program_name").ToString();
                    detail_child.Parent_name = item.SelectToken("parent_name").ToString();
                    detail_child.Parent_address = item.SelectToken("parent_address").ToString();
                    Navigation.id_fosterchildren = item.SelectToken("id_fosterchildren").ToString();
                    Navigation.idProgram = item.SelectToken("id_program").ToString();
                    Navigation.navId_cha_org = item.SelectToken("id_cha_org").ToString();
                }
            }
            catch { }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            initData(sourceAnak);
        }
        
        //Back to home
        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        }

        //Donasi
        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Donationpop.SlideNavBarOpen.Begin();
            marginLeft = 0;
            Donationpop.Margin = new Thickness(0, 0, 0, 0);
            isOpenNavDraw = true;
            Donationpop.white_wall.Visibility = Visibility.Visible;
        }

    }
}