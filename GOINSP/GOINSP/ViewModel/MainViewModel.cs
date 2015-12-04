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

        public MainViewModel()
        {
            _userControlWindow = new UserControl();

            UserControlCommand = new RelayCommand(ShowUserControl);
            OpenDataCommand = new RelayCommand(ShowOpenData);
        }

        private void ShowUserControl()
        {
            _userControlWindow.Show();
        }

        private void ShowOpenData()
        {
            OpenDataImport opendata = new OpenDataImport();
            opendata.Show();
        }
        

    }
}