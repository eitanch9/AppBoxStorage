using BoxDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppBoxStorage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        WareHouseMethods _data;
        DispatcherTimer _timer;
        public MainPage()
        {
            this.InitializeComponent();
            _data = (App.Current as App).Data;
            _timer = (App.Current as App).CheckExpired;
            _timer.Tick += _timer_Tick;
            _y.BeforeTextChanging += _y_BeforeTextChanging;
            _x.BeforeTextChanging += _x_BeforeTextChanging;
            _num.BeforeTextChanging += _num_BeforeTextChanging;
        }

        private void _timer_Tick(object sender, object e)
        {
            _TheList.ItemsSource = null;
        }

        private void _num_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => (!char.IsNumber(c)));
        }

        private void _x_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => (!char.IsNumber(c) && (c != '.')));
        }

        private void _y_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => (!char.IsNumber(c) && (c != '.')));
        }

        private void _goToTheStorageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TheStorage));
        }

        private void _getOfferButton_Click(object sender, RoutedEventArgs e)
        {
            if (_x.Text!="" && _y.Text!="" & _num.Text!="")
            {
                _TheList.ItemsSource = _data.GetPriceOffer( double.Parse(_x.Text), double.Parse(_y.Text), int.Parse(_num.Text)).AsEnumerable();
                int check = _data.CheakOffer(int.Parse(_num.Text));
                switch (check)
                {
                    case 0:
                        {
                            _checkOfferText.Text = "Sorry we don't have a suitable offer for you.";
                            _checkOfferText.Foreground = new SolidColorBrush(Colors.Red);
                            _TheList.ItemsSource = null;
                            break;
                        }
                    case -1:
                        {
                            _checkOfferText.Text = "Sorry we couldn't get the quantity you requested. This is the offer we managed to get.";
                            _checkOfferText.Foreground = new SolidColorBrush(Colors.Red);
                            break;
                        }
                    case 1:
                        {
                            _checkOfferText.Text = "this is the best offer for you";
                            _checkOfferText.Foreground = new SolidColorBrush(Colors.Green);
                            break;
                        }

                }
                _num.Text = "";
                _y.Text = "";
                _x.Text = "";
                

            }
        }

        private void _cleanButton_Click(object sender, RoutedEventArgs e)
        {
            _num.Text = "";
            _y.Text = "";
            _x.Text = "";
            _checkOfferText.Text = "";
            _TheList.ItemsSource = null;
        }

        private async void _TakeOfferButton_Click(object sender, RoutedEventArgs e)
        {
            if (_TheList.ItemsSource != null)
            {
                
                _data.Buy();
             await  new MessageDialog("Thanks you for your buy").ShowAsync(); 

            }
        }
    }
}
