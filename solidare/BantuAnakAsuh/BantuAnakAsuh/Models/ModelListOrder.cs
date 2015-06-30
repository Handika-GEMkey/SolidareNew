using BantuAnakAsuh.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelListOrder : ViewModelBase
    {
        private String id_order;

        public String Id_order
        {
            get { return id_order; }
            set { id_order = value; RaisePropertyChanged(""); }
        }
        private String tgl_order;

        public String Tgl_order
        {
            get { return tgl_order; }
            set { tgl_order = value; RaisePropertyChanged(""); }
        }
        private String status_order;

        public String Status_order
        {
            get { return status_order; }
            set { status_order = value; RaisePropertyChanged(""); }
        }
        private String biaya_donasi;

        public String Biaya_donasi
        {
            get { return biaya_donasi; }
            set { biaya_donasi = value; RaisePropertyChanged(""); }
        }
        private String nama_anak_asuh;

        public String Nama_anak_asuh
        {
            get { return nama_anak_asuh; }
            set { nama_anak_asuh = value; RaisePropertyChanged(""); }
        }
        private String nama_cha_org;

        public String Nama_cha_org
        {
            get { return nama_cha_org; }
            set { nama_cha_org = value; RaisePropertyChanged(""); }
        }
        private String bank;

        public String Bank
        {
            get { return bank; }
            set { bank = value; RaisePropertyChanged(""); }
        }
        private String no_rek;

        public String No_rek
        {
            get { return no_rek; }
            set { no_rek = value; RaisePropertyChanged(""); }
        }
    }
}
