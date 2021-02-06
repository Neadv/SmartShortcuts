using System.Windows;
using System.Windows.Controls;
using SmartShortcuts.Model;
using SmartShortcuts.ViewModel;

namespace SmartShortcuts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void TreeViewShortcuts_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var runCommand = (DataContext as MainViewModel).RunShortcutCommand;
            if (runCommand.CanExecute(sender))
                runCommand.Execute(sender);
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            (DataContext as MainViewModel).SelectedItemCommand.Execute((sender as TreeView).SelectedItem);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).LoadCommand.Execute(sender);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as MainViewModel).CloseCommand.Execute(sender);
        }
    }
}
