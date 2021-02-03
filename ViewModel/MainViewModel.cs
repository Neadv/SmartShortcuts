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

namespace SmartShortcuts.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Group> Groups { get; set; }

        public MainViewModel()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group("General")
                {
                    Shortcuts = new ObservableCollection<Shortcut>
                    {
                        new Shortcut("Концепт", @"C:\Users\Neadekvashka\Desktop\Концепт.txt")
                        {
                            Actions = new ObservableCollection<IAction>
                            {
                                new RunAction(@"C:\Users\Neadekvashka\Desktop\Концепт.txt")
                            }
                        }
                    }
                }
            };
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
