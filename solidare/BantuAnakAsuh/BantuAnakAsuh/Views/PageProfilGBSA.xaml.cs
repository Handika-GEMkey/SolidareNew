using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace BantuAnakAsuh.Views
{
    public partial class PageProfilGBSA : PhoneApplicationPage
    {
        public PageProfilGBSA()
        {
            InitializeComponent();
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PivotItem selectedItem = (PivotItem)e.AddedItems[0];
            String pivotTag = (String)selectedItem.Tag;
            SolidColorBrush Brush2 = new SolidColorBrush();
            Brush2.Color = Colors.DarkGray;
            SolidColorBrush Brush3 = new SolidColorBrush();

            if (pivotTag.Equals("Profile"))
            {
                tbProfile.Foreground = GetColorFromHexa("#FF062D5A");
                tbCaraKerja.Foreground = Brush2;
                tbVisiMisi.Foreground = Brush2;
            }
            else if (pivotTag.Equals("Cara Kerja"))
            {
                tbCaraKerja.Foreground = GetColorFromHexa("#FF062D5A");
                tbProfile.Foreground = Brush2;
                tbVisiMisi.Foreground = Brush2;
            }
            else
            {
                tbVisiMisi.Foreground = GetColorFromHexa("#FF062D5A");
                tbCaraKerja.Foreground = Brush2;
                tbProfile.Foreground = Brush2;
            }
        }

        private void Tap_selectionChangedPivot(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Grid grid = (Grid)sender;

            if (grid.Name.Equals("tapProfile"))
            {
                pivotControl.SelectedIndex = 0;
            }
            else if (grid.Name.Equals("tapCaraKerja"))
            {
                pivotControl.SelectedIndex = 1;
            }
            else
            {
                pivotControl.SelectedIndex = 2;                
            }
        }

        public static SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            return new SolidColorBrush(
                Color.FromArgb(
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16),
                    Convert.ToByte(hexaColor.Substring(7, 2), 16)
                )
            );
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
        }
    }
}