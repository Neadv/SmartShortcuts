using System;
using SmartShortcuts.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.IO;

namespace SmartShortcuts.Model
{
    enum ShortcutType
    {
        Folder,
        Application,
        File
    }

    class Shortcut : INotifyPropertyChanged
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

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
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

        public ImageSource Icon
        {
            get
            {
                if (Type != ShortcutType.Folder)
                    return IconExtracter.GetIcon(path);
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ShortcutType type;
        private string path;
        private string name;
        private DateTime lastModified;

        public Shortcut(string path)
        {
            Path = path;

            var namePattern = new Regex(@"[\w-\.]+\Z");
            var extensionPattern = new Regex(@"\.\w+\Z");
            var match = namePattern.Match(path);

            if (!match.Success)
                throw new ArgumentException("incorrect path");

            string name = match.Value;

            match = extensionPattern.Match(name);

            if (match.Success)
                Name = name.Replace(match.Value, "");
            else
                Name = name;

            LastModified = DateTime.Now;

            if (match.Success && path.Contains(".exe"))
                Type = ShortcutType.Application;
            else if (match.Success)
                Type = ShortcutType.File;
            else
                Type = ShortcutType.Folder;

            if ((Type == ShortcutType.Folder && !Directory.Exists(path)) || (Type != ShortcutType.Folder && !File.Exists(path)))
                throw new ArgumentException("incorrect path"); 

        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
