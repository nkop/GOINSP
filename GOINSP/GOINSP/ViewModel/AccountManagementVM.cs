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
using GOINSP.Utility;


namespace GOINSP.ViewModel
{
    public class AccountManagementVM : ViewModelBase
    {
        public ICommand LoginCommand { get; set; }
        public ICommand VergetenCommand { get; set; }
        public ICommand CreateAccountCommand { get; set; }
        public ICommand ShowAddUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public AccountVM SelectedAccount { get; set; }
        public Models.Account.Rights newRights { get; set; }

        public ObservableCollection<AccountVM> Users { get; set; }

        public ObservableCollection<String> Rights { get; set; }

        public string Email { get; set; }

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

        private string _searchQuota { get; set; }
        public string SearchQuota
        {
            get { return _searchQuota; }
            set { _searchQuota = value;
            RaisePropertyChanged("SearchQuota");
            Search();
            }
        }


        public AccountManagementVM()
        {
            SelectedAccount = new AccountVM();

            LoginCommand = new RelayCommand(Login);
            VergetenCommand = new RelayCommand(ForgottenPass);
            CreateAccountCommand = new RelayCommand(CreateAccount);
            ShowAddUserCommand = new RelayCommand(ShowAddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);

            Rights = new ObservableCollection<String>(Enum.GetNames(typeof(Models.Account.Rights)));
                       
            LoadUsers();
            
        }

        private void Search()
        {
            if (SearchQuota.Length >= 0)
            {
                List<Models.Account> tempUsers = Config.Context.Account.ToList();
                List<AccountVM> tempUsersVM = new List<AccountVM>();
                foreach (Models.Account item in tempUsers)
                {
                    tempUsersVM.Add(new AccountVM(item));
                }
                Users.Clear();

                foreach (AccountVM item in tempUsersVM)
                {
                    if (item.UserName.ToLower().Contains(SearchQuota.ToLower()) || item.Email.ToLower().Contains(SearchQuota.ToLower()))
                    {
                        Users.Add(item);
                    }
                }

                RaisePropertyChanged("Users");
            }
        }

        private void LoadUsers()
        {
            List<Models.Account> tempUsers = Config.Context.Account.ToList();
            Users = new ObservableCollection<AccountVM>(tempUsers.Select(a => new AccountVM(a)));
            RaisePropertyChanged("Users");
        }

        private void ShowAddUser()
        {
            SelectedAccount = null;
            SelectedAccount = new AccountVM();
            AddUserWindow window = new AddUserWindow();
            window.Show();
        }

        private void CreateAccount()
        {            
            Models.Account NewAccount = new Models.Account();
            NewAccount.UserName = SelectedAccount.UserName;
            NewAccount.Password = SelectedAccount.Password;
            NewAccount.Email = Email;
            NewAccount.AccountRights = newRights;

            if (Config.Context.Account.Where(a => a.UserName == SelectedAccount.UserName).FirstOrDefault<Models.Account>() == null)
            {
                if (NewAccount.UserName != null && NewAccount.Password != null && NewAccount.Email != null &&
                    NewAccount.UserName.Length > 3 && NewAccount.Password.Length > 3 && NewAccount.Email.Length > 3)
                {
                    Config.Context.Account.Add(NewAccount);
                    Config.Context.SaveChanges();
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Een van de ingevoerde velden is te kort");
                }
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
            if (SelectedAccount.id != null)
            {
                Models.Account AccToDelete = Config.Context.Account.Where(a => a.UserName == tempAccount.UserName).FirstOrDefault<Models.Account>();
                Config.Context.Entry(AccToDelete).State = EntityState.Deleted;
                Config.Context.SaveChanges();
            }
            SelectedAccount = null;
            LoadUsers();
        }

        private void Login()
        {
            if (LoginName != null)
            {
                Models.Account account = Config.Context.Account.Where(a => a.UserName == LoginName).FirstOrDefault();
                if (account != null)
                {
                    if (LoginPassword == account.Password)
                    {
                        Config.GebruikerID = account.id;
                        Config.Rechten = account.AccountRights;

                        foreach (Window w in Application.Current.Windows)
                        {
                            w.Hide();
                        }

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
            else
            {
                MessageBox.Show("Voer een gebruikersnaam in.");
            }
            
        }

        private void ForgottenPass()
        {
            ForgottenPassword window = new ForgottenPassword();
            window.Show();
        }
    }
}
