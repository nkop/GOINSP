using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Utility
{
    public class CSVImport
    {
        public string CSVString { get; set; }
        public List<string> CSVStrings { get; set; }

        public CSVImport()
        {

        }

        public void LoadFromFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSV Files (.csv)|*.csv";
            fileDialog.FilterIndex = 1;

            bool? okClicked = fileDialog.ShowDialog();

            CSVStrings = new List<string>();

            if (okClicked == true)
            {
                using (StreamReader sr = new StreamReader(fileDialog.FileName))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        CSVStrings.Add(line);
                    }
                }
            }
        }

        public void GetCSVByURL(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                CSVString = webClient.DownloadString(url);
            }
        }
    }
}
