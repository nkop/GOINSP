using GOINSP.ViewModel;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GOINSP
{
    /// <summary>
    /// Interaction logic for LocationPicker.xaml
    /// </summary>
    public partial class LocationPicker : Window
    {
        public LocationPicker()
        {
            InitializeComponent();
        }

        private void PickerMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            PickerMap.Children.RemoveRange(0, PickerMap.Children.Count);

            Point mousePosition = e.GetPosition(this);

            Location pinLocation = PickerMap.ViewportPointToLocation(mousePosition);

            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;

            hiddenTextBoxLong.Text = pinLocation.Longitude.ToString();
            hiddenTextBoxLong.Focus();
            hiddenTextBoxLat.Text = pinLocation.Latitude.ToString();
            hiddenTextBoxLat.Focus();
            hiddenTextBoxConfirm.Focus();
            PickerMap.Children.Add(pin);
        }
    }
}
