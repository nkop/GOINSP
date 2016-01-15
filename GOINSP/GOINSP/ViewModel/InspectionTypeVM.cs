using GalaSoft.MvvmLight;
using GOINSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.ViewModel
{
    public class InspectionTypeVM : ViewModelBase
    {
        private Models.InspectionType inspectiontype;

        public InspectionTypeVM()
        {
            this.inspectiontype = new Models.InspectionType();
        }

        public InspectionTypeVM(Models.InspectionType inspectiontype)
        {
            this.inspectiontype = inspectiontype;
        }

        public Guid id
        {
            get { return inspectiontype.id; }
        }

        public string type
        {
            get { return inspectiontype.type; }
            set
            {
                inspectiontype.type = value;
                RaisePropertyChanged("type");
            }
        }

        public InspectionType toInspectionType()
        {
            return inspectiontype;
        }
    }
}
