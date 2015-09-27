using ping.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ping
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BasicPage2 : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public BasicPage2()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            Windows.Phone.UI.Input.HardwareButtons.BackPressed+=HardwareButtons_BackPressed;
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
{
    Frame.Navigate(typeof(MainPage));
    e.Handled = true;
 	//throw new NotImplementedException();
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
        int[] i = { 3, 1, 2 };
        double x;
        Random r = new Random();
        List<int> sorted = new List<int>();
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            for (int j = 0; j < 3; ++j)
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

                el.Fill = new SolidColorBrush(Colors.White);


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
            for (int j = 0; j < 3; ++j)
            {
                sorted.Add(i[j]);
            }
        }

        private void tb_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //throw new NotImplementedException();
            if (step >= 5)
            {
                TextBlock tb = (TextBlock)sender;
                int s = Convert.ToInt32(tb.Name);
                if (s == sorted.Min())
                {
                    if (s == 1)
                        text.Text = "good! now tap the third bubble!";
                    if (s == 2)
                        text.Text = "good! now tap the first bubble!";
                    if (s == 3)
                    {
                        text.Text = "Awesome! now you are ready to go!";
                        bu.Content = "go to the main page";
                        bu.Click += bu_Click;
                    }

                    tb.Text = tb.Name;

                }
                else
                {
                    text.Text = "You did it wrong";
                }
                sorted.Remove(s);

            }
        }

        private void bu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
            //throw new NotImplementedException();
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
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
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
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        int step=1;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            step++;
            if (step == 2)
            {
                text.Text = "2. And will be turned into bubbles after some time";
                foreach (UIElement t in stack.Children)
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
            }
            if(step==3)
            {
                text.Text = "3. You have to remember those numbers";


            }
            if(step==4)
            {
                text.Text = "4. And tap the bubbles of the numbers in Ascending order";
            }
            if(step==5)
            {
                text.Text = "First tap the middle bubble!..";
            }

        

        }
    }
}
