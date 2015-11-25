using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace GOINSP.ViewModel
{
    public class AccountManagementVM : ViewModelBase
    {
        public ICommand LoginCommand { get; set; }
        public ICommand CreateAccountCommand { get; set; }
        public ICommand ShowAddUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public AccountVM SelectedAccount { get; set; }

        public ObservableCollection<AccountVM> Users { get; set; }

        public ObservableCollection<String> Rights { get; set; }

        Models.Context context;

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

        public string SearchQuota { get; set; }


        public AccountManagementVM()
        {
            SelectedAccount = new AccountVM();
            context = new Models.Context();

            SelectedAccount = new AccountVM();

            LoginCommand = new RelayCommand(Login);
            CreateAccountCommand = new RelayCommand(CreateAccount);
            ShowAddUserCommand = new RelayCommand(ShowAddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            SearchCommand = new RelayCommand(Search);

            Rights = new ObservableCollection<String>(Enum.GetNames(typeof(Models.Account.Rights)));
                       
            LoadUsers();
            
        }

        private void Search()
        {
            if (SearchQuota.Length > 0)
            {
                List<Models.Account> tempUsers = context.Account.ToList();
                List<AccountVM> tempUsersVM = new List<AccountVM>();
                foreach (Models.Account item in tempUsers)
                {
                    tempUsersVM.Add(new AccountVM(item));
                }
                Users.Clear();
                foreach (AccountVM item in tempUsersVM)
                {
                    if (item.UserName.Contains(SearchQuota) || item.Email.Contains(SearchQuota))
                    {
                        Users.Add(item);
                    }
                }
                RaisePropertyChanged("Users");
            }
        }

        private void LoadUsers()
        {
            List<Models.Account> tempUsers = context.Account.ToList();
            Users = new ObservableCollection<AccountVM>(tempUsers.Select(a => new AccountVM(a)).Distinct());
            RaisePropertyChanged("Users");
            
        }

        private void ShowAddUser()
        {
            AddUserWindow window = new AddUserWindow();
            window.Show();
        }

        private void CreateAccount()
        {            
            Models.Account NewAccount = new Models.Account();
            NewAccount.UserName = SelectedAccount.UserName;
            NewAccount.Password = SelectedAccount.Password;
            NewAccount.Email = SelectedAccount.Email;

            if (context.Account.Where(a => a.UserName == SelectedAccount.UserName).FirstOrDefault<Models.Account>() == null)
            {
                context.Account.Add(NewAccount);
                context.SaveChanges();
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Deze gebruikersnaam is al in gebruik.");
            }
        }

        private void DeleteUser()
        {
            AccountVM tempAccount = new AccountVM();
            tempAccount = SelectedAccount;
            if (SelectedAccount.UserName != null)
            {
                Models.Account AccToDelete = context.Account.Where(a => a.UserName == tempAccount.UserName).FirstOrDefault<Models.Account>();
                context.Entry(AccToDelete).State = EntityState.Deleted;
                context.SaveChanges();
            }
            SelectedAccount = null;
            LoadUsers();
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
    }
}
