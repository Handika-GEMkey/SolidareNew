using BantuAnakAsuh.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelLogin
    {
        private String id_donatur;

        public String Id_donatur
        {
            get { return id_donatur; }
            set { id_donatur = value;}
        }
        private String username;

        public String Username
        {
            get { return username; }
            set { username = value;  }
        }
        private String password;

        public String Password
        {
            get { return password; }
            set { password = value;  }
        }
        private String nama_donatur;

        public String Nama_donatur
        {
            get { return nama_donatur; }
            set { nama_donatur = value;  }
        }
        private String alamat_donatur;

        public String Alamat_donatur
        {
            get { return alamat_donatur; }
            set { alamat_donatur = value;  }
        }
        private String email_donatur;

        public String Email_donatur
        {
            get { return email_donatur; }
            set { email_donatur = value;  }
        }
        private String no_tlp;

        public String No_tlp
        {
            get { return no_tlp; }
            set { no_tlp = value;  }
        }
        private String foto_donatur;

        public String Foto_donatur
        {
            get { return foto_donatur; }
            set { foto_donatur = value;  }
        }
        private String jk_donatur;

        public String Jk_donatur
        {
            get { return jk_donatur; }
            set { jk_donatur = value;  }
        }
        private String tgl_register;

        public String Tgl_register
        {
            get { return tgl_register; }
            set { tgl_register = value;  }
        }
        private String status_donatur;

        public String Status_donatur
        {
            get { return status_donatur; }
            set { status_donatur = value;  }
        }
    }
}
