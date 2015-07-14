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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media;
using BantuAnakAsuh.Views;
using BantuAnakAsuh.Helper;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;
namespace BantuAnakAsuh.Views
{
    public partial class NewHomepage : PhoneApplicationPage
    {
       

        public NewHomepage()
        {
            InitializeComponent();
            //ViewModelProfileDonatur vmn = new ViewModelProfileDonatur();
            //this.DataContext = vmn;
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PivotItem selecteditem = (PivotItem)e.AddedItems[0];
            string pivotTag = (string)selecteditem.Tag;

            if (pivotTag.Equals("satu"))
            {
                NewsBorder.Visibility = Visibility.Visible;

                
                DonateBorder.Visibility = Visibility.Collapsed;
                DataContext = new ViewModelProfileDonatur();
                //ApplicationBar.Buttons.RemoveAt(0);
                //ApplicationBar.Buttons.RemoveAt(0);
                //idJenjang = "1";
            }
            if (pivotTag.Equals("dua"))
            {
                NewsBorder.Visibility = Visibility.Collapsed;
                
                DonateBorder.Visibility = Visibility.Visible;
                DataContext = new ViewModelDonasi();

                //ApplicationBarIconButton a = new ApplicationBarIconButton();
                //a.Text = "filter";
                //a.IconUri = new Uri("/Assets/Icons/ic_32_filter.png", UriKind.Relative);
                //ApplicationBar.Buttons.Add(a);

                //ApplicationBarIconButton b = new ApplicationBarIconButton();
                //b.Text = "nearby";
                //b.IconUri = new Uri("/Assets/Icons/ic_48_nearby.png", UriKind.Relative);
                //ApplicationBar.Buttons.Add(b);
            }
        }

        private void ListNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void stackProfile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageProfileDonatur.xaml", UriKind.Relative));
        }
        private void Children_onTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageProfileAnak.xaml", UriKind.Relative));
        }

        // for searching foster children
        
        private void Menu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Grid tapping = (Grid)sender;

            if (tapping.Name.Equals("NewsTap"))
            {
                pivotControl.SelectedIndex = 0;

            }
            else if (tapping.Name.Equals("DonateTap"))
            {
                pivotControl.SelectedIndex = 1;
            }
        }

        private void FitlerBtn_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageFilter.xaml", UriKind.Relative));
            //Donationpop.SlideNavBarClose.Begin();
        }

        private void MapBtn_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageNearby.xaml", UriKind.Relative));
        }

        private void AboutBtn_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageTentang.xaml", UriKind.Relative));
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream rawStream = isf.CreateFile("id_donors"))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(string.Empty);
                    writer.Close();
                }

                using (IsolatedStorageFileStream rawStream = isf.CreateFile("fullname"))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(string.Empty);
                    writer.Close();
                }

                using (IsolatedStorageFileStream rawStream = isf.CreateFile("token"))
                {
                    StreamWriter writer = new StreamWriter(rawStream);
                    writer.Write(string.Empty);
                    writer.Close();
                }
            }
            NavigationService.Navigate(new Uri("/Views/PageStart.xaml", UriKind.Relative));
        }

        private void timeLogo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageKeranjangDonasi.xaml", UriKind.Relative));
        }

        private void GestureListener_DragDeltaInput(object sender, DragDeltaGestureEventArgs e)
        {
            //if (isOpenNavDraw)
            //{
            //    Donationpop.Margin = new Thickness(0, 0, 0, 0);
            //    isOpenNavDraw = false;
            //}

            //double temp = marginLeft + e.VerticalChange;
            //if (temp <= 0 && temp >= -800)
            //{
            //    marginLeft += e.VerticalChange;
            //    Donationpop.Margin = new Thickness(0, marginLeft, 0, 0);
            //}
        }

        private void GestureListener_DragCompletedInput(object sender, DragCompletedGestureEventArgs e)
        {
            //if (marginLeft >= -400)
            //{
            //    Deployment.Current.Dispatcher.BeginInvoke(delegate
            //    {
            //        while (marginLeft < 0)
            //        {
            //            marginLeft++;

            //            Donationpop.Margin = new Thickness(0, marginLeft, 0, 0);
            //        }
            //    });
            //}
            //else
            //{
            //    Deployment.Current.Dispatcher.BeginInvoke(delegate
            //    {
            //        while (marginLeft > -800)
            //        {
            //            marginLeft--;

            //            Donationpop.Margin = new Thickness(0, marginLeft, 0, 0);
            //        }
            //    });
            //}
        }

        public void cancel_btn()
        {
           
        }

        private void RecommendationBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void recommendationLogo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageRekomendasi.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Terminate();
            //this.NavigationService.RemoveBackEntry();
            //base.OnBackKeyPress(e);
            //e.Cancel = true;

            //string caption = "Exit?";
            //string message = "Are you sure you want to exit?";
            //e.Cancel = MessageBoxResult.Cancel == MessageBox.Show(message, caption, MessageBoxButton.OKCancel);
            //if (e.Cancel != true)
            //{
                
            //}

        }

    }
}