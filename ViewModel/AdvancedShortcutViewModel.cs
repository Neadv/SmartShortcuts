using SmartShortcuts.Model;
using SmartShortcuts.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShortcuts.ViewModel
{
    public class AdvancedShortcutViewModel : INotifyPropertyChanged
    {
        public Shortcut Shortcut
        {
            get { return shortcut; }
            set
            {
                shortcut = value;
                OnPropertyChanged(nameof(Shortcut));
            }
        }

        public Group SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }

        public ShortcutAction SelectedAction
        {
            get 
            {
                return selectedAction; 
            }
            set 
            { 
                selectedAction = value;
                OnPropertyChanged(nameof(SelectedAction));
            }
        }

        public RelayCommand SelectIconCommand { get; }
        public RelayCommand OkCommand { get; }
        public RelayCommand RemoveActionCommand { get; }

        public IList<Group> Groups { get; }

        public Action CloseAction { get; set; }

        private Shortcut shortcut;
        private Group selectedGroup;
        private ShortcutAction selectedAction;
        private Shortcut oldShortcut;

        public AdvancedShortcutViewModel(IList<Group> groups, Shortcut shortcut = null)
        {
            Groups = groups;
            oldShortcut = shortcut;
            if (shortcut == null)
            {
                Shortcut = new Shortcut("");
                SelectedGroup = groups[0];
            }
            else
            {
                Shortcut = new Shortcut(shortcut);
                foreach (var g in groups)
                {
                    if (g.Shortcuts.Contains(shortcut))
                    {
                        SelectedGroup = g;
                        break;
                    }
                }
            }
            SelectIconCommand = new RelayCommand((obj) => Shortcut.IconPath = DialogService.ShowFileDialog(), null);
            OkCommand = new RelayCommand((obj) => SaveShortcut(), (obj) => Shortcut.Name != "" && SelectedGroup != null);

            RemoveActionCommand = new RelayCommand((obj) => Shortcut.RemoveAction(selectedAction), (obj) => SelectedAction != null);
        }

        private void SaveShortcut()
        {
            if (oldShortcut == null)
            {
                selectedGroup.AddShortcut(Shortcut);
            }
            else
            {
                Group currentGroup = null;
                foreach (var g in Groups)
                {
                    if (g.Shortcuts.Contains(oldShortcut))
                    {
                        currentGroup = g;
                        break;
                    }
                }
                if (currentGroup == SelectedGroup)
                {
                    currentGroup.Shortcuts[currentGroup.Shortcuts.IndexOf(oldShortcut)] = Shortcut;
                }
                else
                {
                    currentGroup.RemoveShortcut(oldShortcut);
                    SelectedGroup.AddShortcut(Shortcut);
                }
            }
            CloseAction?.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
