using System;
using SmartShortcuts.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace SmartShortcuts.Model
{
    public enum ShortcutType
    {
        Folder,
        Application,
        File
    }

    public class Shortcut : INotifyPropertyChanged
    {
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string IconPath
        {
            get { return iconPath; }
            set
            {
                iconPath = value;
                OnPropertyChanged();
            }
        }

        public ShortcutType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public DateTime LastModified
        {
            get
            {
                return lastModified;
            }
            set
            {
                lastModified = value;
                OnPropertyChanged();
            }
        }

        public int SelectedAction
        {
            get { return selectedAction; }
            set
            {
                selectedAction = value;
                OnPropertyChanged();
            }
        }

        public ImageSource Icon
        {
            get
            {
                if (iconPath != "")
                    return IconExtracter.GetIcon(IconPath);
                return null;
            }
        }

        public ObservableCollection<IAction> Actions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ShortcutType type;
        private string iconPath;
        private string name;
        private int selectedAction;
        private DateTime lastModified;

        public Shortcut(string name, string icon = "")
        {
            Name = name;
            IconPath = icon;
            Actions = new ObservableCollection<IAction>();
            selectedAction = -1;
            LastModified = DateTime.Now;
        }

        public void AddAction(IAction action)
        {
            Actions.Add(action);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
