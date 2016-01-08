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
        private string _fileName;
        private string _path;

        public LoginAuthentication()
        {
            _fileName = "LoginId.txt";
            _path = Path.Combine(Environment.CurrentDirectory, @"Utility\", _fileName);
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
