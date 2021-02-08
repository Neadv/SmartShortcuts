using System;
using SmartShortcuts.Services;
using System.ComponentModel;
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
                OnPropertyChanged(nameof(Name));
            }
        }

        public string IconPath
        {
            get { return iconPath; }
            set
            {
                iconPath = value;
                OnPropertyChanged(nameof(IconPath));
                OnPropertyChanged(nameof(Icon));
            }
        }
        
        public ShortcutType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
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
                OnPropertyChanged(nameof(LastModified));
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public ImageSource Icon
        {
            get
            {
                if (iconPath != "")
                    return IconExtracter.GetIcon(IconPath);
                return null;
            }
        }

        public ObservableCollection<ShortcutAction> Actions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ShortcutType type;
        private string iconPath;
        private string name;
        private DateTime lastModified;
        
        [Newtonsoft.Json.JsonConstructor]
        public Shortcut(string name = "", string icon = "", ShortcutType type = ShortcutType.Folder)
        {
            Name = name;
            IconPath = icon;
            Actions = new ObservableCollection<ShortcutAction>();
            LastModified = DateTime.Now;
            Type = type;
        }

        public Shortcut(Shortcut shortcut)
        {
            Name = shortcut.Name;
            IconPath = shortcut.IconPath;
            Type = shortcut.Type;
            LastModified = DateTime.Now;

            Actions = new ObservableCollection<ShortcutAction>();
            foreach (var a in shortcut.Actions)
            {
                Actions.Add(a);
            }
        }

        public void AddAction(ShortcutAction action) => Actions.Add(action);
        public void RemoveAction(ShortcutAction action) => Actions.Remove(action);

        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
