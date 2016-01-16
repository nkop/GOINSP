using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GOINSP.Models;
using GOINSP.Utility;
using Microsoft.Practices.ServiceLocation;

namespace GOINSP.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            Config.Context = new Context();

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
            SimpleIoc.Default.Register<ManagInfoVM>();
            SimpleIoc.Default.Register<TDataViewModel>();
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

        public ManagInfoVM ManagInfoVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ManagInfoVM>();
            }
        }

        public CompanyCrudVM CompanyCrud
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CompanyCrudVM>();
            }
        }

        public TDataViewModel TDataViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TDataViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}