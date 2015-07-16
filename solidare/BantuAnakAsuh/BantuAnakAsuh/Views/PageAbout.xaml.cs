using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace BantuAnakAsuh.Views
{
    public partial class PageAbout : PhoneApplicationPage
    {
        public PageAbout()
        {
            InitializeComponent();
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
        }

        private void buttonFacebook_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            aboutUrl("https://www.facebook.com/pages/Gerakan-Bantu-Seribu-Anak-Asuh-GBSA/465261570197142");
        }

        private void buttonTwitter_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            aboutUrl("https://twitter.com/alixindonesia/");
        }

        private void buttonWeb_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            aboutUrl("http://alix.azurewebsites.net/");
        }

        private void aboutUrl(string url)
        {
            Microsoft.Phone.Tasks.WebBrowserTask wbt = new Microsoft.Phone.Tasks.WebBrowserTask();
            wbt.Uri = new Uri(url);
            wbt.Show();
        }

        private void buttonRZ_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            aboutUrl("https://www.rumahzakat.org/");
        }

        private void buttonBerma_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            aboutUrl("http://beruangmatahari.com/");
        }
    }
}