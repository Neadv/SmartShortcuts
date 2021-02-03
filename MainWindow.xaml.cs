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
            var tree = (TreeView)sender;
            if (tree.SelectedItem is Shortcut shortcut)
                (DataContext as MainViewModel).RunShortcut(shortcut);
        }
    }
}
