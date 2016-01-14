using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Utility
{
    public static class Config
    {
       public static Guid GebruikerID;
       public static Models.Account.Rights Rechten;


        //Rechten:
        //0: Default
        //1: Externe Inspecteur
        //2: Manager
        //3: Administrator
        //4: Externe Inspecteur
    }
}
