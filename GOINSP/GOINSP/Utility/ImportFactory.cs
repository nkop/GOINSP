using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Utility
{
    public class ImportFactory
    {
        public static IImport GetFactory(string importer)
        {
            if(importer.ToLower() == "regios")
                return new RegioSImport();
            if (importer.ToLower() == "tdata")
                return new TDataImport();
            return null;
        }
    }
}
