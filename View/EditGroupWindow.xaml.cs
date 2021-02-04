﻿using SmartShortcuts.ViewModel;
using System;
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

namespace SmartShortcuts.View
{
    /// <summary>
    /// Логика взаимодействия для EditGroupWindow.xaml
    /// </summary>
    public partial class EditGroupWindow : Window
    {
        public EditGroupWindow(EditGroupViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            viewModel.CloseAction = this.Close;
        }
    }
}