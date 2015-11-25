using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Utility
{
    interface IImport
    {
        JSONImport jsonImporter {get; set;}

        void Import();
    }
}
