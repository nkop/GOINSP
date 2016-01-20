using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Utility
{
    public class ImportProgressValues
    {
        public int MinProgress { get; set; }
        public int MaxProgress { get; set; }
        public ProgressStatus Status { get; set; }

        public ImportProgressValues(int minProgress, int maxProgress, ProgressStatus status)
        {
            this.MinProgress = minProgress;
            this.MaxProgress = maxProgress;
            this.Status = status;
        }

        public enum ProgressStatus
        {
            removing,
            downloading,
            inserting,
            saving,
            error,
            done
        }
    }
}
