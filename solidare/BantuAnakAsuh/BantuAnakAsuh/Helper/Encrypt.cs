using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace BantuAnakAsuh.Helper
{
    public class Encrypt
    {
        public String EncryptIt(String s)
        {
            Encoding byteEncoder = Encoding.UTF8;

            byte[] rijnKey = byteEncoder.GetBytes("h504lXb1erd4ilw7"); //this is key for encrypt data
            byte[] rijnIV = byteEncoder.GetBytes("4hx7e4bwM15d0CrL"); //this is iv for encrypt data

            String result;
            
            AesManaged rijn = new AesManaged();
            
            //Using AES-128
            rijn.KeySize = 128;
            rijn.BlockSize = 128;

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (ICryptoTransform encryptor = rijn.CreateEncryptor(rijnKey, rijnIV))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(s);
                        }
                    }
                }
                result = Convert.ToBase64String(msEncrypt.ToArray());
            }
            rijn.Clear();

            return result;
        }
    }

    
}
