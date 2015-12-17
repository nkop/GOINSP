﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GMap.NET;
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

        private ObservableCollection<ListViewData> testCollection;
        public ObservableCollection<ListViewData> TestCollection
        {
            get { return testCollection; }
            set
            {
                testCollection = value;
                RaisePropertyChanged("TestCollection");
            }
        }

        Context context;

        public OpenDataImportViewModel()
        {
            StartImportCommand = new RelayCommand<string>(StartImport);
            ProgressBarPercentage = 0;
            ButtonsEnabled = true;

            context = new Context();

            TestCollection = new ObservableCollection<ListViewData>();
            TestCollection.Add(new ListViewData("6ul3wIbTzn5LT7yjxpI2~uLiY5U0SRIjJ5O_VS-Y_YQ~AndIPnWA1UenaMKsoVD0GDdau2QeDLw0Eh68IFPO_vSwNk1O_tHP-5NPH5nSp4s9"));
            TestCollection.Add(new ListViewData("6ul3wIbTzn5LT7yjxpI2~uLiY5U0SRIjJ5O_VS-Y_YQ~AndIPnWA1UenaMKsoVD0GDdau2QeDLw0Eh68IFPO_vSwNk1O_tHP-5NPH5nSp4s9"));
            TestCollection.Add(new ListViewData("6ul3wIbTzn5LT7yjxpI2~uLiY5U0SRIjJ5O_VS-Y_YQ~AndIPnWA1UenaMKsoVD0GDdau2QeDLw0Eh68IFPO_vSwNk1O_tHP-5NPH5nSp4s9"));
            TestCollection.Add(new ListViewData("6ul3wIbTzn5LT7yjxpI2~uLiY5U0SRIjJ5O_VS-Y_YQ~AndIPnWA1UenaMKsoVD0GDdau2QeDLw0Eh68IFPO_vSwNk1O_tHP-5NPH5nSp4s9"));
            TestCollection.Add(new ListViewData("6ul3wIbTzn5LT7yjxpI2~uLiY5U0SRIjJ5O_VS-Y_YQ~AndIPnWA1UenaMKsoVD0GDdau2QeDLw0Eh68IFPO_vSwNk1O_tHP-5NPH5nSp4s9"));
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

    public class ListViewData
    {
        public string key { get; set; }

        public ListViewData(string key)
        {
            this.key = key;
        }
    }
}
