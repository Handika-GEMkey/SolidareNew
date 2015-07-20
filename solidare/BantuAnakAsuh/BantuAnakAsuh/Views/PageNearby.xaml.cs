﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Device.Location;
using Microsoft.Phone.Maps;
using Windows.Devices.Geolocation;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Maps.Controls;
using System.IO.IsolatedStorage;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using BantuAnakAsuh.Models;
using BantuAnakAsuh.Helper;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Input;
using BantuAnakAsuh.Common;
using RestSharp;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BantuAnakAsuh.Views
{
    public partial class PageNearby : PhoneApplicationPage
    {
        private IsolatedStorageFile Settings = IsolatedStorageFile.GetUserStoreForApplication();
        public LocationList LocationListobj = new LocationList();
        public List<GeoCoordinate> MyCoordinates = new List<GeoCoordinate>();

        public PageNearby()
        {
            InitializeComponent();
            this.Loaded += MapView_Loaded;
            Navigation.From_Page = "Nearby";
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        }


        private void apbarHome_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        }

        private void apbarMylocat_Click(object sender, EventArgs e)
        {

        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            ShowMyLocationOnTheMap();
            LoadURLDetailAnakAsuh();
            
        }

        private async void ShowMyLocationOnTheMap()
        {
            // Get my current location.
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate =
                CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);

            // Make my current location the center of the Map.
            this.MapVieMode.Center = myGeoCoordinate;
            this.MapVieMode.ZoomLevel = 16;

            // Create a small circle to mark the current location.
            BitmapImage myImage = new BitmapImage(new Uri("/Assets/Icons/ic_25_mylocat.png", UriKind.RelativeOrAbsolute));
            var image = new Image();
            image.Width = 50;
            image.Height = 50;
            image.Opacity = 100;
            image.Stretch = Stretch.UniformToFill;
            image.Source = myImage;

            // Create a MapOverlay to contain the circle.
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = image;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = myGeoCoordinate;

            // Create a MapLayer to contain the MapOverlay.
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);

            // Add the MapLayer to the Map.
            MapVieMode.Layers.Add(myLocationLayer);
        }

        public async void LoadURLDetailAnakAsuh()
        {
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("id_donors", Navigation.navIdDonors),
                    new KeyValuePair<string, string>("token", Navigation.token) 
                });

                var myHttpClient = new HttpClient();
                var response = await myHttpClient.PostAsync("http://solidare.azurewebsites.net/admin/APIv2/fosterchildren/fosterchildren.php", formContent);

                var stringContent = await response.Content.ReadAsStringAsync();

                LoadAnak(stringContent);
                myHttpClient.Dispose();
            }
            catch { }
        }

        public void LoadAnak(String responResult)
        {
            try
            {

                JObject jRoot = JObject.Parse(responResult);
                String result = jRoot.SelectToken("result").ToString();
                JArray JItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                foreach (JObject item in JItem)
                {
                    
                    LocationDetail lokasiDetail = new LocationDetail();
                    lokasiDetail.id_fosterchildren = item["id_fosterchildren"].ToString();
                    lokasiDetail.name = item["name"].ToString();
                    //lokasiDetail.pob = item["pob"].ToString();
                    //lokasiDetail.dob = item["dob"].ToString();
                    //lokasiDetail.gender = item["gender"].ToString();
                    lokasiDetail.address = item["address"].ToString();
                    lokasiDetail.photo = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item["photo"].ToString();
                    //lokasiDetail.cost = item["cost"].ToString();
                    //lokasiDetail.children_status = item["children_status"].ToString();
                    lokasiDetail.latitude = item["latitude"].ToString();
                    lokasiDetail.longitude = item["longitude"].ToString();
                    //lokasiDetail.study_level = item["study_level"].ToString();
                    //lokasiDetail.school = item["school"].ToString();
                    //lokasiDetail.grade = item["grade"].ToString();
                    //lokasiDetail.parent_name = item["parent_name"].ToString();
                    //lokasiDetail.parent_address = item["parent_address"].ToString();
                    //lokasiDetail.jobs = item["jobs"].ToString();
                    //lokasiDetail.salary = item["salary"].ToString();
                    //lokasiDetail.id_cha_org = item["id_cha_org"].ToString();
                    //lokasiDetail.cha_org_name = item["cha_org_name"].ToString();
                    //lokasiDetail.id_program = item["id_program"].ToString();
                    //lokasiDetail.program_name = item["program_name"].ToString();

                    LocationListobj.Add(lokasiDetail);
                }

                /************Add diff locations to list**************/
                MapVieMode.Layers.Clear();
                //MapLayer mapLayer = new MapLayer();
                MyCoordinates.Clear();
                for (int i = 0; i < LocationListobj.Count; i++)
                {
                    MyCoordinates.Add(new GeoCoordinate { Latitude = Double.Parse(LocationListobj[i].latitude), Longitude = Double.Parse(LocationListobj[i].longitude) });
                    
                }
                DrawMapMarkers();
                MapVieMode.Center = MyCoordinates[MyCoordinates.Count - 1];
                Dispatcher.BeginInvoke(() =>
                {
                    MapVieMode.SetView(LocationRectangle.CreateBoundingRectangle(MyCoordinates));
                });
                MapVieMode.SetView(MyCoordinates[MyCoordinates.Count - 1], 10, MapAnimationKind.Linear);
            }
            catch
            {
                MessageBox.Show("An error occured when load data.");
            }
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            MapsSettings.ApplicationContext.ApplicationId = "b185e4b1-725e-4163-b146-92cda6056391";
            MapsSettings.ApplicationContext.AuthenticationToken = "gcDNkfNOsMd67UT4yw7ngA";
        }

        private void DrawMapMarkers()
        {
            MapLayer mapLayer = new MapLayer();
            for (int i = 0; i < MyCoordinates.Count; i++)
            {
                UCCustomToolTip _tooltip = new UCCustomToolTip();
                _tooltip.Description = LocationListobj[i].name.ToString() + "\n" + LocationListobj[i].address+"\n"+LocationListobj[i].cha_org_name;
                _tooltip.DataContext = LocationListobj[i];
                _tooltip.Menuitem.Click += Menuitem_Click;
                _tooltip.imgmarker.Tap += _tooltip_Tapimg;
                MapOverlay overlay = new MapOverlay();
                overlay.Content = _tooltip;
                overlay.GeoCoordinate = MyCoordinates[i];
                overlay.PositionOrigin = new Point(0.0, 1.0);
                mapLayer.Add(overlay);
            }
            MapVieMode.Layers.Add(mapLayer);
        }

        public ICommand SetDonasi
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
            ModelDonasi SelectedItem = obj as ModelDonasi;

            if (SelectedItem != null)
                Navigation.navIdAnak = SelectedItem.id_anak_asuh;

            //listIndex = -1;
        }

        private void Menuitem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem item = (MenuItem)sender;
                string selecteditem = item.Tag.ToString();
                Navigation.menuItem = selecteditem;
                NavigationService.Navigate(new Uri("/Views/DetailAnakAsuhNearby.xaml?anak=" + selecteditem, UriKind.Relative));

            }
            catch
            {
            }
        }

        private void _tooltip_Tapimg(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                Image item = (Image)sender;
                string selecteditem = item.Tag.ToString();
                var selectedparkdata = LocationListobj.Where(s => s.id_fosterchildren == selecteditem).ToList();

                if (selectedparkdata.Count > 0)
                {
                    foreach (var items in selectedparkdata)
                    {
                        ContextMenu contextMenu =
                    ContextMenuService.GetContextMenu(item);
                        contextMenu.DataContext = items;
                        if (contextMenu.Parent == null)
                        {
                            contextMenu.IsOpen = true;
                        }
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        public class LocationDetail
        {
            public String id_fosterchildren { get; set; } //for list anak asuh donatur
            public String name { get; set; }
            public String pob { get; set; } //for list donatur
            public String dob { get; set; } //for list anak asuh donatur
            public String gender { get; set; }
            public String address { get; set; }
            public String photo { get; set; }
            public String cost { get; set; }
            public String children_status { get; set; }
            public String latitude { get; set; }
            public String longitude { get; set; }
            public String study_level { get; set; } //for list anak asuh donatur
            public String school { get; set; }
            public String grade { get; set; }
            public String parent_name { get; set; } //for list anak asuh donatur
            public String parent_address { get; set; }
            public String jobs { get; set; }
            public String salary { get; set; }
            public String id_cha_org { get; set; }
            public String cha_org_name { get; set; }
            public String id_program { get; set; }
            public String program_name { get; set; }

        }
        public class LocationList : List<LocationDetail>
        {
        }
        
        public static class CoordinateConverter
        {
            public static GeoCoordinate ConvertGeocoordinate(Geocoordinate geocoordinate)
            {
                return new GeoCoordinate
                    (
                    geocoordinate.Latitude,
                    geocoordinate.Longitude,
                    geocoordinate.Altitude ?? Double.NaN,
                    geocoordinate.Accuracy,
                    geocoordinate.AltitudeAccuracy ?? Double.NaN,
                    geocoordinate.Speed ?? Double.NaN,
                    geocoordinate.Heading ?? Double.NaN
                    );
            }
        }
    }
}