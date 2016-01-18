using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GOINSP.Models;
using GOINSP.Utility;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GOINSP.ViewModel
{
    public class InspectieEditViewModel : ViewModelBase, INavigatableViewModel
    {
        public ICommand UploadButton { get; set; }
        public ICommand RemoveButton { get; set; }
        public ICommand SaveInspection { get; set; }
        public ICommand UpdateInspection { get; set; }
        public ICommand WeergeefBedrijfCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public INavigatableViewModel LastSender { get; set; }

        private Guid dir;

        public ObservableCollection<AccountVM> Inspecteurs { get; set; }
        public ObservableCollection<InspectionTypeVM> TypeInspectie { get; set; }

        private InspectionVM _inspection;
        public InspectionVM Inspection
        {
            get 
            { 
                return _inspection;
            }
            set 
            { 
                _inspection = value;
                RaisePropertyChanged("Inspection");
            }
        }

        private Bijlage _selectedBijlage;
        public Bijlage selectedBijlage
        {
            get
            {
                return _selectedBijlage;
            }
            set
            {
                _selectedBijlage = value;
            }
        }

        private List<Bijlage> _bijlages;
        public List<Bijlage> Bijlages
        {
            get { return _bijlages; }
            set
            {
                _bijlages = value;
                RaisePropertyChanged("Bijlages");
            }
        }

        private string _fileNames;
        public string Filenames
        {
            get { return _fileNames; }
            set
            {
                _fileNames = value;
                RaisePropertyChanged("Filenames");
            }
        }

        private ObservableCollection<NewCompanyVM> bedrijven;
        public ObservableCollection<NewCompanyVM> Bedrijven
        {
            get
            {
                return bedrijven;
            }
            set
            {
                bedrijven = value;
                RaisePropertyChanged("Bedrijven");
            }
        }

        private NewCompanyVM _selectedBedrijf;
        public NewCompanyVM SelectedBedrijf
        {
            get { return _selectedBedrijf; }
            set
            {
                _selectedBedrijf = value;
                RaisePropertyChanged("SelectedBedrijf");
            }
        }

        private AccountVM _selectedUser;
        public AccountVM selectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; }
        }

        private InspectionTypeVM _selectedtype;
        public InspectionTypeVM SelectedType
        {
            get { return _selectedtype; }
            set
            {
                _selectedtype = value;
                RaisePropertyChanged("SelectedType");
            }
        }

        public InspectieEditViewModel()
        {
            Inspection = new InspectionVM();
            _selectedtype = new InspectionTypeVM();
            _selectedBedrijf = new NewCompanyVM();
            _selectedUser = new AccountVM();

            Inspection.date = DateTime.Now;

            UploadButton = new RelayCommand(selectFile);
            RemoveButton = new RelayCommand(removeBijlage);
            UpdateInspection = new RelayCommand(UpdateOrSave);
            WeergeefBedrijfCommand = new RelayCommand(ShowBedrijf);
            CloseWindowCommand = new RelayCommand(CloseWindow);
        }

        private void CloseWindow()
        {
            Inspection = new InspectionVM();
            if (LastSender != null)
                LastSender.Show();
        }

        private void UpdateOrSave()
        {
            if (Inspection.id.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                Update();
            }
            else
            {
                Save();
            }
        }

        private void Save()
        {
            try
            {
                // Set foreign keys
                Inspection.accountVM = selectedUser;
                Inspection.company = SelectedBedrijf;
                Inspection.InspectiontypeVM = SelectedType;
                Inspection.directory = dir;

                // Add to database
                Config.Context.Inspection.Add(Inspection.toInspection());
                Config.Context.SaveChanges();

                // Add to view
                Inspection = new InspectionVM();
                RaisePropertyChanged("Inspections");
            }
            catch (Exception)
            {
                MessageBox.Show("Er is iets fout gegaan, probeer het nogmaals.");
            }

            CloseView();
        }

        public void LoadAddInspection()
        {
            List<Company> companies = Config.Context.Company.ToList();
            Bedrijven = new ObservableCollection<NewCompanyVM>(companies.Select(c => new NewCompanyVM(c)));

            IEnumerable<InspectionType> inspectiontype = Config.Context.Inspectiontype;
            IEnumerable<InspectionTypeVM> inspectiontypeVM = inspectiontype.Select(a => new InspectionTypeVM(a));
            TypeInspectie = new ObservableCollection<InspectionTypeVM>(inspectiontypeVM);

            dir = Inspection.directory;
            searchBijlage();

            IEnumerable<Account> inspecteurs = Config.Context.Account;
            IEnumerable<AccountVM> accountVM = inspecteurs.Select(c => new AccountVM(c)).Where(x => x.AccountRights == Models.Account.Rights.ExterneInspecteur || x.AccountRights == Models.Account.Rights.InterneInspecteur);
            Inspecteurs = new ObservableCollection<AccountVM>(accountVM);
            RaisePropertyChanged("Inspecteurs");
        }

        public void removeBijlage()
        {
            if (selectedBijlage != null && selectedBijlage.FileName != null)
            {
                String map = dir.ToString();
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, @"GoInspGroepB/" + map);
                DirectoryInfo d = new DirectoryInfo(specificFolder);


                foreach (var bestand in d.GetFiles())
                {
                    string fileName = Path.GetFileName(bestand.FullName);

                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine(selectedBijlage.FileName);
                    Console.WriteLine("");


                    if (fileName == selectedBijlage.FileName)
                    {
                        File.Delete(bestand.FullName);
                        Console.WriteLine("Bestand verwijderd: " + specificFolder + "/" + selectedBijlage.FileName);
                    }

                }

                searchBijlage();
            }
            else
            {
                MessageBox.Show("U heeft geen bijlage geselecteerd.");
            }
        }

        public void searchBijlage()
        {
            Bijlages = new List<Bijlage>();

            string map = dir.ToString();

            if (map != "00000000-0000-0000-0000-000000000000")
            {
                List<Bijlage> templist = new List<Bijlage>();
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "GoInspGroepB/" + map);
                DirectoryInfo d = new DirectoryInfo(specificFolder);

                foreach (var file in d.GetFiles())
                {
                    string tmpExtension = "";
                    switch (Path.GetExtension(file.ToString()).ToLower())
                    {
                        case ".jpg":
                            tmpExtension = "JPG (*.jpg)";
                            break;
                        case ".jpeg":
                            tmpExtension = "JPG (*.jpeg)";
                            break;
                        case ".png":
                            tmpExtension = "PNG (*.png)";
                            break;
                        case ".gif":
                            tmpExtension = "GIF (*.gif)";
                            break;
                        case ".mp3":
                            tmpExtension = "MP3 (*.mp3)";
                            break;
                        case ".mp4":
                            tmpExtension = "MP4 (*.mp4)";
                            break;
                        case ".mov":
                            tmpExtension = "MOV (*.mov)";
                            break;
                        default:
                            break;
                    }

                    Bijlage tmp = new Bijlage();
                    tmp.FileName = file.ToString();
                    tmp.Extension = tmpExtension;

                    templist.Add(tmp);
                }

                Bijlages = templist;
            }

            RaisePropertyChanged("Bijlages");
            Console.WriteLine("OKAY!");
        }

        private void Update()
        {
            try
            {
                Inspection.accountVM = selectedUser;
                Inspection.company = SelectedBedrijf;
                Inspection.InspectiontypeVM = SelectedType;

                Config.Context.Entry(Inspection.toInspection()).State = System.Data.Entity.EntityState.Modified;
                Config.Context.SaveChanges();

                RaisePropertyChanged("Inspections");

            }
            catch (Exception)
            {
                MessageBox.Show("Er is iets fout gegaan, probeer het nogmaals.");
            }

            CloseView();
        }

        private void ShowBedrijf()
        {
            BedrijfInfo window = new BedrijfInfo(SelectedBedrijf.ID);
        }

        public void selectFile()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|MP3 Files (*.mp3)|*.mp3|MP4 Files (*.mp4)|*.mp4|MOV Files (*.mov)|*.mov";

            dlg.Multiselect = true;

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string filenames = "";

                if (Inspection.directory.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    dir = Guid.NewGuid();
                }
                else
                {
                    dir = Inspection.directory;
                }


                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Combine the base folder with your specific folder....
                string specificFolder = Path.Combine(folder, "GoInspGroepB\\" + dir);
                string naam;

                // Check if folder exists and if not, create it
                if (!Directory.Exists(specificFolder))
                    Directory.CreateDirectory(specificFolder);

                foreach (String file in dlg.FileNames)
                {
                    Console.WriteLine(file);
                    filenames += file + ", ";
                    naam = Path.GetFileName(file);

                    if (!File.Exists(specificFolder + "\\" + naam))
                        File.Copy(file, specificFolder + "\\" + naam);
                    else
                        MessageBox.Show("Dit bestand zit al gekoppelt.");
                }

                Filenames = filenames;
            }


            searchBijlage();
        }

        public void Show(INavigatableViewModel sender = null)
        {
            LastSender = sender;
            EditInspection view = new EditInspection();
            view.Show();
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }
    }
}
