﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GOINSP
{
    /// <summary>
    /// Interaction logic for AddCompanyWindow.xaml
    /// </summary>
    public partial class AddCompanyWindow : Window
    {
        public AddCompanyWindow()
        {
            InitializeComponent();
            Closing += AddCompanyWindow_Closing;
        }

        void AddCompanyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
    }
}