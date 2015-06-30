using BantuAnakAsuh.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelBank : ViewModelBase
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

        private String photo;

        public String Photo
        {
            get { return photo; }
            set { photo = value; RaisePropertyChanged(""); }
        }

        private String id_account;
        public String Id_account
        {
            get { return id_account; }
            set { id_account = value; RaisePropertyChanged(""); }
        }

        private String bank;
        public String Bank
        {
            get { return bank; }
            set { bank = value; RaisePropertyChanged(""); }
        }

        private String account_number;
        public String Account_number
        {
            get { return account_number; }
            set { account_number = value; RaisePropertyChanged(""); }
        }

        private String account_name;
        public String Account_name
        {
            get { return account_name; }
            set { account_name = value; RaisePropertyChanged(""); }
        }
    }
}
