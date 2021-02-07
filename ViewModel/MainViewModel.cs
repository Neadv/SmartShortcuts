using System;
using SmartShortcuts.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SmartShortcuts.View;
using System.Runtime.CompilerServices;

namespace SmartShortcuts.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand AddShortcutCommand { get; }
        public RelayCommand AddNewGroupCommand { get; }
        public RelayCommand EditGroupCommand { get; }
        public RelayCommand EditShortcutCommand { get; }
        public RelayCommand RunShortcutCommand { get; }
        public RelayCommand SelectedItemCommand { get; }
        public RelayCommand LoadCommand { get; }
        public RelayCommand CloseCommand { get; }

        public ObservableCollection<Group> Groups
        {
            get => groups;
            set
            {
                groups = value;
                OnPropertyChanged();
            }
        }

        private const string PATH_TO_SAVE = "Shortcuts.json";

        private Group selectedGroup = null;
        private Shortcut selectedShortcut = null;
        private ObservableCollection<Group> groups;

        public MainViewModel()
        {
            AddNewGroupCommand = new RelayCommand((obj) =>
            {
                var addGroup = new AddGroupViewModel(Groups);
                var addWindow = new AddGroupWindow(addGroup);
                addWindow.ShowDialog();
            }, null);

            EditGroupCommand = new RelayCommand((obj) =>
            {
                var edit = new EditGroupViewModel(selectedGroup);
                var editWindow = new EditGroupWindow(edit);
                editWindow.ShowDialog();
            }, (obj) => selectedGroup != null);

            RunShortcutCommand = new RelayCommand((obj) => RunShortcut(selectedShortcut), (obj) => selectedShortcut != null);

            SelectedItemCommand = new RelayCommand((obj) => SelectedItemChanged(obj), null);

            AddShortcutCommand = new RelayCommand((obj) =>
            {
                if (ShowCreateShortcutWindow())
                {
                    ShowAdvancedShortcutWindow();
                }
            }, null);

            EditShortcutCommand = new RelayCommand((obj) =>
            {
                ShowAdvancedShortcutWindow(selectedShortcut);
            }, (obj) => selectedShortcut != null);

            LoadCommand = new RelayCommand((obj) =>
            {
                Groups = ShortcutSerializer.Deserialize(PATH_TO_SAVE);
                if (Groups == null)
                    Groups = new ObservableCollection<Group> { new Group("General") };
            }, null);
            CloseCommand = new RelayCommand((obj) => ShortcutSerializer.Serialize(Groups, PATH_TO_SAVE), null);
        }

        private void SelectedItemChanged(object obj)
        {
            if (obj is Group g)
            {
                selectedShortcut = null;
                selectedGroup = g;
            }
            else if (obj is Shortcut sc)
            {
                selectedGroup = null;
                selectedShortcut = sc;
            }
        }

        private bool ShowCreateShortcutWindow()
        {
            var add = new AddShortcutViewModel(Groups);
            var addWindow = new AddShortcutWindow(add);
            addWindow.ShowDialog();
            return add.IsAdvancedSettings;
        }

        private void ShowAdvancedShortcutWindow(Shortcut shortcut = null)
        {
            var addAdv = new AdvancedShortcutViewModel(groups, shortcut);
            var addAdvWindow = new AdvancedShortcutWindow(addAdv);
            addAdvWindow.ShowDialog();
        }

        public void RunShortcut(Shortcut shortcut)
        {
            foreach (var action in shortcut.Actions)
            {
                action.Execute();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
