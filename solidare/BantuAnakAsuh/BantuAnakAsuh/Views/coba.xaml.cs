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
namespace BantuAnakAsuh.Views
{
    public partial class coba : PhoneApplicationPage
    {
        public coba()
        {
            InitializeComponent();
            ViewModelConfirmation vmc = new ViewModelConfirmation();
            this.DataContext = vmc;
        }
    }
}