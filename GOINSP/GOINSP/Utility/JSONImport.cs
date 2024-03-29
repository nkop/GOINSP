﻿using System;
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
    public class JSONImport
    {
        public string JsonString {get; set;}

        public JSONImport()
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
                JsonString = File.ReadAllText(fileDialog.FileName);
            }
        }

        public void GetJsonByURL(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                JsonString = webClient.DownloadString(url);
            }
        }
    }
}
