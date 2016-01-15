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
using System.Data.Entity;
using GOINSP.Models;
using GOINSP.Utility;

namespace GOINSP.ViewModel
{
    public class AccountVM
    {
        
        private Account account;

        [PreferredConstructorAttribute]
        public AccountVM()
        {
            this.account = new Account();
        }

        public AccountVM(Account account)
        {
            this.account = account;
        }

        public Guid id
        {
            get { return account.id; }
            set { account.id = value; }
        }

        public string UserName
        {
            get { return account.UserName; }
            set { account.UserName = value;
            if (this.UserName != null && this.Password != null && this.Email != null && this.AccountRights != Account.Rights.Default)
            {
                var entry = Config.Context.Entry(account);
                entry.State = EntityState.Modified;
                Config.Context.SaveChanges();
            }                
            }
        }

        public string Password
        {

            get { return account.Password; }
            set { account.Password = value;
            if (this.UserName != null && this.Password != null && this.Email != null && this.AccountRights != Account.Rights.Default)
            {
                var entry = Config.Context.Entry(account);
                entry.State = EntityState.Modified;
                Config.Context.SaveChanges();
            }
            }
        }

        public string Email
        {
            get { return account.Email; }
            set { account.Email = value;
            if (this.UserName != null && this.Password != null && this.Email != null && this.AccountRights != Account.Rights.Default)
            {
                var entry = Config.Context.Entry(account);
                entry.State = EntityState.Modified;
                Config.Context.SaveChanges();
            }
            
            }
        }

        public Models.Account.Rights AccountRights
        {
            get { return account.AccountRights; }
            set
            {
                account.AccountRights = value;
                var entry = Config.Context.Entry(account);
                entry.State = EntityState.Modified;
                Config.Context.SaveChanges();
            }
        }

        public Models.Account ToAccount()
        {
            return this.account;
        }

        
        
    }

}
