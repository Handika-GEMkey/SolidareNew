using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;  
using System.Data.Linq;  
using System.Data.Linq.Mapping; 

namespace BantuAnakAsuh.Models
{
   
    public class ModelGetKeranjang
    {

        public String id_donation { get; set; }
        public String id_fosterchildren { get; set; }
        public String children_name { get; set; }
        public String cost { get; set; }
        public String pre_donation_time { get; set; }
        public String id_cha_org { get; set; }
        public String cha_org_name { get; set; }
        public String photo { get; set; }
        public String payment_status { get; set; }
        public String confirmation_status { get; set; }
    }
}
