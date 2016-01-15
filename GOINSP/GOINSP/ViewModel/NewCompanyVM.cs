using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOINSP.Models;
using GalaSoft.MvvmLight;

namespace GOINSP.ViewModel
{
    public class NewCompanyVM : ViewModelBase
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
                RaisePropertyChanged("Bedrijfsnaam");
            }
        }

        public string BedrijfsEmail
        {
            get { return company.BedrijfsEmail; }
            set
            {
                company.BedrijfsEmail = value;
                RaisePropertyChanged("BedrijfsEmail");
            }
        }

        public string BedrijfsAdres
        {
            get { return company.BedrijfsAdres; }
            set
            {
                company.BedrijfsAdres = value;
                RaisePropertyChanged("BedrijfsAdres");
            }
        }

        public string BedrijfsPostcode
        {
            get 
            { 
                return company.BedrijfsPostcode; 
            }
            set 
            { 
                company.BedrijfsPostcode = value;
                RaisePropertyChanged("BedrijfsPostcode");
            }
        }

        public string BedrijfsWijk
        {
            get
            {
                return company.BedrijfsWijk;
            }
            set
            {
                company.BedrijfsWijk = value;
                RaisePropertyChanged("BedrijfsWijk");
            }
        }

        public string BedrijfsGemeente
        {
            get
            {
                return company.BedrijfsGemeente;
            }
            set
            {
                company.BedrijfsGemeente = value;
                RaisePropertyChanged("BedrijfsGemeente");
            }
        }

        public string BedrijfsGemeenteCode
        {
            get
            {
                return company.BedrijfsGemeenteCode;
            }
            set
            {
                company.BedrijfsGemeenteCode = value;
                RaisePropertyChanged("BedrijfsGemeenteCode");
            }
        }

        public decimal BedrijfsLat
        {
            get { return company.BedrijfsLat; }
            set
            {
                company.BedrijfsLat = value;
                RaisePropertyChanged("BedrijfsLat");
            }
        }

        public decimal BedrijfsLon
        {
            get { return company.BedrijfsLon; }
            set
            {
                company.BedrijfsLon = value;
                RaisePropertyChanged("BedrijfsLon");
            }
        }

        public string BedrijfsNummer
        {
            get { return company.BedrijfsNummer; }
            set
            {
                company.BedrijfsNummer = value;
                RaisePropertyChanged("BedrijfsNummer");
            }
        }

        public string totaalAdres
        {
            get { return company.BedrijfsAdres + " " + company.BedrijfsNummer; }
        }

        public Company toCompany()
        {
            return company;
        }
    }
}
