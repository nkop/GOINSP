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
            context.SaveChanges();
            }
        }

        public string Password
        {

            get { return account.Password; }
            set { account.Password = value;
            context.SaveChanges();
            }
        }

        public Models.Account ToAccount()
        {
            return this.account;
        }

        
        
    }

}
