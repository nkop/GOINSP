using GalaSoft.MvvmLight.Messaging;
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
    /// Interaction logic for QuestionListWindow.xaml
    /// </summary>
    public partial class QuestionListWindow : Window
    {
        public QuestionListWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<NotificationMessage>(this, (nm) =>
            {
                if (nm.Notification == "CloseView")
                {
                    if (nm.Sender == this.DataContext)
                        this.Close();
                }
            });
        }
    }
}
