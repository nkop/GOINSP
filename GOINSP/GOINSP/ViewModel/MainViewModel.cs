using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace GOINSP.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private UserControl _userControlWindow;
        public ICommand UserControlCommand { get; set; }
        public ICommand OpenDataCommand { get; set; }
        public ICommand OpenDataImportCommand { get; set; }
        public ICommand OpenInspectionCommand { get; set; }
        public ICommand ManagementInfoCommand { get; set; }
        public ICommand OpenCompanyCommand { get; set; }

        public MainViewModel()
        {
            _userControlWindow = new UserControl();
            UserControlCommand = new RelayCommand(ShowUserControl);
            OpenDataCommand = new RelayCommand(ShowOpenData);
            ManagementInfoCommand = new RelayCommand(ManagementInfo);
            UserControlCommand = new RelayCommand(ShowUserControl);
            OpenDataImportCommand = new RelayCommand(ShowOpenDataImport);
            OpenInspectionCommand = new RelayCommand(ShowInspectionOverview);
            OpenCompanyCommand = new RelayCommand(showCompanyCrud);
        }

        private void showCompanyCrud()
        {
            
        }

        private void ShowUserControl()
        {
            _userControlWindow = new UserControl();
            _userControlWindow.Show();
        }

        private void ShowOpenData()
        {
            OpenDataImport opendata = new OpenDataImport();
            opendata.Show();
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