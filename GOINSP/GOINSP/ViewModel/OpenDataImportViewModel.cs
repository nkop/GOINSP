using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GOINSP.Models;
using GOINSP.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    public class OpenDataImportViewModel : ViewModelBase
    {
        public ICommand StartImportCommand {get; set;}
        private int progressBarPercentage;
        public int ProgressBarPercentage
        {
            get
            {
                return progressBarPercentage;
            }
            set
            {
                progressBarPercentage = value;
                RaisePropertyChanged("ProgressBarPercentage");
            }
        }

        private string progressLabelText;
        public string ProgressLabelText
        {
            get
            {
                return progressLabelText;
            }
            set
            {
                progressLabelText = value;
                RaisePropertyChanged("ProgressLabelText");
            }
        }

        private bool buttonsEnabled;
        public bool ButtonsEnabled
        {
            get
            {
                return buttonsEnabled;
            }
            set
            {
                buttonsEnabled = value;
                RaisePropertyChanged("ButtonsEnabled");
            }
        }

        public OpenDataImportViewModel()
        {
            StartImportCommand = new RelayCommand<string>(StartImport);
            ProgressBarPercentage = 0;
            ButtonsEnabled = true;
        }

        public void StartImport(string dataType)
        {
            IImport importer = ImportFactory.GetFactory(dataType);

            Progress<ImportProgressValues> progressIndicator = new Progress<ImportProgressValues>(ReportProgress);

            ButtonsEnabled = false;
            Task task = Task.Factory.StartNew(() =>
            {
                importer.Import(progressIndicator);
            });

            task.ContinueWith((doneTask) =>
            {
                ProgressLabelText = "Klaar met importeren";
                ButtonsEnabled = true;
            });
        }

        private void ImportComplete()
        {

        }

        private void ReportProgress(ImportProgressValues progress)
        {
            switch(progress.Status)
            {
                case ImportProgressValues.ProgressStatus.downloading:
                    ProgressBarPercentage = 0;
                    ProgressLabelText = "Downloaden van nieuwe velden";
                    break;
                case ImportProgressValues.ProgressStatus.removing:
                    ProgressBarPercentage = 0;
                    ProgressLabelText = "Opschonen van oude velden";
                    break;
                case ImportProgressValues.ProgressStatus.inserting:
                    ProgressBarPercentage = (int)(((float)progress.MinProgress/(float)progress.MaxProgress)*100);
                    ProgressLabelText = "Veld " + progress.MinProgress + " van " + progress.MaxProgress+" aan het importeren";
                    break;
                case ImportProgressValues.ProgressStatus.saving:
                    ProgressBarPercentage = 100;
                    ProgressLabelText = "Velden opslaan";
                    break;
                default:
                    break;
            }
        }
    }

    public class ListViewData
    {
        public string key { get; set; }

        public ListViewData(string key)
        {
            this.key = key;
        }
    }
}
