﻿using SmartShortcuts.Model;
using SmartShortcuts.ViewModel;
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
    /// Логика взаимодействия для AddShortcutWindow.xaml
    /// </summary>
    public partial class AddShortcutWindow : Window
    {
        public AddShortcutWindow(AddShortcutViewModel vm)
        {
            InitializeComponent();

            DataContext = vm;
            vm.CloseAction = this.Close;

            TypeComboBox.ItemsSource = Enum.GetValues(typeof(ShortcutType)).Cast<ShortcutType>();
        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {

        }
    }
}
