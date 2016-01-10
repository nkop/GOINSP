using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GOINSP.Utility
{
    public class LoginAuthentication
    {
        private string _fileName, _path, _folder, _specificFolder;

        public LoginAuthentication()
        {
            _fileName = "LoginId.txt";

            // The folder for the roaming current user 
            _folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            _specificFolder = Path.Combine(_folder, "GOINSPGroepB");

            // Check if folder exists and if not, create it
            if (!Directory.Exists(_specificFolder))
                Directory.CreateDirectory(_specificFolder);

            _path = Path.Combine(_specificFolder, _fileName);
        }

        public void SaveLoginId(Guid id)
        {
            File.WriteAllText(_path, String.Empty);
            using (StreamWriter sw = new StreamWriter(_path, true))
            {
                
                sw.WriteLine(id);
                sw.Close();
            }           
        }

        public Guid ReadLoginId()
        {
            
            Guid id;
            string line = "";
            try
            {
                using (StreamReader sr = new StreamReader(_path))
                {
                    line = sr.ReadToEnd();
                                      
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("probleempje in het lezen van de file");
                Console.WriteLine(e.Message);
            }
            return id = new Guid(line);        
        }

    }
}
