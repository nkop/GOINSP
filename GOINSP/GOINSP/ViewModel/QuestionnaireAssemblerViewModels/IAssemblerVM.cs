using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GOINSP.ViewModel.QuestionnaireAssemblerViewModels
{
    public interface IAssemblerVM
    {
        Visibility Visibility { get; set; }
        string AssemblerName { get; set; }
        void OnFocus();
        void Clean();
    }
}
