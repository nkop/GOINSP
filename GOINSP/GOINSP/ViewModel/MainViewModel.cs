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

        public MainViewModel()
        {
            _userControlWindow = new UserControl();

            UserControlCommand = new RelayCommand(ShowUserControl);
            OpenDataImportCommand = new RelayCommand(ShowOpenDataImport);
        }

        private void ShowUserControl()
        {
            _userControlWindow.Show();
        }

        private void ShowOpenDataImport()
        {
            OpenDataImport _openDataImportWindow = new OpenDataImport();
            _openDataImportWindow.Show();
        }
    }
}