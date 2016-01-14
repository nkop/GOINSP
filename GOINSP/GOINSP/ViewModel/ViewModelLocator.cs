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
            SimpleIoc.Default.Register<LocationPickerVM>();
            SimpleIoc.Default.Register<QuestionTemplateVM>();
            SimpleIoc.Default.Register<QuestionListVM>();
            SimpleIoc.Default.Register<InspectionViewModel>();
            SimpleIoc.Default.Register<InspectionSpecsViewModel>();
            SimpleIoc.Default.Register<CompanyCrudVM>();
            SimpleIoc.Default.Register<QuestionnaireAnswerViewModel>();
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

        public LocationPickerVM LocationPickerModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LocationPickerVM>();
            }
        }

        public QuestionTemplateVM QuestionTemplate
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuestionTemplateVM>();
            }
        }

        public QuestionListVM QuestionList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuestionListVM>();
            }
        }

        public InspectionViewModel Inspection
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InspectionViewModel>();
            }
        }

        public InspectionSpecsViewModel InspectionSpecs
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InspectionSpecsViewModel>();
            }
        }

        public QuestionnaireAnswerViewModel QuestionnaireAnswerViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuestionnaireAnswerViewModel>();
            }
        }

        public CompanyCrudVM CompanyCrud
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CompanyCrudVM>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}