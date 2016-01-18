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
    public class InspectionViewModel : ViewModelBase, INavigatableViewModel
    {
        public ObservableCollection<InspectionVM> Inspections { get; set; }

        private List<Bijlage> _bijlages;
        private Bijlage _selectedBijlage;

        private Guid dir;
        public ObservableCollection<InspectionTypeVM> TypeInspectie { get; set; }
        private ObservableCollection<NewCompanyVM> bedrijven; 
        public ObservableCollection<NewCompanyVM> Bedrijven { 
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
        public ObservableCollection<InspectionVM> BedrijfInspecties { get; set; }
        public ObservableCollection<AccountVM> Inspecteurs { get; set; }

        private Visibility addInspectionVisibility;
        public Visibility AddInspectionVisibility
        {
            get
            {
                return addInspectionVisibility;
            }
            set
            {
                addInspectionVisibility = value;
                RaisePropertyChanged("AddInspectionVisibility");
            }
        }


        public ICommand AddInspection { get; set; }
        public ICommand SaveInspection { get; set; }
        public ICommand UpdateInspection { get; set; }
        public ICommand WeergeefBedrijfCommand { get; set; }

        public ICommand UploadButton { get; set; }

        public ICommand RemoveButton { get; set; }

        private InspectionVM _newInspection;

        private InspectionVM _selectedInspection;

        private string _searchQuota { get; set; }

        private NewCompanyVM _selectedBedrijf;
        private string _fileNames;
        private AccountVM _selectedUser;
        private InspectionTypeVM _selectedtype;

        public Guid InspectionID;

        public InspectionViewModel()
        {
            IEnumerable<Inspection> inspectie = Config.Context.Inspection;

            IEnumerable<InspectionVM> inspectionVM  = null; 
            
            if (Config.Rechten == Models.Account.Rights.ExterneInspecteur)
            {
                List<Inspection> AllInspections = inspectie.ToList();
                List<Inspection> InspectionsForUser = inspectie.Where(x => x.inspector.id == Config.GebruikerID).ToList();
                List<Inspection> inspections = new List<Inspection>();
                List<Company> Companies = InspectionsForUser.Select(x => x.company).ToList();
                foreach(Company company in Companies)
                {
                    InspectionsForUser.AddRange(AllInspections.Where(x => x.company == company).ToList().Distinct());
                }

                InspectionsForUser = InspectionsForUser.Distinct().ToList();

                inspectionVM = InspectionsForUser.Select(a => new InspectionVM(a));
            }
            else if (Config.Rechten == Models.Account.Rights.InterneInspecteur)
            {
                List<Inspection> AllInspections = inspectie.ToList();
                List<Inspection> InspectionsForUser = inspectie.Where(x => x.inspector.id == Config.GebruikerID).ToList();
                inspectionVM = InspectionsForUser.Select(a => new InspectionVM(a));
            }
            else
            {
                inspectionVM = inspectie.Select(a => new InspectionVM(a));
            }

            AddInspectionVisibility = Visibility.Collapsed;
            if (Config.Rechten == Account.Rights.Manager || Config.Rechten == Account.Rights.Administrator)
            {
                AddInspectionVisibility = Visibility.Visible;
            }
            
            Inspections = new ObservableCollection<InspectionVM>(inspectionVM);
            RaisePropertyChanged("Inspections");

            IEnumerable<InspectionType> inspectiontype = Config.Context.Inspectiontype;
            IEnumerable<InspectionTypeVM> inspectiontypeVM = inspectiontype.Select(a => new InspectionTypeVM(a));
            TypeInspectie = new ObservableCollection<InspectionTypeVM>(inspectiontypeVM);
            RaisePropertyChanged("TypeInspectie");

            AddInspection = new RelayCommand(Add);
            SaveInspection = new RelayCommand(Save);
            UpdateInspection = new RelayCommand(Update);
            WeergeefBedrijfCommand = new RelayCommand(ShowBedrijf);
            UploadButton = new RelayCommand(selectFile);
            RemoveButton = new RelayCommand(removeBijlage);

            _newInspection = new InspectionVM();
            _selectedInspection = new InspectionVM();
            _selectedtype = new InspectionTypeVM();
            _selectedBedrijf = new NewCompanyVM();
            _selectedUser = new AccountVM();

            newInspection.date = DateTime.Now;

        }

        public string SearchQuota
        {
            get { return _searchQuota; }
            set
            {
                _searchQuota = value;
                RaisePropertyChanged("SearchQuota");
                Search();
            }
        }

        public void LoadAddInspection()
        {
            List<Company> companies = Config.Context.Company.ToList();
            Bedrijven = new ObservableCollection<NewCompanyVM>(companies.Select(c => new NewCompanyVM(c)));

            IEnumerable<Account> inspecteurs = Config.Context.Account;
            IEnumerable<AccountVM> accountVM = inspecteurs.Select(c => new AccountVM(c)).Where(x => x.AccountRights == Models.Account.Rights.ExterneInspecteur || x.AccountRights == Models.Account.Rights.InterneInspecteur);
            Inspecteurs = new ObservableCollection<AccountVM>(accountVM);
            RaisePropertyChanged("Inspecteurs");
        }

        public InspectionVM newInspection
        {
            get { return _newInspection; }
            set { _newInspection = value; }
        }

        public InspectionVM selectedInspection
        {
            get { return _selectedInspection; }
            set
            {
                _selectedInspection = value;
                OpenInspection();
            }
        }

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

        public InspectionVM UpdateSelectedInspection
        {
            get { return _selectedInspection; }
            set
            {
                _selectedInspection = value;
                OpenInspection(false);
            }
        }

        public AccountVM selectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; }
        }

        public NewCompanyVM SelectedBedrijf
        {
            get { return _selectedBedrijf; }
            set
            {
                _selectedBedrijf = value;
                RaisePropertyChanged("SelectedBedrijf");
            }
        }

        public List<Bijlage> Bijlages
        {
            get { return _bijlages; }
            set
            {
                _bijlages = value;
                RaisePropertyChanged("Bijlages");
            }
        }

        public string Filenames
        {
            get { return _fileNames; }
            set
            {
                _fileNames = value;
                RaisePropertyChanged("Filenames");
            }
        }

        public InspectionTypeVM SelectedType
        {
            get { return _selectedtype; }
            set
            {
                _selectedtype = value;
                RaisePropertyChanged("SelectedType");
            }
        }

        private void Add()
        {
            AddInspection window = new AddInspection();
            window.Show();
            LoadAddInspection();
        }

        private void Save()
        {
            try
            {
                // Set foreign keys
                _newInspection.accountVM = selectedUser;
                _newInspection.company = SelectedBedrijf;
                _newInspection.InspectiontypeVM = SelectedType;
                _newInspection.directory = dir;

                // Add to database
                Config.Context.Inspection.Add(_newInspection.toInspection());
                Config.Context.SaveChanges();

                // Add to view
                Inspections.Add(_newInspection);
                newInspection = new InspectionVM();
                RaisePropertyChanged("Inspections");

                CloseView();
            }
            catch (Exception)
            {
                MessageBox.Show("Er is iets fout gegaan, probeer het nogmaals.");
            }
        }

        private void Update()
        {
            try
            {
                _selectedInspection.accountVM = selectedUser;
                _selectedInspection.company = SelectedBedrijf;
                _selectedInspection.InspectiontypeVM = SelectedType;

                Config.Context.Entry(selectedInspection.toInspection()).State = System.Data.Entity.EntityState.Modified;
                Config.Context.SaveChanges();

                UpdateSelectedInspection = selectedInspection;
                RaisePropertyChanged("UpdateSelectedInspection");
                RaisePropertyChanged("Inspections");

                CloseView();
            }
            catch (Exception)
            {
                MessageBox.Show("Er is iets fout gegaan, probeer het nogmaals.");
            }
        }

        private void Search()
        {
            if (SearchQuota.Length >= 0)
            {
                List<Models.Inspection> tempInspection = Config.Context.Inspection.ToList();
                List<InspectionVM> tempInspectionVM = new List<InspectionVM>();
                foreach (Models.Inspection item in tempInspection)
                {
                    tempInspectionVM.Add(new InspectionVM(item));
                }
                Inspections.Clear();
                foreach (InspectionVM item in tempInspectionVM)
                {
                    if (item.name.ToLower().Contains(SearchQuota.ToLower()) || item.company.BedrijfsAdres.ToLower().Contains(SearchQuota.ToLower()) || item.company.BedrijfsPostcode.ToLower().Contains(SearchQuota.ToLower()))
                    {
                        Inspections.Add(item);
                    }
                }
                RaisePropertyChanged("Inspections");
            }
        }

        public void searchBijlage()
        {
            Bijlages = new List<Bijlage>();

            string map = selectedInspection.directory.ToString();

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
            else
            {
                //MessageBox.Show("Er zijn geen bijlagen gevonden");
            }

            Console.WriteLine("OKAY!");
        }

        public void OpenInspection(bool show = true)
        {
            if (_selectedInspection != null)
            {
                InspectionSpecsViewModel InspectionVMInstance = ServiceLocator.Current.GetInstance<InspectionSpecsViewModel>();
                InspectionVMInstance.SetInspection(_selectedInspection.id);

                if (show)
                    InspectionVMInstance.Show(this);
            }
        }

        private void ShowBedrijf()
        {
            BedrijfInfo window = new BedrijfInfo(SelectedBedrijf.ID);
        }

        public void Show(INavigatableViewModel sender = null)
        {
            throw new NotImplementedException();
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView2")
            );
        }

        public void removeBijlage()
        {
            if (selectedBijlage.FileName != null || selectedBijlage != null)
            {
                String map = _selectedInspection.directory.ToString();
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, @"GoInspGroepB/" + map);
                DirectoryInfo d = new DirectoryInfo(specificFolder);


                foreach (var bestand in d.GetFiles())
                {
                    string fileName = Path.GetFileName (bestand.FullName);

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

                if (selectedInspection == null)
                {
                    dir = Guid.NewGuid();
                }
                else
                {
                    dir = selectedInspection.directory;
                }
                

                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Combine the base folder with your specific folder....
                string specificFolder = Path.Combine(folder, "GoInspGroepB/" + dir);
                string naam;

                // Check if folder exists and if not, create it
                if (!Directory.Exists(specificFolder))
                    Directory.CreateDirectory(specificFolder);

                foreach (String file in dlg.FileNames)
                {
                    Console.WriteLine(file);
                    filenames += file + ", ";
                    naam = Path.GetFileName(file);

                    File.Copy(file, specificFolder + "/" + naam);

                }

                Filenames = filenames;
            }


            if (selectedInspection != null)
            {
                searchBijlage();
            }

        }
    }
}
