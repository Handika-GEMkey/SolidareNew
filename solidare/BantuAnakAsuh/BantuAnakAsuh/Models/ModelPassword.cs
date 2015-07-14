using BantuAnakAsuh.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelPassword : ViewModelBase
    {
        private String password;

        public String Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(""); }
        }
        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(""); }
        }
        private String phone;

        public String Phone
        {
            get { return phone; }
            set { phone = value; RaisePropertyChanged(""); }
        }
    }
}
