using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GOINSP.Models;
using GOINSP.Utility;
using GOINSP.ViewModel.QuestionnaireViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    public class QuestionnaireAnswerViewModel : ViewModelBase, INavigatableViewModel
    {
        public ICommand SaveQuestionnaireCommand { get; set; }
        public ICommand EditQuestionnaireCommand { get; set; }
        public ICommand DeleteQuestionnaireCommand { get; set; }
        public ICommand AddImageCommand { get; set; }
        public ICommand ViewImageCommand { get; set; }
        public ICommand DeleteImageCommand { get; set; }

        private QuestionnaireVM questionnaireVM;
        public QuestionnaireVM QuestionnaireVM {
            get
            {
                return questionnaireVM;
            }
            set
            {
                questionnaireVM = value;
                questionnaireVM.QuestionnaireCollection = new ObservableCollection<QuestionVM>(questionnaireVM.QuestionnaireCollection.OrderBy(xy => xy.ListNumber));
                RaisePropertyChanged("QuestionnaireVM");
            } 
        }

        public QuestionnaireAnswerViewModel()
        {
            SaveQuestionnaireCommand = new RelayCommand(SaveQuestionnaire);
            EditQuestionnaireCommand = new RelayCommand(EditQuestionnaire);
            DeleteQuestionnaireCommand = new RelayCommand(DeleteQuestionnaire);
            AddImageCommand = new RelayCommand<QuestionVM>(AddImage);
            ViewImageCommand = new RelayCommand<QuestionVM>(ViewImage);
            DeleteImageCommand = new RelayCommand<QuestionVM>(DeleteImage);
        }

        private void DeleteImage(QuestionVM obj)
        {
            try
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "GoInspGroepB\\QuestionImages");
                File.Delete(specificFolder + @"/" + obj.QuestionID.ToString() + ".jpg");
                obj.ImageAddable = System.Windows.Visibility.Visible;
                obj.ImageViewable = System.Windows.Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, Iets ging mis.");
            }
        }

        private void ViewImage(QuestionVM obj)
        {
            try
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "GoInspGroepB\\QuestionImages");
                System.Diagnostics.Process.Start(specificFolder + @"/" + obj.QuestionID.ToString()+".jpg");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, Iets ging mis.");
            }
        }

        private void AddImage(QuestionVM obj)
        {
            if(obj.QuestionID.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                // Set filter for file extension and default file extension 
                dlg.DefaultExt = ".jpg";
                dlg.Filter = "JPG Files (*.jpg)|*.jpg";

                dlg.Multiselect = false;

                // Display OpenFileDialog by calling ShowDialog method 
                Nullable<bool> result = dlg.ShowDialog();


                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    string filenames = "";

                    string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                    // Combine the base folder with your specific folder....
                    string specificFolder = Path.Combine(folder, "GoInspGroepB\\QuestionImages");
                    string naam;

                    // Check if folder exists and if not, create it
                    if (!Directory.Exists(specificFolder))
                        Directory.CreateDirectory(specificFolder);

                    foreach (String file in dlg.FileNames)
                    {
                        Console.WriteLine(file);
                        filenames += file + ", ";
                        naam = obj.QuestionID.ToString();

                        if (!File.Exists(specificFolder + "\\" + naam + ".jpg"))
                        {
                            File.Copy(file, specificFolder + "\\" + naam + ".jpg");
                            obj.ImageAddable = System.Windows.Visibility.Collapsed;
                            obj.ImageViewable = System.Windows.Visibility.Visible;
                        }
                        else
                            MessageBox.Show("Dit bestand zit al gekoppelt.");
                    }
                }
            }
        }

        private void DeleteQuestionnaire()
        {
            DialogResult result = MessageBox.Show("Weet u zeker dat de vragenlijst verwijdert moet worden? Dit kan niet ongedaan gemaakt worden.", "Let op!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (QuestionnaireVM != null)
                {
                    Config.Context.Questionnaire.Remove(questionnaireVM.GetQuestionnaire());
                    Config.Context.SaveChanges();
                    CloseView();
                }
            }
        }

        private void SaveQuestionnaire()
        {
            if (QuestionnaireVM != null)
            {
                QuestionnaireVM.Update();
                CloseView();
            }
        }

        private void EditQuestionnaire()
        {
            if(QuestionnaireVM != null)
            {
                QuestionTemplateVM questionTemplateVM = ServiceLocator.Current.GetInstance<QuestionTemplateVM>();
                questionTemplateVM.SetQuestionnaire(QuestionnaireVM);
                questionTemplateVM.Show(this);
                CloseView();
            }
        }

        public void Show(INavigatableViewModel sender = null)
        {
            QuestionnaireAnswerView questionnaireAnswerView = new QuestionnaireAnswerView();
            questionnaireAnswerView.Show();
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }
    }
}
