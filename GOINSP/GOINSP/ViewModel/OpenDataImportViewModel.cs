using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GMap.NET;
using GOINSP.Models;
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

        
        PointLatLng Point = new PointLatLng(51.6790508905935, 5.0618044538692);

        Context context;

        public OpenDataImportViewModel()
        {
            StartImportCommand = new RelayCommand<string>(StartImport);
            ProgressBarPercentage = 0;
            ButtonsEnabled = true;

            context = new Context();


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
                ProgressLabelText = "Done importing!";
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
                    ProgressLabelText = "Downloading records...";
                    break;
                case ImportProgressValues.ProgressStatus.removing:
                    ProgressBarPercentage = 0;
                    ProgressLabelText = "Cleaning old records...";
                    break;
                case ImportProgressValues.ProgressStatus.inserting:
                    ProgressBarPercentage = (int)(((float)progress.MinProgress/(float)progress.MaxProgress)*100);
                    ProgressLabelText = "Importing record " + progress.MinProgress + " of " + progress.MaxProgress+"...";
                    break;
                default:
                    break;
            }
        }
    }
}
