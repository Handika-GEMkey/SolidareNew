using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelProfileDonatur
    {
        public String id_fosterchildren { get; set; } //for list anak asuh donatur
        public String name { get; set; }
        public String pob { get; set; } //for list donatur
        public String dob { get; set; } //for list anak asuh donatur
        public String gender { get; set; }
        public String address { get; set; }
        public String photo { get; set; }
        public String cost { get; set; }
        public String children_status { get; set; }
        public String latitude { get; set; }
        public String longitude { get; set; }
        public String study_level { get; set; } //for list anak asuh donatur
        public String school { get; set; }
        public String grade { get; set; }
        public String parent_name { get; set; } //for list anak asuh donatur
        public String parent_address { get; set; }
        public String jobs { get; set; }
        public String salary { get; set; }
        public String id_cha_org { get; set; }
        public String cha_org_name { get; set; }
        public String id_program { get; set; }
        public String program_name { get; set; }
    }
}
