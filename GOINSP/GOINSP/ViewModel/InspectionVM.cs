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

        public InspectionVM()
        {
            this.inspection = new Models.Inspection();
        }

        public InspectionVM(Models.Inspection inspection)
        {
            this.inspection = inspection;
        }

        public Guid id
        {
            get { return inspection.id; }
        }

        public string name
        {
            get { return inspection.name; }
            set { inspection.name = value; }
        }

        public DateTime date
        {
            get { return inspection.date; }
            set { inspection.date = value; }
        }

        public double longtitude
        {
            get { return inspection.longtitude; }
            set { inspection.longtitude = value; }
        }

        public double latitude
        {
            get { return inspection.latitude; }
            set { inspection.latitude = value; }
        }

        public string address
        {
            get { return inspection.address; }
            set { inspection.address = value; }
        }

        public string zipcode
        {
            get { return inspection.zipcode; }
            set { inspection.zipcode = value; }
        }

        public Guid inspectorid
        {
            get { return inspection.inspectorid; }
            set { inspection.inspectorid = value; }
        }

        public string description
        {
            get { return inspection.description; }
            set { inspection.description = value; }
        }

        public string image
        {
            get { return inspection.image; }
            set { inspection.image = value; }
        }
    }
}
