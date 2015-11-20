using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace GOINSP.ViewModel
{
    public class AccountVM
    {
        Models.Context context;
        private Models.Account account;
        public ICommand LoginCommand { get; set; }
        public ICommand CreateAccountCommand { get; set; }
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
            this.account = new Models.Account();
            context = new Models.Context();

            LoginCommand = new RelayCommand(Login);
            CreateAccountCommand = new RelayCommand(CreateAccount);
           
        }


        public string UserName
        {
            get { return account.UserName; }
            set { account.UserName = value; }
        }

        public string Password
        {

            get { return account.Password; }
            set { account.Password = value; }
        }

        public Models.Account ToAccount()
        {
            return this.account;
        }

        private void Login()
        {
           //MenuControl window = new MenuControl();
           //window.Show();


           Models.Account account = context.Account.Where(a => a.UserName == LoginName).FirstOrDefault();
           if (account.UserName != null)
           {
               if (LoginPassword == account.Password)
               {
                   MenuControl window = new MenuControl();
                   window.Show();
               }
               else
               {
                   MessageBox.Show("De combinatie van gebruikersnaam & wachtwoord is onjuist.");
               }
           }
           else
           {
               MessageBox.Show("Deze gebruikersnaam is niet bekend in het systeem.");
           }
        }
        private void CreateAccount()
        {
            this.UserName = "Admin";
            this.Password = "123";
            context.Account.Add(this.ToAccount());
            context.SaveChanges();
        }
    }

}
