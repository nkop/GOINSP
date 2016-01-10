using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOINSP.Utility
{
    public interface INavigatableViewModel
    {
        void Show(INavigatableViewModel sender = null);
        void CloseView();
    }
}
