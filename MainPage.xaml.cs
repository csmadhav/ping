using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace ping
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
       

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        int count = 1;
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            count = 1;
            dt.Stop();
            dt.Tick -= dt_Tick;
            play.Opacity = 0;
            About.Opacity = 0;
            hplay.Opacity = 0;
            quit.Opacity = 0;
            tutorial.Opacity = 0;

        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AdDuplex.Universal.Controls.WinPhone.XAML.InterstitialAd interstitialAd = new AdDuplex.Universal.Controls.WinPhone.XAML.InterstitialAd("146319");

            interstitialAd.LoadAd();
            dt.Interval = new TimeSpan(0,0,0,0,250);
            dt.Tick += dt_Tick;
            dt.Start();
            show.Begin();
            Frame.BackStack.Clear();
            Windows.Storage.ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if(localsettings.Values.ContainsKey("score"))
            {
                Hignscore.Text = localsettings.Values["score"].ToString();
            }
            else
            {
                Hignscore.Text = " 0";
            }
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        void dt_Tick(object sender, object e)
        {
            switch(count)
            {
                
                case 1: show1.Begin();
                    break;
                case 2: show2.Begin();
                    break;
                case 3: show3.Begin();
                    break;
                case 4: show4.Begin();
                    break;
                case 5: dt.Stop();
                    break;
            }
            count++;
            //throw new NotImplementedException();
        }
        DispatcherTimer dt=new DispatcherTimer();
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Level l = new Level();
            l.levelno = 1;
            l.numbers = 3;
            
            l.displaytime = 2;
            l.numberlimit = 10;
            l.score = 0;
            l.lives = 3;
            l.thinktime = 5;
            if(!Frame.Navigate(typeof(Playarea),l))
            {
                MessageDialog m = new MessageDialog("Unable to create next page");
                await m.ShowAsync();
            }


        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!Frame.Navigate(typeof(BasicPage1)))
            {
                MessageDialog m = new MessageDialog("Unable to create next page");
                await m.ShowAsync();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(howtoplay));

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BasicPage2));
        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
           //reviewfunction();
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + "75fcd60c-3a34-410e-a08c-e456068e95e1"));
        }
    }
}
