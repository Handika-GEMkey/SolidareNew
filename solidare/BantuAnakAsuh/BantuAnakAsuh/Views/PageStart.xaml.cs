using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.IO.IsolatedStorage;
using System.IO;
using BantuAnakAsuh.Helper;

namespace BantuAnakAsuh.Views
{
    public partial class PageStart : PhoneApplicationPage
    {
        public PageStart()
        {
            
            InitializeComponent();
            this.cekLogin();
            VisualStateManager.GoToState(this, "Normal", false);
        }
        private void OpenClose_Left(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (left > -100)
            {
                MoveViewWindow(-420);
            }
            else
            {
                MoveViewWindow(0);
            }
        }
        private void OpenClose_Right(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (left > -520)
            {
                MoveViewWindow(-840);
            }
            else
            {
                MoveViewWindow(-420);

            }
        }

        void MoveViewWindow(double left)
        {
            _viewMoved = true;
            if (left == -480)
            {
            }
            else
            {
            }

            ((Storyboard)canvas.Resources["moveAnimation"]).SkipToFill();
            ((DoubleAnimation)((Storyboard)canvas.Resources["moveAnimation"]).Children[0]).To = left;
            ((Storyboard)canvas.Resources["moveAnimation"]).Begin();
        }

        private void canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.DeltaManipulation.Translation.X != 0)
                Canvas.SetLeft(LayoutRoot, Math.Min(Math.Max(-1440, Canvas.GetLeft(LayoutRoot) + e.DeltaManipulation.Translation.X), 0));
        }

        double initialPosition;
        bool _viewMoved = false;
        private void canvas_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            _viewMoved = false;
            initialPosition = Canvas.GetLeft(LayoutRoot);
        }

        private void canvas_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (_viewMoved)
                return;
            if (Math.Abs(initialPosition - left) < 100)
            {
                //bouncing back
                MoveViewWindow(initialPosition);
                return;
            }
            //change of state
            if (initialPosition - left > 0)
            {
                //slide to the left
                if (initialPosition > -480)
                {
                    MoveViewWindow(-480);
                }
                else if (initialPosition > -960)
                {
                    MoveViewWindow(-960);
                }
                else
                {
                    MoveViewWindow(-1440);
                }
            }
            else
            {
                //slide to the right
                if (initialPosition < -960)
                {
                    MoveViewWindow(-960);
                }
                else if (initialPosition < -480)
                {
                    MoveViewWindow(-480);
                }
                else
                {
                    MoveViewWindow(0);
                }
            }

        }


        string id;
        string count="";

        private void cekLogin()
        {
            id = "";
            this.LoadID();

            if (!id.Equals(""))
            {
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    if(count.Equals(""))
                    {
                        Navigation.countKeranjang = "0";
                    }
                    NavigationService.Navigate(new Uri("/Views/NewHomepage.xaml", UriKind.Relative));
                    //this.Loaded += StartedPage_Loaded;
                });
            }
        }

        public void LoadID()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    using (IsolatedStorageFileStream rawStream = isf.OpenFile("id_donatur", System.IO.FileMode.Open))
                    {
                        StreamReader reader = new StreamReader(rawStream);

                        id = reader.ReadToEnd();
                        Navigation.navIdLogin = id;
                        reader.Close();
                    }

                    using (IsolatedStorageFileStream rawStream = isf.OpenFile("countDonasi",System.IO.FileMode.Open))
                    {
                        StreamReader reader = new StreamReader(rawStream);
                        count = reader.ReadToEnd();
                        Navigation.countKeranjang = count; 
                        reader.Close();
                    }
                }
                catch
                {
                    //data tidak ditemukan
                }
            }
        }

        private void buttonLogin_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageLogin.xaml", UriKind.Relative));
        }

        private void buttonRegister_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/PageRegister.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Terminate();
        }

    }
}