using GOINSP_portal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GOINSP_portal.Controllers
{
    public class HomeController : Controller
    {
        List<string> files = new List<string>();
        GOINSPEntities db = new GOINSPEntities();
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Login()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(FormCollection form)
        {
            string company = form["company"];
            string key = form["key"];
            

            Company cmp = db.Companies.Where(c => c.BedrijfsNaam == company && c.BedrijfsPortalKey == key).FirstOrDefault();
            if (cmp != null)
            {
                HttpContext.Session["company"] = cmp;
                HttpContext.Session["companyName"] = cmp.BedrijfsNaam;


                return RedirectToAction("Reports");
            }
            ViewBag.Error = "Bedrijf niet bekend of verkeerde Key ingevoerd.";
            return View();
        }

        public ActionResult Reports()
        {
            if (HttpContext.Session["company"] != null)
            {
                getReports((Company) HttpContext.Session["company"]);
                ViewBag.files = files;
                return View();
            }
            else
            {
                return View("NoAccess");
            }

        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();       

            return View("Index");
        }

        public FileStreamResult DownloadFile(String file)
        {
            if (HttpContext.Session["company"] != null)
            {

                Company cmp = (Company)HttpContext.Session["company"];

                FileStreamResult fileStreamResult;
                System.Net.WebRequest req = System.Net.WebRequest.Create("ftp://marijnsimons.nl/" + cmp.companyid.ToString().ToUpper() + "/" + file);
                req.Credentials = new System.Net.NetworkCredential("goinsp@marijnsimons.nl", "Waterval!");
                System.IO.Stream stream = req.GetResponse().GetResponseStream();
                fileStreamResult = new FileStreamResult(stream, "application/pdf");
                fileStreamResult.FileDownloadName = file;

                return fileStreamResult;
            }
            return null;
        }

        private void getReports(Company cmp)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://www.marijnsimons.nl/" + cmp.companyid.ToString().ToUpper() +"/");
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("goinsp@marijnsimons.nl", "Waterval!");
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                while (!reader.EndOfStream)
                {
                    files.Add(reader.ReadLine());
                }


                reader.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Niet gevonden" + e.ToString());
            }
        }
    }
}