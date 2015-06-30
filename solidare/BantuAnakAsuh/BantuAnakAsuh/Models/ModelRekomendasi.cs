using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelRekomendasi
    {
        private String id_rekomendasi;

        public String Id_rekomendasi
        {
            get { return id_rekomendasi; }
            set { id_rekomendasi = value; }
        }
        private String nama;

        public String Nama
        {
            get { return nama; }
            set { nama = value; }
        }
        private String id_orangtua_asli;

        public String Id_orangtua_asli
        {
            get { return id_orangtua_asli; }
            set { id_orangtua_asli = value; }
        }
        private String status;

        public String Status
        {
            get { return status; }
            set { status = value; }
        }
        private String foto;

        public String Foto
        {
            get { return foto; }
            set { foto = value; }
        }
        private String tempat_lahir;

        public String Tempat_lahir
        {
            get { return tempat_lahir; }
            set { tempat_lahir = value; }
        }
        private String jenis_kelamin;

        public String Jenis_kelamin
        {
            get { return jenis_kelamin; }
            set { jenis_kelamin = value; }
        }
        private String anak_ke;

        public String Anak_ke
        {
            get { return anak_ke; }
            set { anak_ke = value; }
        }
        private String alamat;

        public String Alamat
        {
            get { return alamat; }
            set { alamat = value; }
        }
        private String latitude;

        public String Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        private String longitude;

        public String Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        private String status_rekomendasi;

        public String Status_rekomendasi
        {
            get { return status_rekomendasi; }
            set { status_rekomendasi = value; }
        }
    }
}
