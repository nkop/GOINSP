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
    /// Interaction logic for MenuControl.xaml
    /// </summary>
    public partial class MenuControl : Window
    {
        public MenuControl(string rights)
        {
            InitializeComponent();
            if (rights == "Administrator")
            {
                
            }
            else if (rights == "Manager")
            {
                UserControl.Visibility = Visibility.Collapsed;
            }
            else if (rights == "ExterneInspecteur")
            {
                UserControl.Visibility = Visibility.Collapsed;
                Management.Visibility = Visibility.Collapsed;
            }
            else if (rights == "InterneInspecteur")
            {
                UserControl.Visibility = Visibility.Collapsed;
                Management.Visibility = Visibility.Collapsed;
            }
            else
            {
                UserControl.Visibility = Visibility.Collapsed;
                Management.Visibility = Visibility.Collapsed;
            }

        }
    }
}
