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
        //    //get { return Models.username; }
        //   // set { Models.login = value; }
        //}

        //public string Password
        //{
            
        //    //get { return Models.password; }
        //    //set { Models.password = value; }
        //}

        private void Login()
        {
            //if 
        }
    }
}
