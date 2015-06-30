using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BantuAnakAsuh.Common;
using System.Windows.Input;
using RestSharp;
using BantuAnakAsuh.Helper;
using Microsoft.Phone.Shell;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Data.Linq.Mapping;
using BantuAnakAsuh.Views;
using System.Windows.Navigation;

namespace BantuAnakAsuh.ViewModels
{
    class ViewModelEnvironment : ViewModelBase
    {
       

       
       

       
        public ICommand PublishCommand
        {
            get
            {
                return new DelegateCommand(PushToServer);
            }
        }

        private void PushToServer(object obj)
        {
            try
            {

                RestRequest request = new RestRequest(URL.BASE3 + "api/environment/environment.php", Method.POST);

               
                //calling server with restClient
                RestClient restClient = new RestClient();
                restClient.ExecuteAsync(request, (response) =>
                {
                    ShellToast toast = new ShellToast();
                    toast.Title = "Status Upload";
                    JObject jRoot = JObject.Parse(response.Content);
                    String result = jRoot.SelectToken("result").ToString();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (result.Equals("sukses"))
                        {
                            MessageBox.Show("Your posting successfully uploaded");

                        }
                        else
                        {

                            MessageBox.Show("Failed");
                        }
                    }
                    else
                    {
                        //error ocured during upload
                        MessageBox.Show("Your posting failed. Please check the Internet connection.");
                    }
                });
            }
            catch (Exception ec)
            {
                MessageBox.Show("Failed to display, the Internet connection is unstable.");
            }
            finally
            {
                //ProgressVisibiliy = Visibility.Visible;
            }
        }

        public byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = stream.Position;
            stream.Position = 0;

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                stream.Position = originalPosition;
            }
        }

       

    }
}
