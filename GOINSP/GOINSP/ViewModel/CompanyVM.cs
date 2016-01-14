using GOINSP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel
{
    public class CompanyVM
    {
        private Company company;
        private Context context;

        public CompanyVM()
        {
            company = new Company();
            this.context = new Context();
        }

        public CompanyVM(Company company)
        {
            this.company = company;
            this.context = new Context();
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
                var entry = this.context.Entry(company);
                entry.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public string BedrijfsEmail
        {
            get { return company.BedrijfsEmail; }
            set
            {
                company.BedrijfsEmail = value;
                var entry = this.context.Entry(company);
                entry.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Company toCompany()
        {
            return company;
        }
    }
}
