using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GOINSP.Models;
using GOINSP.Utility;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    public class InspectionSpecsViewModel : ViewModelBase, INavigatableViewModel
    {
        private Location mapPoint;
        public Location MapPoint
        {
            get
            {
                return mapPoint;
            }
            set
            {
                mapPoint = value;
                RaisePropertyChanged("MapPoint");
            }
        }

        private InspectionVM inspectionSpecs;
        public InspectionVM InspectionSpecs
        {
            get
            {
                return inspectionSpecs;
            }
            set
            {
                inspectionSpecs = value;
                FillPoint();
                RaisePropertyChanged("InspectionSpecs");
            }
        }

        public ICommand OpenQuestionnaireCommand { get; set; }
        public ICommand OpenEditInspection { get; set; }

        public void FillPoint()
        {
            MapPoint = null;

            if(InspectionSpecs.company != null)
            {
                MapPoint = new Location((double)InspectionSpecs.company.BedrijfsLat, (double)InspectionSpecs.company.BedrijfsLon);
            }
        }

        public InspectionSpecsViewModel()
        {
            OpenQuestionnaireCommand = new RelayCommand(OpenQuestionnaire);
            OpenEditInspection = new RelayCommand(OpenEditInspectionWindow);
        }

        public void SetInspection(Guid id)
        {
            IEnumerable<Inspection> inspectie = Config.Context.Inspection;
            InspectionSpecs = inspectie.Select(a => new InspectionVM(a)).Where(p => p.id == id).First();
        }
          
        public void Show(INavigatableViewModel sender = null)
        {
            InspectionSpecs window = new InspectionSpecs();
            window.Show();
        }

        public void OpenQuestionnaire()
        {
            if(inspectionSpecs.questionnaire == null)
            {
                QuestionListVM questionListVM = ServiceLocator.Current.GetInstance<QuestionListVM>();
                questionListVM.CreateQuestionnaireList();
                questionListVM.BoundInspection = InspectionSpecs;
                questionListVM.Show(this);
            }
            else
            {
                QuestionnaireAnswerViewModel questionnaireAnswerViewModel = ServiceLocator.Current.GetInstance<QuestionnaireAnswerViewModel>();
                questionnaireAnswerViewModel.QuestionnaireVM = inspectionSpecs.questionnaire;
                questionnaireAnswerViewModel.QuestionnaireVM.CheckConditionBoundQuestions();
                questionnaireAnswerViewModel.Show(this);
            }
        }

        public void OpenEditInspectionWindow()
        {
            EditInspection window = new EditInspection();
            window.Show();
            CloseView();
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }
    }
}
