using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GOINSP.Utility
{
    public class ConversionHelper
    {
        public static bool VisibilityToBool(Visibility visibility)
        {
            if (visibility == Visibility.Visible)
                return true;
            return false;
        }

        public static Visibility BoolToVisibility(bool visibility)
        {
            if (visibility)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }
    }
}
