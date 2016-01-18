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
using GOINSP.Utility;

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

            MessageBox.Show(Config.Rechten.ToString());

            if (Config.Rechten == Models.Account.Rights.Administrator)
            {

            }
            else if (Config.Rechten == Models.Account.Rights.Manager)
            {
                UserControl.Visibility = Visibility.Collapsed;
            }
            else if (Config.Rechten == Models.Account.Rights.ExterneInspecteur)
            {
                UserControl.Visibility = Visibility.Collapsed;
                Management.Visibility = Visibility.Collapsed;
                OpenData.Visibility = Visibility.Collapsed;
                Bedrijven.Visibility = Visibility.Collapsed;
            }
            else if (Config.Rechten == Models.Account.Rights.InterneInspecteur)
            {
                UserControl.Visibility = Visibility.Collapsed;
                Management.Visibility = Visibility.Collapsed;
                OpenData.Visibility = Visibility.Collapsed;
            }
            else
            {
                UserControl.Visibility = Visibility.Collapsed;
                Management.Visibility = Visibility.Collapsed;
                OpenData.Visibility = Visibility.Collapsed;
                Bedrijven.Visibility = Visibility.Collapsed;
            }

        }
    }
}
