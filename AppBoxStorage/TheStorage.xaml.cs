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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppBoxStorage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TheStorage : Page
    {
        WareHouseMethods _data;
        DispatcherTimer _timer;
        public TheStorage()
        {
            this.InitializeComponent();
            _data = (App.Current as App).Data;
            _timer = (App.Current as App).CheckExpired;
            _theList.ItemsSource = _data.GetBoxes();
            _y.BeforeTextChanging += _y_BeforeTextChanging;
            _x.BeforeTextChanging += _x_BeforeTextChanging;
            _num.BeforeTextChanging += _num_BeforeTextChanging;
            _timer.Tick += _timer_Tick;

        }

        private void _timer_Tick(object sender, object e)
        {
          _theList.ItemsSource= _data.GetBoxes();
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

        private void _goBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void _add_Click(object sender, RoutedEventArgs e)
        {
            if (_x.Text != "" && _y.Text != "" & _num.Text != "")
            {
                _data.AddBox(double.Parse(_x.Text), double.Parse(_y.Text), int.Parse(_num.Text));
                _num.Text = "";
                _y.Text = "";
                _x.Text = "";
                _theList.ItemsSource = _data.GetBoxes();
            }
        }
    }
}
