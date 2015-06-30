using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls.Maps;
using System.Windows.Media;
using System.Device.Location;
using System.Windows.Shapes;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;


namespace BantuAnakAsuh.Views
{
    public partial class PageKonsultasi : PhoneApplicationPage
    {
        public PageKonsultasi()
        {
            InitializeComponent();
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageMenu.xaml", UriKind.Relative));
            
        }

        private void buttonGetdirection_Click(object sender, EventArgs e)
        {
            Microsoft.Phone.Tasks.MapsDirectionsTask mdt = new MapsDirectionsTask();

            LabeledMapLocation LMLD = new LabeledMapLocation();
            LMLD.Label = "Rumah Zakat";
            LMLD.Location = new System.Device.Location.GeoCoordinate(-6.943214, 107.630858);
            mdt.End = LMLD;

            mdt.Show(); 
        }

        private void buttonPhone_Click(object sender, EventArgs e)
        {
            PhoneCallTask phoneCallTask = new PhoneCallTask();

            phoneCallTask.PhoneNumber = "0227332407";
            phoneCallTask.DisplayName = "Rumah Zakat";

            phoneCallTask.Show();
        }

        private void buttonMail_Click(object sender, EventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "Konsultasi Rumah Zakat";
            emailComposeTask.Body = "Silahkan isi";
            emailComposeTask.To = "welcome@rumahzakat.org";

            emailComposeTask.Show();
        }

        private void buttonFacebook_Click(object sender, EventArgs e)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask();

            webBrowserTask.Uri = new Uri("https://www.facebook.com/rumahzakatfans", UriKind.Absolute);

            webBrowserTask.Show();
        }


    }
}