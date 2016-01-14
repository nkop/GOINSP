using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOINSP.Models;

namespace GOINSP.ViewModel
{
    public class NewCompanyVM
    {
        private Company company;

        public NewCompanyVM()
        {
            company = new Company();
        }

        public NewCompanyVM(Company company)
        {
            this.company = company;
        }

        public Guid ID
        {
            get { return company.companyid; }
            set { company.companyid = value; }
        }

        public string Bedrijfsnaam
        {
            get { return company.BedrijfsNaam; }
            set 
            { 
                company.BedrijfsNaam = value;
            }
        }

        public string BedrijfsEmail
        {
            get { return company.BedrijfsEmail; }
            set
            {
                company.BedrijfsEmail = value;
            }
        }

        public string BedrijfsAdres
        {
            get { return company.BedrijfsAdres; }
            set { company.BedrijfsAdres = value; }
        }

        public string BedrijfsPostcode
        {
            get { return company.BedrijfsPostcode; }
            set { company.BedrijfsPostcode = value; }
        }

        public Company toCompany()
        {
            return company;
        }
    }
}
