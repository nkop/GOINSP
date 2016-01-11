using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOINSP.Models;

namespace GOINSP.ViewModel
{
    class NewCompanyVM
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

        public Company toCompany()
        {
            return company;
        }
    }
}
