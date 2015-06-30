using BantuAnakAsuh.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    class ModelProgram : ViewModelBase
    {
        private String id_program;

        public String Id_program
        {
            get { return id_program; }
            set { id_program = value; RaisePropertyChanged(""); }
        }
        private String program_name;

        public String Program_name
        {
            get { return program_name; }
            set { program_name = value; RaisePropertyChanged(""); }
        }
        private String description;

        public String Description
        {
            get { return description; }
            set { description = value; RaisePropertyChanged(""); }
        }
    }
}
