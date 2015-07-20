using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BantuAnakAsuh.ViewModels;
using BantuAnakAsuh.Models;
using System.Text;
using BantuAnakAsuh.Helper;
using Microsoft.Phone.Maps.Services;
using System.Device.Location;
using System.Diagnostics;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json.Linq;
using Windows.Devices.Geolocation;
using BantuAnakAsuh.Resources;

namespace BantuAnakAsuh.Views
{
    public partial class PageRekomendasi : PhoneApplicationPage
    {

        public string Latitude, Longitude = "";
        Microsoft.Phone.Maps.Controls.MapLayer markerLayer = null;
        GeocodeQuery geoQ = null;
        IList<MapLocation> resList = null;

        DateTime? date;
        //String[] listStatusArray = { "Choose Status", "Orphan", "Orphans", "Poor children" };

        public PageRekomendasi()
        {
            InitializeComponent();
            this.DataContext = new ViewModelRekomendasi();
            //this.listStatus.ItemsSource = listStatusArray;

            pivot2.Visibility = Visibility.Collapsed;
            pivot1.Visibility = Visibility.Visible;

            //geoQ = new GeocodeQuery();
            //geoQ.QueryCompleted += geoQ_QueryCompleted;
            //Debug.WriteLine("All construction done for GeoCoding");
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
        }
        
        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PivotItem selectedItem = (PivotItem)e.AddedItems[0];
            String pivotTag = (String)selectedItem.Tag;
            if (pivotTag.Equals("satu"))
            {
                NewsBorder.Visibility = Visibility.Visible;

                DonateBorder.Visibility = Visibility.Collapsed;
                //elip1.Visibility = Visibility.Visible;
                //elip2.Visibility = Visibility.Collapsed;
                //elip3.Visibility = Visibility.Collapsed;
                //elip1_Copy.Visibility = Visibility.Collapsed;
                //elip2_Copy.Visibility = Visibility.Visible;
                //elip3_Copy.Visibility = Visibility.Visible;
            }
            else if (pivotTag.Equals("dua"))
            {
                NewsBorder.Visibility = Visibility.Collapsed;

                DonateBorder.Visibility = Visibility.Visible;
                //elip1.Visibility = Visibility.Collapsed;
                //elip2.Visibility = Visibility.Visible;
                //elip3.Visibility = Visibility.Collapsed;
                //elip1_Copy.Visibility = Visibility.Visible;
                //elip2_Copy.Visibility = Visibility.Collapsed;
                //elip3_Copy.Visibility = Visibility.Visible;
            }
            //else if (pivotTag.Equals("tiga"))
            //{
            //    elip1.Visibility = Visibility.Collapsed;
            //    elip2.Visibility = Visibility.Collapsed;
            //    elip3.Visibility = Visibility.Visible;
            //    elip1_Copy.Visibility = Visibility.Visible;
            //    elip2_Copy.Visibility = Visibility.Visible;
            //    elip3_Copy.Visibility = Visibility.Collapsed;
            //}

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.OK == MessageBox.Show("Waiting for uploaded"))
            {
                pivot2.Visibility = Visibility.Visible;
                pivot1.Visibility = Visibility.Collapsed;
                Pivot_Control.SelectedIndex = 1;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.OK == MessageBox.Show("Waiting for uploaded"))
            {
                //pivot3.Visibility = Visibility.Visible;
                pivot2.Visibility = Visibility.Collapsed;
                //Pivot_Control.SelectedIndex = 2;
            }
        }

        private void TapClick(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
            btn_frontyard.Visibility = Visibility.Collapsed;
            background_frontyrd.Visibility = Visibility.Collapsed;

            addimage4.Visibility = Visibility.Visible;
            addimage3.Visibility = Visibility.Visible;

            addimage5.Visibility = Visibility.Visible;
            addimage6.Visibility = Visibility.Collapsed;

            
        }

        private void imageonTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            addimage1.Visibility = Visibility.Visible;
            addimage2.Visibility = Visibility.Collapsed;

            addimage3.Visibility = Visibility.Visible;
            addimage4.Visibility = Visibility.Collapsed;

            addimage5.Visibility = Visibility.Visible;
            addimage6.Visibility = Visibility.Collapsed;
        }

        private void btn_address_Click(object sender, RoutedEventArgs e)
        {
            GetPosition();
            //if (markerLayer != null)
            //{
            //    map1.Layers.Remove(markerLayer);
            //    markerLayer = null;
            //}

            //markerLayer = new Microsoft.Phone.Maps.Controls.MapLayer();
            //map1.Layers.Add(markerLayer);

            //if (geoQ.IsBusy == true)
            //{
            //    geoQ.CancelAsync();
            //}
            //// Set the full address query

            //GeoCoordinate setMe = new GeoCoordinate(map1.Center.Latitude, map1.Center.Longitude);
            //setMe.HorizontalAccuracy = 1000000;

            //geoQ.GeoCoordinate = setMe;
            //geoQ.SearchTerm = alamat_anakasuh.Text;

            //Latitude = geoQ.GeoCoordinate.Latitude.ToString();
            //Longitude = geoQ.GeoCoordinate.Longitude.ToString();

            ////vrekomend.Latitude = this.Latitude;
            ////vrekomend.Longitude = this.Longitude;

            //Navigation.Latitude = this.Latitude.ToString();
            //Navigation.Longitude = this.Longitude.ToString();

            //geoQ.MaxResultCount = 200;

            //geoQ.QueryAsync();
            //Debug.WriteLine("GeocodeAsync started for: " + alamat_anakasuh.Text);

        }

        public void GetPosition()
        {
            var latitude = 0d;
            var longitude = 0d;
            var locator = new Geolocator();
            var geocodequery = new GeocodeQuery();

            if (!locator.LocationStatus.Equals(PositionStatus.Disabled))
            {
                geocodequery.GeoCoordinate = new GeoCoordinate(0, 0);
                geocodequery.SearchTerm = alamat_anakasuh.Text;
                geocodequery.QueryAsync();

                geocodequery.QueryCompleted += (sender, args) =>
                {
                    if (!args.Result.Equals(null))
                    {
                        var result = args.Result.FirstOrDefault();

                        latitude = result.GeoCoordinate.Latitude;
                        longitude = result.GeoCoordinate.Longitude;

                        Navigation.Latitude = latitude.ToString();
                        Navigation.Longitude = longitude.ToString();

                        //                        mapLocation.Center = new GeoCoordinate(latitude, longitude);
                        map1.SetView(result.BoundingBox, Microsoft.Phone.Maps.Controls.MapAnimationKind.Parabolic);
                    }
                };

            }

            else
            {
                MessageBox.Show("Service Geolocation not enabled!", AppResources.ApplicationTitle, MessageBoxButton.OK);
                return;
            }
        }

        #region code lama map
        //void geoQ_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        //{
        //    // The result is a GeocodeResponse object
        //    resList = e.Result;

        //    Debug.WriteLine("Geo query, error: " + e.Error);
        //    Debug.WriteLine("Geo query, cancelled: " + e.Cancelled);
        //    Debug.WriteLine("Geo query, cancelled: " + e.UserState.ToString());
        //    Debug.WriteLine("Geo query, Result.Count(): " + resList.Count());


        //    if (resList.Count() > 0)
        //    {
        //        for (int i = 0; i < resList.Count(); i++)
        //        {
        //            Debug.WriteLine("Result no.: " + i);

        //            Debug.WriteLine("Name: " + resList[i].Information.Name);
        //            Debug.WriteLine("Address.ToString: " + resList[i].Information.Address.ToString());
        //            Debug.WriteLine("Address.District: " + resList[i].Information.Address.District);
        //            Debug.WriteLine("Address.Country: " + resList[i].Information.Address.CountryCode + ": " + resList[i].Information.Address.Country);
        //            Debug.WriteLine("Address.County: " + resList[i].Information.Address.County);
        //            Debug.WriteLine("Address.Neighborhood: " + resList[i].Information.Address.Neighborhood);
        //            Debug.WriteLine("Address.Street: " + resList[i].Information.Address.Street);
        //            Debug.WriteLine("Address.PostalCode: " + resList[i].Information.Address.PostalCode);
        //            Debug.WriteLine("Address.Continent: " + resList[i].Information.Address.Continent);

        //            Debug.WriteLine("GeoCoordinate.Latitude: " + resList[i].GeoCoordinate.Latitude.ToString());
        //            Debug.WriteLine("GeoCoordinate.Longitude: " + resList[i].GeoCoordinate.Longitude.ToString());

        //            string numNum = "0" + i;
        //            if (i > 9)
        //            {
        //                numNum = "" + i;
        //            }

        //            AddResultToMap(numNum, resList[i].GeoCoordinate);
        //        }
        //    }

        //    if ((markerLayer != null)) // fit all
        //    {
        //        if (markerLayer.Count() == 1)
        //        {
        //            map1.Center = markerLayer[0].GeoCoordinate;
        //        }
        //        else
        //        {

        //            bool gotRect = false;
        //            double north = 0;
        //            double west = 0;
        //            double south = 0;
        //            double east = 0;

        //            for (var p = 0; p < markerLayer.Count(); p++)
        //            {
        //                if (!gotRect)
        //                {
        //                    gotRect = true;
        //                    north = south = markerLayer[p].GeoCoordinate.Latitude;
        //                    west = east = markerLayer[p].GeoCoordinate.Longitude;
        //                }
        //                else
        //                {
        //                    if (north < markerLayer[p].GeoCoordinate.Latitude) north = markerLayer[p].GeoCoordinate.Latitude;
        //                    if (west > markerLayer[p].GeoCoordinate.Longitude) west = markerLayer[p].GeoCoordinate.Longitude;
        //                    if (south > markerLayer[p].GeoCoordinate.Latitude) south = markerLayer[p].GeoCoordinate.Latitude;
        //                    if (east < markerLayer[p].GeoCoordinate.Longitude) east = markerLayer[p].GeoCoordinate.Longitude;
        //                }
        //            }

        //            if (gotRect)
        //            {
        //                map1.SetView(new LocationRectangle(north, west, south, east));
        //            }
        //        }
        //    }
        //}

        //private void AddResultToMap(String text, GeoCoordinate location)
        //{

        //    MapOverlay oneMarker = new MapOverlay();
        //    oneMarker.GeoCoordinate = location;

        //    Canvas canCan = new Canvas();

        //    Ellipse Circhegraphic = new Ellipse();
        //    Circhegraphic.Fill = new SolidColorBrush(Colors.Brown);
        //    Circhegraphic.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
        //    Circhegraphic.StrokeThickness = 5;
        //    Circhegraphic.Opacity = 0.8;
        //    Circhegraphic.Height = 40;
        //    Circhegraphic.Width = 40;

        //    canCan.Children.Add(Circhegraphic);
        //    TextBlock textt = new TextBlock { Text = text };
        //    textt.HorizontalAlignment = HorizontalAlignment.Center;
        //    Canvas.SetLeft(textt, 10);
        //    Canvas.SetTop(textt, 5);
        //    Canvas.SetZIndex(textt, 5);

        //    canCan.Children.Add(textt);
        //    oneMarker.Content = canCan;

        //    oneMarker.PositionOrigin = new Point(0.5, 0.5);
        //    textt.MouseLeftButtonUp += textt_MouseLeftButtonUp;

        //    markerLayer.Add(oneMarker);
        //}

        //void textt_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    Debug.WriteLine("textt_MouseLeftButtonUp");
        //    TextBlock textt = sender as TextBlock;
        //    if (textt != null && (resList != null))
        //    {
        //        int hint = int.Parse(textt.Text);

        //        if (hint >= 0 && hint < resList.Count())
        //        {

        //            string showString = resList[hint].Information.Name;
        //            showString = showString + "\nAddress: ";
        //            showString = showString + "\n" + resList[hint].Information.Address.HouseNumber + " " + resList[hint].Information.Address.Street;
        //            showString = showString + "\n" + resList[hint].Information.Address.PostalCode + " " + resList[hint].Information.Address.City;
        //            showString = showString + "\n" + resList[hint].Information.Address.Country + " " + resList[hint].Information.Address.CountryCode;
        //            showString = showString + "\nDescription: ";
        //            showString = showString + "\n" + resList[hint].Information.Description.ToString();

        //            MessageBox.Show(showString);
        //        }
        //    }

        //}
        #endregion

        private void Button_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            btn_inside.Visibility = Visibility.Collapsed;
            background_inside.Visibility = Visibility.Collapsed;

            addimage4.Visibility = Visibility.Collapsed;
            addimage3.Visibility = Visibility.Visible;

            addimage5.Visibility = Visibility.Visible;
            addimage6.Visibility = Visibility.Visible;
        }

        private void Button_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            addimage1.Visibility = Visibility.Collapsed;
            addimage2.Visibility = Visibility.Visible;
        }

        private void apbarRekomendasi_Click(object sender, EventArgs e)
        {
            var vm = (ViewModelRekomendasi)DataContext;
            vm.PublishCommand1.Execute(null);

            if (Navigation.menuItem == "pivot_environment")
            {
                if (MessageBoxResult.OK == MessageBox.Show("Waiting for uploaded"))
                {
                    ApplicationBar.Buttons.RemoveAt(0);

                    pivot2.Visibility = Visibility.Visible;
                    pivot1.Visibility = Visibility.Collapsed;
                    Pivot_Control.SelectedIndex = 1;

                    ApplicationBar = new ApplicationBar();
                    ApplicationBar.BackgroundColor = Color.FromArgb(100, 245, 245, 245);
                    ApplicationBar.ForegroundColor = Colors.Black;
                    ApplicationBar.Mode = ApplicationBarMode.Default;
                    ApplicationBar.Opacity = 1.0;
                    ApplicationBar.IsVisible = true;
                    ApplicationBar.IsMenuEnabled = true;

                    ApplicationBarIconButton btnSendEnvironment = new ApplicationBarIconButton();
                    btnSendEnvironment.IconUri = new Uri("/Assets/icons/ic_48_checklist.png", UriKind.Relative);
                    btnSendEnvironment.Text = "send";

                    ApplicationBar.Buttons.Add(btnSendEnvironment);
                    btnSendEnvironment.Click += new EventHandler(btnSendEnvironment_Click);
                }
                else if (Navigation.menuItem == "pivot_failed")
                {
                    pivot2.Visibility = Visibility.Collapsed;
                    pivot1.Visibility = Visibility.Visible;
                    Pivot_Control.SelectedIndex = 0;
                }
            }
        }

        private void btnSendEnvironment_Click(object sender, EventArgs e)
        {
            if (MessageBoxResult.OK == MessageBox.Show("Waiting for uploaded"))
            {
                var vm = (ViewModelRekomendasi)DataContext;
                vm.PublishCommand2.Execute(null);  
            }
        }
    }
}