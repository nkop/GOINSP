﻿using GalaSoft.MvvmLight;
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

        public OpenDataImportViewModel()
        {
            StartImportCommand = new RelayCommand<string>(StartImport);
            ProgressBarPercentage = 0;
        }

        public void StartImport(string dataType)
        {
            IImport importer = ImportFactory.GetFactory(dataType);

            Progress<int> progressIndicator = new Progress<int>(ReportProgress);

            Task task = Task.Factory.StartNew(() =>
            {
                importer.Import(progressIndicator);
            });
        }

        private void ReportProgress(int progress)
        {
            ProgressBarPercentage = progress;
        }
    }
}
