using BantuAnakAsuh.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelOrganization : ViewModelBase
    {
        private String id_donation;
        public String Id_donation
        {
            get { return id_donation; }
            set { id_donation = value; RaisePropertyChanged(""); }
        }

      
        private String children_name;

        public String Children_name
        {
            get { return children_name; }
            set { children_name = value; RaisePropertyChanged(""); }
        }
        private String id_cha_org;

        public String Id_cha_org
        {
            get { return id_cha_org; }
            set { id_cha_org = value; RaisePropertyChanged(""); }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(""); }
        }

        private String address;

        public String Address
        {
            get { return address; }
            set { address = value; RaisePropertyChanged(""); }
        }

        private String phone;

        public String Phone
        {
            get { return phone; }
            set { phone = value; RaisePropertyChanged(""); }
        }

        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; RaisePropertyChanged(""); }
        }

        private String website;

        public String Website
        {
            get { return website; }
            set { website = value; RaisePropertyChanged(""); }
        }

        private String photo;

        public String Photo
        {
            get { return photo; }
            set { photo = value; RaisePropertyChanged(""); }
        }
    }
}
