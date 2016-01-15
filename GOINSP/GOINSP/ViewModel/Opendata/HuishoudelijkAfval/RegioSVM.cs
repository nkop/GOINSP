using GOINSP.Models;
using GOINSP.Models.Opendata.HuishoudelijkAfval;
using GOINSP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel.Opendata.HuishoudelijkAfval
{
    public class RegioSVM
    {        
        private RegioS regios;

        public RegioSVM()
        {
            regios = new RegioS();
        }

        public string Key
        {
            get { return regios.Key; }
            set { regios.Key = value.Trim(); }
        }

        public string Title
        {
            get { return regios.Title; }
            set { regios.Title = value.Trim(); }
        }

        public string Description
        {
            get { return regios.Description; }
            set { regios.Description = value.Trim(); }
        }

        public void Insert()
        {
            Config.Context.HuishoudelijkAfvalRegioS.Add(regios);
            Config.Context.SaveChanges();
        }
    }
}
