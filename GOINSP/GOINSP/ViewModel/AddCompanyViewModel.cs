using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GOINSP.Models;

namespace GOINSP.ViewModel
{
    public class AddCompanyViewModel : ViewModelBase
    {
        private Context context;
        private string name;
        private string email;
        public ICommand btnSave { get; set; }

        public AddCompanyViewModel()
        {
            context = new Context();
            btnSave = new RelayCommand(save);
        }

        private void save()
        {
            if (name.Length > 0 && email.Length > 0)
            {
                try
                {
                    NewCompanyVM vm = new NewCompanyVM();
                    vm.Bedrijfsnaam = name;
                    vm.BedrijfsEmail = email;

                    context.Company.Add(vm.toCompany());
                    context.SaveChanges();
                    MessageBox.Show("Toegevoegd.");
                }
                catch
                {
                    MessageBox.Show("Er is iets fout gegaan. Probeer het opnieuw.");
                }
            }
            else
            {
                MessageBox.Show("Er moet iets ingevuld zijn.");
            }
        }

        public string txtName
        {
            get { return name; }
            set { name = value; }
        }

        public string txtEmail
        {
            get { return email; }
            set { email = value; }
        }
    }
}
