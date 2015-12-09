using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel
{
    public class InspectionVM : ViewModelBase
    {
        private Models.Inspection inspection;
        private Models.Context context;

        public InspectionVM()
        {
            this.inspection = new Models.Inspection();
            this.context = new Models.Context();
        }

        public InspectionVM(Models.Inspection inspection)
        {
            this.inspection = inspection;
            this.context = new Models.Context();
        }
    }
}
