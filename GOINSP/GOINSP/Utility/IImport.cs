using GOINSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Utility
{
    public interface IImport
    {
        JSONImport jsonImporter {get; set;}
        CSVImport csvImporter { get; set; }
        Context context { get; set; }

        void Import(IProgress<ImportProgressValues> progress);
    }
}
