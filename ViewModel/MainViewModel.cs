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

namespace SmartShortcuts.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand AddShortcutCommand { get; }
        public RelayCommand AddNewGroupCommand { get; }
        public RelayCommand EditGroupCommand { get; }
        public RelayCommand RunShortcutCommand { get; }
        public RelayCommand SelectedItemCommand { get; }

        public ObservableCollection<Group> Groups { get; set; }

        private Group selectedGroup = null;
        private Shortcut selectedShortcut = null;

        public MainViewModel()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group("General")
                {
                    Shortcuts = new ObservableCollection<Shortcut>
                    {
                        new Shortcut("Файтинг", @"C:\Users\Neadekvashka\Desktop\ConsoleApp1\bin\Release\netcoreapp3.1\publish\ConsoleApp1.exe")
                        {
                            Actions = new ObservableCollection<IAction>
                            {
                                new RunAction(@"C:\Users\Neadekvashka\Desktop\ConsoleApp1\bin\Release\netcoreapp3.1\publish\ConsoleApp1.exe"),
                            },
                            Type = ShortcutType.Application
                        },
                        new Shortcut("OpenGL")
                        {
                            Actions = new ObservableCollection<IAction>
                            {
                                new RunAction(@"C:\Users\Neadekvashka\Desktop\TestOpenGL"),
                            }
                        },
                        new Shortcut("Labs", @"C:\Users\Neadekvashka\Desktop\Лабы.7z")
                        {
                            Actions = new ObservableCollection<IAction>
                            {
                                new RunAction(@"C:\Users\Neadekvashka\Desktop\Лабы.7z"),
                            }
                        }
                    }
                }
            };
            
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

            SelectedItemCommand = new RelayCommand((obj) =>
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
            }, null);

            AddShortcutCommand = new RelayCommand((obj) => 
            {
                var add = new AddShortcutViewModel(Groups);
                var addWindow = new AddShortcutWindow(add);
                addWindow.ShowDialog();
            }, null);
        }

        public void RunShortcut(Shortcut shortcut)
        {
            if (shortcut.SelectedAction == -1)
            {
                foreach (var action in shortcut.Actions)
                {
                    action.Execute();
                }
            }
            else if (shortcut.SelectedAction < shortcut.Actions.Count && shortcut.SelectedAction >= 0)
            {
                shortcut.Actions[shortcut.SelectedAction].Execute();
            }
        }

        private void OnPropertyChanged([CallerMemberShip] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
