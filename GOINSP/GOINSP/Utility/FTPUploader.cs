using GOINSP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Utility
{
    public class FTPUploader
    {
        public void upload(Company cmp, string fileLocation, Inspection insp)
        {
            string user = "goinsp@marijnsimons.nl";
            string pwd = "Waterval!";

            FtpDirectoryExists(("ftp://marijnsimons.nl/" + cmp.companyid.ToString().ToUpper() + "/"), user, pwd);
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://marijnsimons.nl/" + cmp.companyid.ToString().ToUpper() + "/" + "Inspection-" + insp.name + "-" + insp.date.ToString("ddMMyyyy") + ".pdf");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.KeepAlive = false;
            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(user, pwd);

            // Copy the contents of the file to the request stream.
            byte[] fileContents = File.ReadAllBytes(fileLocation);

            request.ContentLength = fileContents.Length;
            using (Stream s = request.GetRequestStream())
            {
                s.Write(fileContents, 0, fileContents.Length);
            }
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

            response.Close();
            
        }

        public void FtpDirectoryExists(string directoryPath, string ftpUser, string ftpPassword)
        {
            try { 
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(directoryPath);
                request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.UseBinary = true;
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }
            } catch (Exception e)
            {
                //Directory exists
            }
        }
    }
}
