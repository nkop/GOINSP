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

namespace GOINSP.ViewModel
{
    public class AccountVM
    {
        
        private Models.Account account;
        private Models.Context context;

        [PreferredConstructorAttribute]
        public AccountVM()
        {
            this.account = new Models.Account();
            this.context = new Models.Context();
        }

        public AccountVM(Models.Account account)
        {
            this.account = account;
            this.context = new Models.Context();
        }

        public string UserName
        {
            get { return account.UserName; }
            set { account.UserName = value;
                var entry = this.context.Entry(account);
                entry.State = EntityState.Modified; 
                context.SaveChanges();                
            }
        }

        public string Password
        {

            get { return account.Password; }
            set { account.Password = value;
            var entry = this.context.Entry(account);
            entry.State = EntityState.Modified;
            context.SaveChanges();
            }
        }

        public string Email
        {
            get { return account.Email; }
            set { account.Email = value;
            var entry = this.context.Entry(account);
            entry.State = EntityState.Modified;
            context.SaveChanges();
            }
        }

        public Models.Account.Rights AccountRights
        {
            get { return account.AccountRights; }
            set
            {
                account.AccountRights = value;
                var entry = this.context.Entry(account);
                entry.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Models.Account ToAccount()
        {
            return this.account;
        }

        
        
    }

}
