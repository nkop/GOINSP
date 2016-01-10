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
        public ICommand OpenInspectionCommand { get; set; }
        public ICommand ManagementInfoCommand { get; set; }

        public MainViewModel()
        {
            ManagementInfoCommand = new RelayCommand(ManagementInfo);
            UserControlCommand = new RelayCommand(ShowUserControl);
            OpenDataImportCommand = new RelayCommand(ShowOpenDataImport);
            OpenInspectionCommand = new RelayCommand(ShowInspectionOverview);
        }

        private void ShowUserControl()
        {
            _userControlWindow = new UserControl();
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
        }

        private void ShowInspectionOverview()
        {
            InspectionWindow _showInspectionOverview = new InspectionWindow();
            _showInspectionOverview.Show();
        }
    }
}