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
using System.Text;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace BantuAnakAsuh.Views
{
    public partial class PageRekomendasiLingkungan : PhoneApplicationPage
    {
        
        public PageRekomendasiLingkungan()
        {
            InitializeComponent();
            ViewModelEnvironment vm = new ViewModelEnvironment();
            this.DataContext = vm;
        }

        

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
        }

    }
}