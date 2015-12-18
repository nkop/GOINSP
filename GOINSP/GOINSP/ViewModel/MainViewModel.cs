using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace GOINSP.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private UserControl _userControlWindow;
        public ICommand UserControlCommand { get; set; }
        public ICommand OpenDataImportCommand { get; set; }
<<<<<<< HEAD
        public ICommand OpenInspectionCommand { get; set; }

        public MainViewModel()
=======

        public ICommand ManagementInfoCommand { get; set; }

        public MainViewModel()
>>>>>>> refs/remotes/origin/master
        {
            _userControlWindow = new UserControl();

            ManagementInfoCommand = new RelayCommand(ManagementInfo);
            UserControlCommand = new RelayCommand(ShowUserControl);
            OpenDataImportCommand = new RelayCommand(ShowOpenDataImport);
<<<<<<< HEAD
            OpenInspectionCommand = new RelayCommand(ShowInspectionOverview);
=======
>>>>>>> refs/remotes/origin/master
        }

        private void ShowUserControl()
        {
            _userControlWindow.Show();
        }

        private void ManagementInfo()
        {
            ManagementInformationWindow _managementInfoWindow = new ManagementInformationWindow();
            _managementInfoWindow.Show();
        }
        private void ShowOpenDataImport()
        {
            OpenDataImport _openDataImportWindow = new OpenDataImport();
            _openDataImportWindow.Show();
<<<<<<< HEAD
        }

        private void ShowInspectionOverview()
        {
            InspectionWindow _showInspectionOverview = new InspectionWindow();
            _showInspectionOverview.Show();
        }
    }
=======
        }
    }
>>>>>>> refs/remotes/origin/master
}