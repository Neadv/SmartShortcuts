using SmartShortcuts.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SmartShortcuts.ViewModel
{
    public class EditGroupViewModel
    {
        public string Name { get; set; }

        public RelayCommand RemoveCommand { get; }
        public RelayCommand OkCommand { get; }

        public ObservableCollection<Shortcut> Shortcuts { get; set; }
        public Shortcut SelectedItem { get; set; }

        public Action CloseAction { get; set; }

        public EditGroupViewModel(Group group)
        {
            Name = group.Name;
            Shortcuts = new ObservableCollection<Shortcut>(group.Shortcuts);
            var removedShortcuts = new List<Shortcut>();

            RemoveCommand = new RelayCommand((obj) =>
            {
                removedShortcuts.Add(SelectedItem);
                Shortcuts.Remove(SelectedItem);
            }, (obj) => SelectedItem != null);
           
            OkCommand = new RelayCommand((obj) =>
            {
                group.Name = Name;
                foreach (var s in removedShortcuts)
                {
                    group.Shortcuts.Remove(s);
                }
                CloseAction?.Invoke();
            }, null);
        }
    }
}
