using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BantuAnakAsuh.Models
{
    public class ModelValidasiPost
    {

        private static ModelValidasiPost instance;
        private static readonly IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        private string pesanvalidasi;

        public ModelValidasiPost()
        {
            pesanvalidasi = this.LoadSettings("pesanValidasi");
           
        }

        #region IsolatedStorage
        private void SaveSettings(string key, string value)
        {
            if (!settings.Contains(key))
                settings.Add(key, value);
            else
                settings[key] = value;
            settings.Save();
        }

        private string LoadSettings(string key)
        {
            string result = "";

            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                result = IsolatedStorageSettings.ApplicationSettings[key] as string;

            return result;
        }
        #endregion

        public static ModelValidasiPost Instance
        {
            get
            {
                if (instance == null)
                    instance = new ModelValidasiPost();
                return instance;
            }
            set { instance = value; }
        }

        public string PesanValidasi
        {
            get { return pesanvalidasi; }
            set
            {
                pesanvalidasi = value;
                this.SaveSettings("namaPengguna", pesanvalidasi);
            }

        }

    }
}
