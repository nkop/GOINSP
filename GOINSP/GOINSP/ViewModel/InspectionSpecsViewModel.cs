using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GOINSP.Models;
using GOINSP.Utility;
using GOINSP.ViewModel.QuestionnaireViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Diagnostics;
using System.IO;
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
        private InspectionViewModel inspectionviewmodel;

        private Location mapPoint;
        private List<Bijlage> _filsList;
        private Bijlage _selectedBijlage;
        public string dir;
        
        public List<Bijlage> filesList
        {
            get
            {
                return _filsList;
            }
            set
            {
                _filsList = value;
                RaisePropertyChanged("filesList");
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
                openBijlage();
            }
        }
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

        private Visibility editInspectionVisibility;
        public Visibility EditInspectionVisibility
        {
            get
            {
                return editInspectionVisibility;
            }
            set
            {
                editInspectionVisibility = value;
                RaisePropertyChanged("AddInspectionVisibility");
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
                CheckPermissions();
                RaisePropertyChanged("InspectionSpecs");
            }
        }

        public ICommand OpenQuestionnaireCommand { get; set; }
        public ICommand OpenEditInspection { get; set; }
        public ICommand PrintRapport { get; set; }
        public ICommand OpenAfvalCommand { get; set; }

        public void FillPoint()
        {
            MapPoint = null;

            if(InspectionSpecs.company != null)
            {
                MapPoint = new Location((double)InspectionSpecs.company.BedrijfsLat, (double)InspectionSpecs.company.BedrijfsLon);
            }
        }

        public void CheckPermissions()
        {
            EditInspectionVisibility = Visibility.Visible;
            if(InspectionSpecs.accountVM != null && Config.Rechten == Account.Rights.ExterneInspecteur)
            {
                if(InspectionSpecs.accountVM.id != Config.GebruikerID)
                {
                    EditInspectionVisibility = Visibility.Collapsed;
                }
            }
        }

        public InspectionSpecsViewModel()
        {
            inspectionviewmodel = ServiceLocator.Current.GetInstance<InspectionViewModel>();

            OpenQuestionnaireCommand = new RelayCommand(OpenQuestionnaire);
            OpenEditInspection = new RelayCommand(OpenEditInspectionWindow);
            PrintRapport = new RelayCommand(generatePDF);
            OpenAfvalCommand = new RelayCommand(OpenAfval);
        }

        public void OpenAfval()
        {
            TDataViewModel tDataViewModel = ServiceLocator.Current.GetInstance<TDataViewModel>();
            tDataViewModel.GMCode = InspectionSpecs.company.BedrijfsGemeenteCode;
            tDataViewModel.Show(this);
        }

        public void searchBijlagen()
        {
            filesList = new List<Bijlage>();
            dir = inspectionSpecs.directory.ToString();

            if (dir != "00000000-0000-0000-0000-000000000000")
            {
                List<Bijlage> templist = new List<Bijlage>();
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "GoInspGroepB/" + dir);
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

                filesList = templist;
            }
            else
            {
                MessageBox.Show("Er zijn geen bijlagen gevonden");
            }
            
        }

        public void openBijlage()
        {
            if (_selectedBijlage != null)
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "GoInspGroepB/" + dir);
                System.Diagnostics.Process.Start(specificFolder + @"/" + _selectedBijlage.FileName);
            }
        }

        public void SetInspection(Guid id)
        {
            IEnumerable<Inspection> inspectie = Config.Context.Inspection;
            InspectionSpecs = inspectie.Select(a => new InspectionVM(a)).Where(p => p.id == id).First();

            searchBijlagen();
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
            InspectionViewModel inspection = ServiceLocator.Current.GetInstance<InspectionViewModel>();
            EditInspection window = new EditInspection();
            searchBijlagen();
            window.Show();
            CloseView();
        }

        public void CloseView()
        {
            Messenger.Default.Send<NotificationMessage>(
                new NotificationMessage(this, "CloseView")
            );
        }

        public void generatePDF()
        {

            Console.WriteLine(inspectionSpecs.id);

            //Create a byte array that will eventually hold our final PDF
            Byte[] bytes;

            //Boilerplate iTextSharp setup here
            //Create a stream that we can write to, in this case a MemoryStream
            using (var ms = new MemoryStream())
            {

                //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                using (var doc = new Document())
                {

                    //Create a writer that's bound to our PDF abstraction and our stream
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        //Open the document for writing
                        doc.Open();

                        var voorpagina = @"
                            <div style=""margin-top: 200px;""><center>
                            <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                            <h1>" + inspectionSpecs.name + @"</h1><br /><h2>GoInsp</h2></center><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                            Datum: " + inspectionSpecs.date +  @"
                            <br />Inspecteur: " + inspectionSpecs.accountVM.UserName + @"
                            <br /><br />
                            <b>Adres inspectie:</b><br />
                            " + inspectionSpecs.company.BedrijfsAdres + @" " + inspectionSpecs.company.BedrijfsNummer + @"<br />
                            " + inspectionSpecs.company.BedrijfsPostcode + @" " + inspectionSpecs.company.BedrijfsGemeente + @"
                            </div>
                        ";

                        var QuestionTable = "Dit rapport heeft geen gekoppelde vragenlijst.";

                        if (InspectionSpecs.questionnaire != null)
                        {
                            QuestionTable = "<table>";
                            InspectionSpecs.questionnaire.CheckConditionBoundQuestions();
                            ObservableCollection<QuestionVM> SortedQuestionList = new ObservableCollection<QuestionVM>(InspectionSpecs.questionnaire.QuestionnaireCollection.OrderBy(xy => xy.ListNumber));

                            foreach (QuestionVM f in SortedQuestionList)
                            {
                                if(f.Visible == Visibility.Visible)
                                    QuestionTable += @"<tr><td style='width: 50%'>" + f.question.Question + @"</td><td>" + f.GetAnswer() + @"</td></tr>";
                            }
                            QuestionTable += @"</table>";
                        }

                        var Bijlagen = "Dit Rapport heeft geen bijlagen.";

                        if (inspectionSpecs.directory.ToString() != "00000000-0000-0000-0000-000000000000")
                        {
                            Bijlagen = "";
                            string mappie = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                            string speciaalmappie = Path.Combine(mappie, "GoInspGroepB/" + dir);
                            DirectoryInfo d = new DirectoryInfo(speciaalmappie);



                            foreach (var file in d.GetFiles())
                            {
                                if (Path.GetExtension(file.ToString()).ToLower() == ".jpg" || Path.GetExtension(file.ToString()).ToLower() == ".jpeg"
                                    || Path.GetExtension(file.ToString()).ToLower() == ".png" || Path.GetExtension(file.ToString()).ToLower() == ".gif")
                                {
                                    Bijlagen += "<img src='" + speciaalmappie + "/" + file + @"' /><br />" + file + "<br />";
                                }
                                else
                                {
                                    Bijlagen += file + "<br />";
                                }
                                
                                
                            }
                        }

                        Console.WriteLine(Bijlagen);

                        var InfoPagina = @"<div><br /><h1>Algemene informatie:</h1><br /><br />

                            <table>
                            <tr>
                            <td style='width: 200px'>Naam Inspectie:</td>
                            <td>" + inspectionSpecs.name + @"</td>
                            </tr>
                            <tr>
                            <td>Datum inspectie:</td>
                            <td>" + inspectionSpecs.date + @"</td>
                            </tr>
                            <tr>
                            <td>Naam inspecteur:</td>
                            <td>" + inspectionSpecs.accountVM.UserName + @"</td>
                            </tr>
                             <tr>
                            <td>Bedrijfsnaam inspectie:</td>
                            <td>" + inspectionSpecs.company.Bedrijfsnaam + @"</td>
                            </tr>
                            <tr>
                            <td>Bedrijfsadres inspectie:</td>
                            <td>" + inspectionSpecs.company.BedrijfsAdres + @" " + inspectionSpecs.company.BedrijfsNummer + @"</td>
                            </tr>
                            <tr>
                            <td>Bedrijfspostcode inspectie:</td>
                            <td>" + inspectionSpecs.company.BedrijfsPostcode + @"</td>
                            </tr>
                            <tr>
                            <td>Pedrijfsplaats inspectie:</td>
                            <td>" + inspectionSpecs.company.BedrijfsGemeente + @"</td>
                            </tr>
                            <tr>
                            <td>Bijlagen:</td>
                            <td>-</td>
                            </tr>
                            </table>

                        <br /><br />
                        <h1>Rapportage</h1><br />" + inspectionSpecs.description +  @"<br /><br /><h1>Vragenlijst</h1>
                        <br />
                        " + QuestionTable + @"
                        <br /><br />
                        <h1>Bijlagen</h1><br />
                        " + Bijlagen + @"
                        </div>
                        ";

                        var htmlletje = voorpagina + InfoPagina;


                        //XMLWorker also reads from a TextReader and not directly from a string
                        using (var srHtml = new StringReader(htmlletje))
                        {

                            //Parse the HTML
                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
                        }


                        doc.Close();
                    }
                }

                //After all of the PDF "stuff" above is done and closed but **before** we
                //close the MemoryStream, grab all of the active bytes from the stream
                bytes = ms.ToArray();
            }

            //Now we just need to do something with those bytes.
            //Here I'm writing them to disk but if you were in ASP.Net you might Response.BinaryWrite() them.
            //You could also write the bytes to a database in a varbinary() column (but please don't) or you
            //could pass them to another function for further PDF processing.

            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            string specificFolder = Path.Combine(folder, "GoInspGroepB");

            // Check if folder exists and if not, create it
            if (!Directory.Exists(specificFolder))
                Directory.CreateDirectory(specificFolder);

            string filename = Guid.NewGuid().ToString() + ".pdf";

            var testFile = System.IO.Path.Combine(specificFolder, filename);
            System.IO.File.WriteAllBytes(testFile, bytes);

            System.Diagnostics.Process.Start(testFile);
        }
    }
}
