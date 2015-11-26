using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Utility
{
    public class ImportProgresValues
    {
        public int MinProgress { get; set; }
        public int MaxProgress { get; set; }
        public ProgressStatus Status { get; set; }

        public ImportProgresValues(int minProgress, int maxProgress, ProgressStatus status)
        {
            this.MinProgress = minProgress;
            this.MaxProgress = maxProgress;
            this.Status = status;
        }

        public enum ProgressStatus
        {
            removing,
            downloading,
            inserting
        }
    }
}
