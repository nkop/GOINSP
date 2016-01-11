using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace GOINSP.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AccountManagementVM>();
            SimpleIoc.Default.Register<OpenDataImportViewModel>();
            SimpleIoc.Default.Register<ForgottenPasswordVM>();
            SimpleIoc.Default.Register<InspectionViewModel>();
            SimpleIoc.Default.Register<InspectionSpecsViewModel>();
            SimpleIoc.Default.Register<AddCompanyViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public AccountManagementVM Account
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountManagementVM>();
            }
        }

        public OpenDataImportViewModel OpenDataImport
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OpenDataImportViewModel>();
            }
        }

        public ForgottenPasswordVM ForgottenPassword
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ForgottenPasswordVM>();
            }
        }

        public InspectionViewModel Inspection
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InspectionViewModel>();
            }
        }

        public AddCompanyViewModel AddCompany
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddCompanyViewModel>();
            }
        }

        public InspectionSpecsViewModel InspectionSpecs
        {
            get
            {
                return new InspectionSpecsViewModel();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}