using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelDonasi
    {
        public String id_anak_asuh { get; set; }
        public String id_orangtua_asli { get; set; }
        public String nama_anak_asuh { get; set; }
        public String status_anak { get; set; }
        public String tempat_lahir { get; set; }
        public String tanggal_lahir { get; set; }
        public String jk_anak_asuh { get; set; }
        public String anak_ke { get; set; }
        public String alamat { get; set; }
        public String jenjang_pendidikan { get; set; }
        public String kelas { get; set; }
        public String nama_sekolah { get; set; }
        public String foto_anak { get; set; }
        public String biaya_donasi { get; set; }
        public String status_donasi { get; set; }
        public String latitude { get; set; }
        public String longitude { get; set; }
    }
}
