using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;
using GOINSP.ViewModel.Opendata;
using Newtonsoft.Json.Linq;

namespace GOINSP.Utility
{
    class JSONImport
    {
        public string JsonString {get; set;}

        public JSONImport()
        {

        }

        public void ParseJson() 
        {
        }

        public void LoadFromFile() 
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "JSON Files (.json)|*.json";
            fileDialog.FilterIndex = 1;

            bool? okClicked = fileDialog.ShowDialog();

            if (okClicked == true)
            {
                Stream fileStream = fileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    JsonString = reader.ReadLine();
                }
                fileStream.Close();
            }
        }
    }
}
