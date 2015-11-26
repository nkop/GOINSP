using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GOINSP.Utility;
using System;
using System.Collections.Generic;
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

        public OpenDataImportViewModel()
        {
            StartImportCommand = new RelayCommand<string>(StartImport);
            ProgressBarPercentage = 0;
        }

        public void StartImport(string dataType)
        {
            IImport importer = ImportFactory.GetFactory(dataType);

            Progress<ImportProgresValues> progressIndicator = new Progress<ImportProgresValues>(ReportProgress);

            Task task = Task.Factory.StartNew(() =>
            {
                importer.Import(progressIndicator);
            });
        }

        private void ReportProgress(ImportProgresValues progress)
        {
            switch(progress.Status)
            {
                case ImportProgresValues.ProgressStatus.downloading:
                    ProgressBarPercentage = 0;
                    ProgressLabelText = "Downloading records...";
                    break;
                case ImportProgresValues.ProgressStatus.removing:
                    ProgressBarPercentage = 0;
                    ProgressLabelText = "Cleaning old records...";
                    break;
                case ImportProgresValues.ProgressStatus.inserting:
                    ProgressBarPercentage = (int)(((float)progress.MinProgress/(float)progress.MaxProgress)*100);
                    ProgressLabelText = "Importing record " + progress.MinProgress + " of " + progress.MaxProgress+"...";
                    break;
                default:
                    break;
            }
        }
    }
}
