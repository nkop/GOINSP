using GalaSoft.MvvmLight;
using GOINSP.Models;
using GOINSP.Models.QuestionnaireModels;
using GOINSP.ViewModel.QuestionnaireViewModels;
using GOINSP.XamlToHtmlParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel
{
    public class InspectionVM : ViewModelBase
    {
        private Models.Inspection inspection;

        public InspectionVM()
        {
            this.inspection = new Models.Inspection();
        }

        public InspectionVM(Models.Inspection inspection)
        {
            this.inspection = inspection;
        }

        public Guid id
        {
            get { return inspection.id; }
        }

        public string name
        {
            get { return inspection.name; }
            set
            {
                inspection.name = value;
                RaisePropertyChanged("name");
            }
        }

        public DateTime date
        {
            get { return inspection.date; }
            set
            {
                inspection.date = value;
                RaisePropertyChanged("date");
            }
        }

        public string description
        {
            get { return inspection.description; }
            set
            {
                inspection.description = value;
                RaisePropertyChanged("description");
            }
        }

        public byte[] image
        {
            get { return inspection.image; }
            set
            {
                inspection.image = value;
                RaisePropertyChanged("image");
            }
        }

        public Inspection toInspection()
        {
            return inspection;
        }

        public NewCompanyVM company
        {
            get
            {
                if (inspection.company == null)
                    return null;
                return new NewCompanyVM(inspection.company);
            }
            set { inspection.company = value.toCompany(); }
        }

        public AccountVM accountVM
        {
            get
            {
                if (inspection.inspector == null)
                    return null;
                return new AccountVM(inspection.inspector);
            }
            set { inspection.inspector = value.ToAccount(); }
        }

        public InspectionTypeVM InspectiontypeVM
        {
            get
            {
                if (inspection.inspectiontype == null)
                    return null;
                return new InspectionTypeVM(inspection.inspectiontype);
            }
            set { inspection.inspectiontype = value.toInspectionType(); }
        }

        public QuestionnaireVM questionnaire
        {
            get {
                if (inspection.questionnaire == null)
                    return null;
                return new QuestionnaireVM(inspection.questionnaire);
            }
            set { inspection.questionnaire = value.GetQuestionnaire(); }
        }

    }
}
