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

        private string _searchQuota { get; set; }
        public string SearchQuota
        {
            get { return _searchQuota; }
            set { _searchQuota = value;
            RaisePropertyChanged("SearchQuota");
            Search();
            }
        }

        private LoginAuthentication _loginauth;

        public AccountManagementVM()
        {
            SelectedAccount = new AccountVM();
            context = new Models.Context();
            _loginauth = new LoginAuthentication();

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
            if (LoginName != null)
            {
                Models.Account account = context.Account.Where(a => a.UserName == LoginName).FirstOrDefault();
                if (account != null)
                {
                    if (LoginPassword == account.Password)
                    {
                        _loginauth.SaveLoginId(account.id);

                        foreach (Window w in Application.Current.Windows)
                        {
                            w.Hide();
                        }

                        MenuControl window = new MenuControl(account.AccountRights.ToString());
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
