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

namespace BantuAnakAsuh.Views
{
    public partial class PageRekomendasi : PhoneApplicationPage
    {
        DateTime? date;
        //String[] listStatusArray = { "Choose Status", "Orphan", "Orphans", "Poor children" };

        public PageRekomendasi()
        {
            InitializeComponent();
            this.DataContext = new ViewModelRekomendasi();
            //this.listStatus.ItemsSource = listStatusArray;


            //string format = "yyyy-MM-dd";
            //string date1 = TglLahirPick.ValueString;
            //DateTime datevalue = DateTime.Parse(date1);
            //string d = datevalue.ToString(format);
            //MessageBox.Show(d);

            //ViewModelRekomendasi vm = new ViewModelRekomendasi();
            //vm.Tanggal_lahir = d;

            

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
            
           
        }
        
        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PivotItem selectedItem = (PivotItem)e.AddedItems[0];
            String pivotTag = (String)selectedItem.Tag;
            if (pivotTag.Equals("satu"))
            {
                elip1.Visibility = Visibility.Visible;
                elip2.Visibility = Visibility.Collapsed;
                elip3.Visibility = Visibility.Collapsed;
                elip1_Copy.Visibility = Visibility.Collapsed;
                elip2_Copy.Visibility = Visibility.Visible;
                elip3_Copy.Visibility = Visibility.Visible;
            }
            else if (pivotTag.Equals("dua"))
            {
                elip1.Visibility = Visibility.Collapsed;
                elip2.Visibility = Visibility.Visible;
                elip3.Visibility = Visibility.Collapsed;
                elip1_Copy.Visibility = Visibility.Visible;
                elip2_Copy.Visibility = Visibility.Collapsed;
                elip3_Copy.Visibility = Visibility.Visible;
            }
            else if (pivotTag.Equals("tiga"))
            {
                elip1.Visibility = Visibility.Collapsed;
                elip2.Visibility = Visibility.Collapsed;
                elip3.Visibility = Visibility.Visible;
                elip1_Copy.Visibility = Visibility.Visible;
                elip2_Copy.Visibility = Visibility.Visible;
                elip3_Copy.Visibility = Visibility.Collapsed;
            }

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
                pivot3.Visibility = Visibility.Visible;
                pivot2.Visibility = Visibility.Collapsed;
                Pivot_Control.SelectedIndex = 2;
            }
        }

        private void TapClick(object sender, System.Windows.Input.GestureEventArgs e)
        {
            addimage1.Visibility = Visibility.Collapsed;
            addimage2.Visibility = Visibility.Visible;


            addimage3.Visibility = Visibility.Collapsed;
            addimage4.Visibility = Visibility.Visible;

            addimage5.Visibility = Visibility.Collapsed;
            addimage6.Visibility = Visibility.Visible;
        }

        private void imageonTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            addimage1.Visibility = Visibility.Visible;
            addimage2.Visibility = Visibility.Collapsed;

            addimage3.Visibility = Visibility.Visible;
            addimage4.Visibility = Visibility.Collapsed;

            addimage5.Visibility = Visibility.Visible;
            addimage6.Visibility = Visibility.Collapsed;
        }

       
    }
}