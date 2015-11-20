using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Ioc;

namespace GOINSP.ViewModel
{
    public class AccountVM
    {
        Models.Context context;
        private Models.Account account;
        public ICommand LoginCommand { get; set; }
        public ICommand CreateAccountCommand { get; set; }
        public ICommand ShowAddUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        
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

        public ObservableCollection<AccountVM> Users { get; set; }

        [PreferredConstructorAttribute]
        public AccountVM()
        {
            this.account = new Models.Account();
            context = new Models.Context();

            LoginCommand = new RelayCommand(Login);
            CreateAccountCommand = new RelayCommand(CreateAccount);
            ShowAddUserCommand = new RelayCommand(ShowAddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);

            List<Models.Account> tempUsers = context.Account.ToList();
            Users = new ObservableCollection<AccountVM>(tempUsers.Select(a => new AccountVM(a)).Distinct());
        }

        public AccountVM(Models.Account account)
        {
            this.account = account;
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
        private void ShowAddUser()
        {
            AddUserWindow window = new AddUserWindow();
            window.Show();               
        }

        private void CreateAccount()
        {
            Models.Account NewAccount = new Models.Account();
            NewAccount.UserName = LoginName;
            NewAccount.Password = LoginPassword;
            context.Account.Add(NewAccount);
            context.SaveChanges();
        }

        private void DeleteUser()
        {
            throw new NotImplementedException();
        }
    }

}
