using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    class AccountVM
    {
        public ICommand LoginCommand { get; set; }
        private string _loginname { get; set; }
        public string LoginName
        {
            get { return _loginname; }
            set { _loginname = value; }
        }

        private string _loginpassword { get; set; }
        public string LoginPassword
        {
            get { return _loginpassword; }
            set { _loginpassword = value; }
        }
       

        public AccountVM()
        {
            LoginCommand = new RelayCommand(Login);
        }

        //public string UserName
        //{
        //    get { return Models.Account.UserName; }
        //    set { Models.Account.UserName = value; }
        //}

        //public string Password
        //{

        //    get { return Models.Account.Password; }
        //    set { Models.Account.Password = value; }
        //}

        private void Login()
        {
            UserControl window = new UserControl();
            window.Show();
        }
    }
}
