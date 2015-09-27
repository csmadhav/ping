using ping.Common;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ping
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class levelup : Page
    {
        private NavigationHelper navigationHelper;
        public levelup()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            //throw new NotImplementedException();
            _checkedGoBackCommand = new RelayCommand(
                                    () => this.CheckGoBack(),
                                    () => this.CanCheckGoBack()
                                );
            navigationHelper.GoBackCommand = _checkedGoBackCommand;
            e.Handled = true;
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;

           
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
           // throw new NotImplementedException();
            //throw new NotImplementedException();
            Frame.BackStack.Clear();
            
           
            int temp = l.levelno - 1;
            level.Text = "Level " + temp + " Clear!";
            temp = l.score + 100;
            Score.Text = "Score:" + temp;
        }

       

        private void CommandInvokedHandler(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            if(command.Label=="yes")
            {
                Frame.Navigate(typeof(MainPage));
            }
        }

        Level l;
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        /// 
        RelayCommand _checkedGoBackCommand;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            l = (Level)e.Parameter;
            this.navigationHelper.OnNavigatedTo(e);
           

        }

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
                        Frame.BackStack.Clear();
                        Frame.Navigate(typeof(MainPage));

                        break;
                    }
                case "No":
                    {
                        break;
                    }
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Playarea), l);
        }
    }
}
