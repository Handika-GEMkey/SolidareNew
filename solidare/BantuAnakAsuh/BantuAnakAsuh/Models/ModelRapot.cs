using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelRapot
    {
        public String id_anak_asuh { get; set; }
        public String nama_anak_asuh { get; set; }
        public String alamat { get; set; }
        public String jenjang_pendidikan { get; set; }
        public String kelas { get; set; }
        public String nama_sekolah { get; set; }
        public String foto_anak { get; set; }
        public String semester_pr { get; set; }
        public String tanggal_laporan { get; set; }
        public String file_laporan { get; set; }

        public String id_report { get; set; }
        public String id_fosterchildren { get; set; }
        public String name { get; set; }
        public String study_level { get; set; }
        public String school { get; set; }
        public String grade { get; set; }
        public String semester { get; set; }
        public String report_date { get; set; }
        public String report_file { get; set; }
        
    }
}
