using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BantuAnakAsuh.Common;
using System.Windows.Input;
using RestSharp;
using BantuAnakAsuh.Helper;
using Microsoft.Phone.Shell;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Data.Linq.Mapping;
using BantuAnakAsuh.Views;

namespace BantuAnakAsuh.Models
{
    class ModelRekomendasiLingkungan : ViewModelBase
    {

        private BitmapImage fotoDepanRumah;

        public BitmapImage FotoDepanRumah
        {
            get { return fotoDepanRumah; }
            set { fotoDepanRumah = value; RaisePropertyChanged(""); }
        }

        private BitmapImage fotoDalamRumah;

        public BitmapImage FotoDalamRumah
        {
            get { return fotoDalamRumah; }
            set { fotoDalamRumah = value; RaisePropertyChanged(""); }
        }

        private BitmapImage fotoLingkunganSatu;

        public BitmapImage FotoLingkunganSatu
        {
            get { return fotoLingkunganSatu; }
            set { fotoLingkunganSatu = value; RaisePropertyChanged(""); }
        }
        private BitmapImage fotoLingkunganDua;

        public BitmapImage FotoLingkunganDua
        {
            get { return fotoLingkunganDua; }
            set { fotoLingkunganDua = value; RaisePropertyChanged(""); }
        }
    }
}
