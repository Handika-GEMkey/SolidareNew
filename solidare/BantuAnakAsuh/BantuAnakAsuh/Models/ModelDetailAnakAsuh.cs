using BantuAnakAsuh.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelDetailAnakAsuh : ViewModelBase
    {
        private String id_anak_asuh;

        public String Id_anak_asuh
        {
            get { return id_anak_asuh; }
            set { id_anak_asuh = value; RaisePropertyChanged(""); }
        }
        private String id_orangtua_asli;

        public String Id_orangtua_asli
        {
            get { return id_orangtua_asli; }
            set { id_orangtua_asli = value; RaisePropertyChanged(""); }
        }
        private String nama_anak_asuh;

        public String Nama_anak_asuh
        {
            get { return nama_anak_asuh; }
            set { nama_anak_asuh = value; RaisePropertyChanged(""); }
        }
        private String status_anak;

        public String Status_anak
        {
            get { return status_anak; }
            set { status_anak = value; RaisePropertyChanged(""); }
        }
        private String tempat_lahir;

        public String Tempat_lahir
        {
            get { return tempat_lahir; }
            set { tempat_lahir = value; RaisePropertyChanged(""); }
        }
        private String tanggal_lahir;

        public String Tanggal_lahir
        {
            get { return tanggal_lahir; }
            set { tanggal_lahir = value; RaisePropertyChanged(""); }
        }
        private String jk_anak_asuh;

        public String Jk_anak_asuh
        {
            get { return jk_anak_asuh; }
            set { jk_anak_asuh = value; RaisePropertyChanged(""); }
        }
        private String anak_ke;

        public String Anak_ke
        {
            get { return anak_ke; }
            set { anak_ke = value; RaisePropertyChanged(""); }
        }
        private String alamat;

        public String Alamat
        {
            get { return alamat; }
            set { alamat = value; RaisePropertyChanged(""); }
        }
        private String jenjang_pendidikan;

        public String Jenjang_pendidikan
        {
            get { return jenjang_pendidikan; }
            set { jenjang_pendidikan = value; RaisePropertyChanged(""); }
        }
        private String kelas;

        public String Kelas
        {
            get { return kelas; }
            set { kelas = value; RaisePropertyChanged(""); }
        }
        private String nama_sekolah;

        public String Nama_sekolah
        {
            get { return nama_sekolah; }
            set { nama_sekolah = value; RaisePropertyChanged(""); }
        }
        private String foto_anak;

        public String Foto_anak
        {
            get { return foto_anak; }
            set { foto_anak = value; RaisePropertyChanged(""); }
        }
        private String biaya_donasi;

        public String Biaya_donasi
        {
            get { return biaya_donasi; }
            set { biaya_donasi = value; RaisePropertyChanged(""); }
        }
        private String status_donasi;

        public String Status_donasi
        {
            get { return status_donasi; }
            set { status_donasi = value; RaisePropertyChanged(""); }
        }
        private String latitude;

        public String Latitude
        {
            get { return latitude; }
            set { latitude = value; RaisePropertyChanged(""); }
        }
        private String longitude;

        public String Longitude
        {
            get { return longitude; }
            set { longitude = value; RaisePropertyChanged(""); }
        }

        private String id_program;

        public String Id_pogram
        {
            get { return longitude; }
            set { longitude = value; RaisePropertyChanged(""); }
        }

        private String nama_orangtua_asli;

        public String Nama_orangtua_asli
        {
            get { return nama_orangtua_asli; }
            set { nama_orangtua_asli = value; RaisePropertyChanged(""); }
        }
        private String alamat_orangtua_asli;

        public String Alamat_orangtua_asli
        {
            get { return alamat_orangtua_asli; }
            set { alamat_orangtua_asli = value; RaisePropertyChanged(""); }
        }
        private String pekerjaan;

        public String Pekerjaan
        {
            get { return pekerjaan; }
            set { pekerjaan = value; RaisePropertyChanged(""); }
        }
        private String penghasilan;

        public String Penghasilan
        {
            get { return penghasilan; }
            set { penghasilan = value; RaisePropertyChanged(""); }
        }
        private String nama_org;
        public String Nama_Org
        {
            get { return nama_org; }
            set { nama_org = value; RaisePropertyChanged(""); }
        }

        private String bank;
        public String Bank
        {
            get { return bank; }
            set { bank = value; RaisePropertyChanged(""); }
        }

        private String no_rek;
        public String No_Rek
        {
            get { return no_rek; }
            set { no_rek = value; RaisePropertyChanged(""); }
        }
    }
}
