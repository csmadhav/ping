using ping.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.Data.Xml.Dom;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ping
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Playarea : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Playarea()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        /// 
        DispatcherTimer dt = new DispatcherTimer();
        int[] i = new int[6];
        List<int> sorted=new List<int>();
        public int levelno;
        
        public int displaytime;
        public int numbers;
        public int numberlimit;
        public int scoreint;
        public int lives;
        public int think=0;
        Level l;
        DispatcherTimer thinktime = new DispatcherTimer();
        public Windows.UI.Color GetRandomColor()
        {
            
            Color[] knowncolors = { Colors.Red,Colors.Violet,Colors.Tomato,Colors.Teal,Colors.SteelBlue,Colors.Snow,Colors.SeaGreen,Colors.RosyBrown,Colors.Pink,Colors.LimeGreen };
            Random r = new Random();
            return knowncolors[r.Next(9)];

        }
        RelayCommand _checkedGoBackCommand;
        private bool CanCheckGoBack()
        {
            return true;
        }

        private async void CheckGoBack()
        {
            //Debug.WriteLine("CheckGoBack");
            MessageDialog dlg = new MessageDialog("Do you want to quit? Your progress will be lost", "Quit?");
            dlg.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandHandler1)));
            dlg.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandHandler1)));

            await dlg.ShowAsync();
        }

        private void CommandHandler1(IUICommand command)
        {
            var label = command.Label;
            switch (label)
            {
                case "Yes":
                    {
                        if (thinktime.IsEnabled == true)
                            thinktime.Stop();
                        Frame.Navigate(typeof(MainPage));

                        break;
                    }
                case "No":
                    {
                        break;
                    }
            }
        }
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            Frame.BackStack.Clear();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            //_checkedGoBackCommand = new RelayCommand(
            //                        () => this.CheckGoBack(),
            //                        () => this.CanCheckGoBack()
            //                    );

            //navigationHelper.GoBackCommand = _checkedGoBackCommand;
            
            l = (Level)e.NavigationParameter;
            think = l.thinktime;
            time.Text = "TIME: " + think+" s";
            if((l.levelno==3)||(l.levelno==4))
            {
                l.displaytime=3;
            }
            if (l.levelno > 4)
            {
                l.displaytime = 4;
            }
            else
            {
                if (l.levelno > 12)
                {
                    l.displaytime = 3;
                }
                else
                {
                    if (l.levelno > 23)
                    {
                        l.displaytime = 2;
                    }
                }
            }
            this.levelno = l.levelno;
            
            this.displaytime = l.displaytime;
            this.numbers = l.numbers;
            this.numberlimit = l.numberlimit;
            this.lives = l.lives;
            if(lives==1)
            {
                clear.Foreground = new SolidColorBrush(Colors.Red);
            }
            this.scoreint = l.score;
            if (scoreint != 0)
            {
                int temp = scoreint + 100;
                score.Text = "SCORE:" + temp;
            }
            else
                score.Text = "SCORE:" + scoreint;
            level.Text = "LEVEL " + levelno.ToString();
            score.HorizontalAlignment = HorizontalAlignment.Center;
            Random r = new Random();
            
            double x;
            
            //for (int f = 0; f < numbers;++f )
            //{
            //    i[f] = r.Next(1, numberlimit);
            //}
            //for (int f = 0; f < numbers;++f )
            //{
            //    for(int k=0;k<numbers;++k)
            //    {
            //        if (k == f)
            //            continue;
            //        else
            //        {
            //            if(i[k]==i[f])
            //            {
            //                while(i[k]==i[f])
            //                {
            //                    i[f] = r.Next(1, numberlimit);

            //                }
            //            }
            //        }

            //    }

            //}
            int temps;
            int k;
            i[0]=r.Next(1, numberlimit);
            for (int f = 1; f < numbers;++f )
            {
                temps = r.Next(1, numberlimit);
                k = f - 1;
                while(k!=-1)
                {
                    while(i[k]==temps)
                    {
                        temps = r.Next(1, numberlimit);
                    }
                    k--;
                    i[f] = temps;
                }
            }




                for (int j = 0; j < numbers; ++j)
                {
                    Grid g = new Grid();

                    TextBlock tb = new TextBlock();
                    tb.Text = i[j].ToString();
                    tb.Foreground = new SolidColorBrush(Colors.Black);
                    tb.HorizontalAlignment = HorizontalAlignment.Left;
                    x = r.NextDouble() * 200;
                    tb.Tapped += tb_Tapped;
                    tb.Name = i[j].ToString();

                    Ellipse el = new Ellipse();
                    el.Height = 60;
                    el.Width = 60;
                    g.Margin = new Thickness(x, 20, 0, 0);

                    el.Fill = new SolidColorBrush(GetRandomColor());


                    el.Opacity = 0.5;


                    tb.FontSize = 50;
                    //if (numberlimit < 10)
                    //{
                    //    tb.Margin = new Thickness(x + (el.Height / 2) - (tb.FontSize / 2) , (el.Height / 2) - (tb.FontSize / 2)+10, 0, 0);
                    //}
                    //else
                    //{
                    //    tb.Margin = new Thickness(x + (el.Height / 2) - (tb.FontSize / 2) , 10+(el.Height / 2) - (tb.FontSize / 2), 0, 0);

                    //}
                    el.HorizontalAlignment = HorizontalAlignment.Center;
                    tb.HorizontalAlignment = HorizontalAlignment.Center;
                    tb.VerticalAlignment = VerticalAlignment.Center;
                    g.Children.Add(el);
                    g.Children.Add(tb);

                    stack.Children.Add(g);


                }
            
            dt.Interval = new TimeSpan(0, 0, displaytime);
            thinktime.Interval = new TimeSpan(0, 0, 1);
            thinktime.Tick += thinktime_Tick;
            dt.Tick += dt_Tick;
            dt.Start();
            for(int j=0;j<numbers;++j)
            {
                sorted.Add(i[j]);
            }
            clear.Text = "LIVES:" + this.lives;
            thtime = think;


        }
        bool pressed=false;
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            //throw new NotImplementedException();
          

                _checkedGoBackCommand = new RelayCommand(
                                    () => this.CheckGoBack(),
                                    () => this.CanCheckGoBack()
                                );
                pressed = true;
                navigationHelper.GoBackCommand = _checkedGoBackCommand;
                e.Handled = true;
            
        }
        int thtime;
        void thinktime_Tick(object sender, object e)
        {
            
            thtime--;
            time.Text = "TIME: " + thtime+" s";
            if(thtime==0)
            {
                if (pressed == true)
                {
                    thinktime.Stop();
                    return;
                }
                thinktime.Stop();
                Frame.Navigate(typeof(BasicPage3),l);
            }


            //throw new NotImplementedException();
        }
      

       
        
        async void  tb_Tapped(object sender, TappedRoutedEventArgs e)
        {
            

            if (!dt.IsEnabled)
            {
                
                Blop.Play();
                TextBlock tb = (TextBlock)sender;
                
                if (tb.Text != "O")
                    return;

                int s = Int32.Parse(tb.Name);
                if (sorted.Count == 1)
                {
                    clear.Visibility = Visibility.Visible;
                    l.levelno++;
                    if (l.numbers < 6)
                    {
                        l.numbers++;
                    }
                   
                    if (l.levelno < 10)
                        l.numberlimit = 10;
                    else
                    {
                        if (l.levelno < 20)
                            l.numberlimit = 20;
                        else
                        {
                            if (l.levelno < 30)
                                l.numberlimit = 30;
                            else
                            {
                                l.numberlimit = 40;
                            }

                        }
                    }
                    //MessageDialog mg = new MessageDialog("Congrats!!! You Win");
                    //await mg.ShowAsync();
                    if(l.thinktime<15)
                    l.thinktime++;
                    Frame.Navigate(typeof(levelup), l);
                }
                if (s == sorted.Min())
                {
                    l.score += 100;
                    score.Text = "SCORE:" + l.score.ToString();
                    sorted.Remove(s);
                    tb.Text = s.ToString();
                    
                }
                else
                {


                   
                    
                        
                        
                        bool win=highsc();

                        
                        lives--;

                        if (lives == 0)
                        {
                            if (win == false)
                            {
                                
                                MessageDialog mg = new MessageDialog("YOU LOSE! your score:" + l.score,"Game Over");
                                await mg.ShowAsync();
                            }
                            else
                            {
                                MessageDialog mg = new MessageDialog("Congrats! High score! your score:" + l.score,"Game Over");
                                await mg.ShowAsync();

                            }
                            AdDuplex.Universal.Controls.WinPhone.XAML.InterstitialAd interstitialAd = new AdDuplex.Universal.Controls.WinPhone.XAML.InterstitialAd("146319");
                            interstitialAd.ShowAd();
                            thinktime.Stop();
                            Frame.Navigate(typeof(MainPage));
                        }
                        else
                        {
                            l.lives = lives;

                            thinktime.Stop();
                            MessageDialog mg = new MessageDialog("LIFE GONE! you have " + l.lives+" left");
                            await mg.ShowAsync();
                           
                            Frame.Navigate(typeof(Playarea), l);
                            
                        }
                    

                }
               

                
            }
           
           
           

        }

       
       
        private bool highsc()
        {
            Windows.Storage.ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localsettings.Values.ContainsKey("score"))
            {
                if (Convert.ToInt32(localsettings.Values["score"]) < l.score)
                {
                    localsettings.Values["score"] = l.score;
                    return true;
                }

            }
            else
            {
                localsettings.Values["score"] = l.score;
                return true;
            }
            return false;


        }

        void dt_Tick(object sender, object e)
        {
            dt.Stop();
            
            foreach( UIElement t in stack.Children)
            {
                if (t is Grid)
                {

                    foreach (UIElement tx in ((Grid)t).Children)
                    {
                        if (tx is TextBlock)
                        {
                            TextBlock r = (TextBlock)tx;
                            r.Text = "O";
                        }
                    }
                }
                    
                
            }
            thinktime.Start();
            
            
        }

        

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            
            thinktime.Stop();
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            thinktime.Stop();
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
