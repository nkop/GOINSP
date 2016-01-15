using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace GOINSP
{
    /// <summary>
    /// Interaction logic for AddInspection.xaml
    /// </summary>
    public partial class AddInspection : Window
    {
        public ObservableCollection<string> Files
        {
            get
            {
                return _files;
            }
        }

        private ObservableCollection<string> _files;

        public AddInspection()
        {
            InitializeComponent();

            Messenger.Default.Register<NotificationMessage>(this, (nm) =>
            {
                if (nm.Notification == "CloseView2")
                {
                    if (nm.Sender == this.DataContext)
                        this.Close();
                }
            });

            _files = new ObservableCollection<string>();
        }

        private void DropBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                var listbox = sender as ListBox;
                listbox.Background = new SolidColorBrush(Color.FromRgb(155, 155, 155));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropBox_DragLeave(object sender, DragEventArgs e)
        {
            var listbox = sender as ListBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        private void DropBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                _files.Clear();
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string filePath in files)
                {
                    _files.Add(filePath);
                }

                UploadFiles(files);
            }

            var listbox = sender as ListBox;
            listbox.Background = new SolidColorBrush(Color.FromRgb(226, 226, 226));
        }

        private void UploadFiles(string[] files)
        {
            try
            {
                //foreach (var file in files)
                //{
                //    FileStream filestream = new FileStream(file, FileMode.Open);
                //    string fileName = System.IO.Path.GetFileName(file);

                //    string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                //    // Combine the base folder with your specific folder....
                //    string specificFolder = System.IO.Path.Combine(folder, "GoInspGroepB");

                //    // Check if folder exists and if not, create it
                //    if (!Directory.Exists(specificFolder))
                //        Directory.CreateDirectory(specificFolder);

                //    specificFolder = specificFolder + "/test.png";

                //    using (var stream = new FileStream(specificFolder, FileMode.Create, FileAccess.Write))
                //    {
                //        filestream.CopyTo(stream);
                //    }

                //    Console.WriteLine("Copied.");

                //}
            }
            catch (Exception e)
            {
                //Handle exceptions - file not found, access denied, no internet connection etc etc
            }
        }

    }
}
