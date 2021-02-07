using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartShortcuts.Model
{
    public class Group : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get 
            {
                return name; 
            }
            set 
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Shortcut> Shortcuts { get; set; }

        private string name;

        public Group(string name)
        {
            Name = name;
            Shortcuts = new ObservableCollection<Shortcut>();
        }

        public void AddShortcut(Shortcut shortcut)
        {
            Shortcuts.Add(shortcut);
        }

        public void RemoveShortcut(Shortcut shortcut)
        {
            Shortcuts.Remove(shortcut);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
