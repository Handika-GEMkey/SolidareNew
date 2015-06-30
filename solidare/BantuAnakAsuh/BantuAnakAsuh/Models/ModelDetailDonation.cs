using BantuAnakAsuh.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelDetailDonation : ViewModelBase
    {
        private String id_donation;

        public String Id_donation
        {
            get { return id_donation; }
            set { id_donation = value; RaisePropertyChanged(""); }
        }

        private String id_fosterchildren;

        public String Id_fosterchildren
        {
            get { return id_fosterchildren; }
            set { id_fosterchildren = value; RaisePropertyChanged(""); }
        }

        private String children_name;

        public String Children_name
        {
            get { return children_name; }
            set { children_name = value; RaisePropertyChanged(""); }
        }

        private String cost;

        public String Cost
        {
            get { return cost; }
            set { cost = value; RaisePropertyChanged(""); }
        }

        private String pre_donation_time;

        public String Pre_donation_time
        {
            get { return pre_donation_time; }
            set { pre_donation_time = value; RaisePropertyChanged(""); }
        }

        private String id_cha_org;

        public String Id_cha_org
        {
            get { return id_cha_org; }
            set { id_cha_org = value; RaisePropertyChanged(""); }
        }

        private String cha_org_name;

        public String Cha_org_name
        {
            get { return cha_org_name; }
            set { cha_org_name = value; RaisePropertyChanged(""); }
        }
        private String photo;

        public String Photo
        {
            get { return photo; }
            set { photo = value; RaisePropertyChanged(""); }
        }

    }
}
