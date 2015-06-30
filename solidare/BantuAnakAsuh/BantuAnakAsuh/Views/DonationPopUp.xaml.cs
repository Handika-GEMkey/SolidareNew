using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BantuAnakAsuh.Helper;
using BantuAnakAsuh.Views;
using System.Windows.Media.Imaging;
using BantuAnakAsuh.ViewModels;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace BantuAnakAsuh.Views
{
    public partial class DonationPopUp 
    {
       
        public DonationPopUp()
        {
            InitializeComponent();
            NavDrawMargin.left = 0;
            

        }

        private void GestureListener_DragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            if (NavDrawMargin.left <= 400)
            {
                NavDrawMargin.left += e.HorizontalChange;
                LayoutRoot.Margin = new Thickness(0, NavDrawMargin.left, 0, 0);
            }
        }

        private void GestureListener_DragCompleted(object sender, DragCompletedGestureEventArgs e)
        {

            if (LayoutRoot.Margin.Bottom >= 200)
            {
                NavDrawMargin.left = 0;
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    while (LayoutRoot.Margin.Left < 400)
                    {
                        NavDrawMargin.left = LayoutRoot.Margin.Bottom + 1;
                        LayoutRoot.Margin = new Thickness(0, NavDrawMargin.left, 0, 0);
                    }
                });


            }
            else
            {
                NavDrawMargin.left = 0;
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    while (LayoutRoot.Margin.Bottom > 0)
                    {
                        NavDrawMargin.left = LayoutRoot.Margin.Bottom - 1;
                        LayoutRoot.Margin = new Thickness(0, NavDrawMargin.left, 0, 0);
                    }
                });

            }

        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            white_wall.Visibility = Visibility.Collapsed;
            ViewModelProgram viewmodelProgram = new ViewModelProgram();
            this.DataContext = viewmodelProgram;
            Donatebtn.Visibility = Visibility.Visible;
            
        }

        private void Grid_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ViewModelKeranjang keranjang = new ViewModelKeranjang();
            this.DataContext = keranjang;
            Donatebtn.Visibility = Visibility.Collapsed;
            

        }

        private void Grid_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            SlideNavBarClose.Begin();
            Donatebtn.Visibility = Visibility.Collapsed;
            
        }

            
        
        
    }
}