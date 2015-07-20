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
using BantuAnakAsuh.Models;
using BantuAnakAsuh.Helper;
using Newtonsoft.Json.Linq;
using Microsoft.Phone.Tasks;
using System.IO;
using System.Net.Http;

namespace BantuAnakAsuh.Views
{
    public partial class PageSetting : PhoneApplicationPage
    {
        public PageSetting()
        {
            InitializeComponent();
            ViewModelSetting vmSetting = new ViewModelSetting();
            this.DataContext = vmSetting;
            Navigation.From_Page = "PageSetting";
            
        }

        private void buttonMenu_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageDetailDonatur.xaml", UriKind.Relative));

        }

        private void buttonChangePassword_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PagePassword.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageDetailDonatur.xaml", UriKind.Relative));
        }

        private void saveChanges_Click(object sender, EventArgs e)
        {
            var vm = (ViewModelSetting)DataContext;
            vm.PublishCommand.Execute(null);
        }
    }
}