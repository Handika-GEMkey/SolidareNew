using System;
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

namespace BantuAnakAsuh.Views
{
    public partial class PageNearby : PhoneApplicationPage
    {
        public PageNearby()
        {
            InitializeComponent();
            this.Loaded += MapView_Loaded;
            Navigation.From_Page = "Nearby";
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
        }


        private void apbarHome_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageDonasi.xaml", UriKind.Relative));
        }

        private void apbarMylocat_Click(object sender, EventArgs e)
        {

            
            
        }
        

        GeoCoordinateWatcher watcher;



        //- code map

        private IsolatedStorageFile Settings = IsolatedStorageFile.GetUserStoreForApplication();
        private LocationList LocationListobj = new LocationList();
        public List<GeoCoordinate> MyCoordinates = new List<GeoCoordinate>();




        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            LoadURLDetailAnakAsuh();
            ShowMyLocationOnTheMap();
        }


        public async void LoadURLDetailAnakAsuh()
        {
            HttpClient client = new HttpClient();
            try
            {
                string responResult = await client.GetStringAsync(new Uri("http://alix.azurewebsites.net/admin/api/anakasuh/anakasuh.php?nocache=" + Guid.NewGuid()));
                LoadKejahatan(responResult);
                client.Dispose();
            }
            catch { }
        }

        private void LoadKejahatan(String responResult)
        {
            try
            {
                /************Binding dari Database**************/
                JObject jRoot = JObject.Parse(responResult);
                JArray jItem = JArray.Parse(jRoot.SelectToken("item").ToString());
                foreach (JObject item in jItem)
                {
                    LocationDetail lokasidetail = new LocationDetail();
                    lokasidetail.id_anak_asuh = item.SelectToken("id_anak_asuh").ToString();
                    lokasidetail.nama_anak_asuh = item.SelectToken("nama_anak_asuh").ToString();
                    lokasidetail.foto_anak = URL.BASE3 + "modul/mod_AnakAsuh/photo/" + item.SelectToken("foto_anak").ToString();
                    lokasidetail.latitude = item.SelectToken("latitude").ToString();
                    lokasidetail.longitude = item.SelectToken("longitude").ToString();
                    lokasidetail.alamat = item.SelectToken("alamat").ToString();
                    LocationListobj.Add(lokasidetail);
                }
                /************Add diff locations to list**************/

                MapVieMode.Layers.Clear();
                MapLayer mapLayer = new MapLayer();
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
            }
        }


        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            MapsSettings.ApplicationContext.ApplicationId = "<applicationid>";
            MapsSettings.ApplicationContext.AuthenticationToken = "<authenticationtoken>";
        }
        private void DrawMapMarkers()
        {
            MapLayer mapLayer = new MapLayer();
            for (int i = 0; i < MyCoordinates.Count; i++)
            {
                UCCustomToolTip _tooltip = new UCCustomToolTip();
                _tooltip.Description = LocationListobj[i].nama_anak_asuh.ToString() + "\n" + LocationListobj[i].alamat;
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
        //public ICommand SetDonasi
        //{
        //    get
        //    {
        //        return new DelegateCommand(SetIdAnak, CanSetIdAnak);
        //    }
        //}

        //private bool CanSetIdAnak(object arg)
        //{
        //    return true;
        //}

        //private void SetIdAnak(object obj)
        //{
        //    ModelDonasi SelectedItem = obj as ModelDonasi;
            
        //    if (SelectedItem != null)
        //        Navigation.navIdAnak = SelectedItem.id_anak_asuh;

        //    //listIndex = -1;
        //}


        private void Menuitem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem item = (MenuItem)sender;
                string selecteditem = item.Tag.ToString();
                //Navigation.menuItem = selecteditem;
                
                var selectedparkdata = LocationListobj.Where(s => s.id_anak_asuh == selecteditem).ToList();
                if (selectedparkdata.Count > 0)
                {
                    foreach (var items in selectedparkdata)
                    {
                        if (Settings.FileExists("LocationDetailItem"))
                        {
                            Settings.DeleteFile("LocationDetailItem");
                        }
                        using (IsolatedStorageFileStream fileStream = Settings.OpenFile("LocationDetailItem", FileMode.Create))
                        {
                            DataContractSerializer serializer = new DataContractSerializer(typeof(LocationDetail));
                            serializer.WriteObject(fileStream, items);
                        }
                        
                        break;
                    }
                }
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
                var selectedparkdata = LocationListobj.Where(s => s.id_anak_asuh == selecteditem).ToList();

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
            public string id_anak_asuh { get; set; }
            public string nama_anak_asuh { get; set; }
            public string foto_anak { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string alamat { get; set; }
            
        }
        public class LocationList : List<LocationDetail>
        {
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
        //08994452514
        //Alamat atau latitude longitude  -6.934801, 107.629852
        //Email farni.nur.amalia@gmail.com



    }
}