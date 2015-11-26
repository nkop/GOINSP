using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace GOINSP.ViewModel
{
    public class ForgotPasswordVM : ViewModelBase
    {
        private Models.Account account;
        private Models.Context context;
        private string _username;
        private string _email;
        public ICommand SubmitPassword { get; set; }
        public ObservableCollection<AccountVM> Users { get; set; }

        public ForgotPasswordVM()
        {
            this.account = new Models.Account();
            this.context = new Models.Context();

            List<Models.Account> tempUsers = context.Account.ToList();
            Users = new ObservableCollection<AccountVM>(tempUsers.Select(a => new AccountVM(a)).Distinct());

            SubmitPassword = new RelayCommand(askNewPassword);
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private void askNewPassword()
        {
            test
        }
    }
}