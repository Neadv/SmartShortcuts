using SmartShortcuts.Model;
using SmartShortcuts.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Media;

namespace SmartShortcuts.ViewModel
{
    public class AddShortcutViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Model.Group SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }
        public ShortcutType SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string ShortcutPath
        {
            get { return shortcutPath; }
            set
            {
                shortcutPath = value;
                OnPropertyChanged(nameof(ShortcutPath));
                OnPropertyChanged(nameof(Icon));
            }
        }
        public IList<Model.Group> Groups { get; set; }
        public ImageSource Icon => IconExtracter.GetIcon(shortcutPath);
        public Action CloseAction { get; set; }

        public RelayCommand OpenFileCommand { get; }
        public RelayCommand OpenFolderCommand { get; }
        public RelayCommand OkCommand { get; }
        public RelayCommand AdvancedSettingCommand { get; }

        public bool IsAdvancedSettings { get; private set; } = false;

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(ShortcutPath):
                        error = ValidatePath();
                        break;
                    case nameof(Name):
                        error = ValidateName();
                        break;
                }
                return error;
            }
        }

        private Model.Group selectedGroup;
        private ShortcutType selectedType;
        private string name;
        private string shortcutPath;

        public AddShortcutViewModel(IList<Model.Group> groups, Model.Group currentGroup = null)
        {
            Groups = groups;
            SelectedGroup = currentGroup ?? groups[0];

            OpenFileCommand = new RelayCommand((obj) => SetPath(DialogService.ShowFileDialog()), null);
            OpenFolderCommand = new RelayCommand((obj) => SetPath(DialogService.ShowFolderDialog()), null);

            OkCommand = new RelayCommand((obj) =>
            {
                Shortcut shortcut = new Shortcut(Name, ShortcutPath, selectedType);
                shortcut.AddAction(new ShortcutAction(ShortcutPath));
                selectedGroup.AddShortcut(shortcut);
                CloseAction?.Invoke();
            }, (obj) => selectedGroup != null && Validate());

            AdvancedSettingCommand = new RelayCommand((obj) =>
            {
                IsAdvancedSettings = true;
                CloseAction?.Invoke();
            }, null);
        }

        private void SetPath(string path)
        {
            if (path == null || path == "")
                return;

            ShortcutPath = path;

            Regex namePattern = new Regex(@"[\w\.-]+\z");
            if (namePattern.IsMatch(path))
            {
                string name = namePattern.Match(path).Value;
                
                Regex extPattern = new Regex(@"\.\w+\z");
                string extension = extPattern.Match(name).Value;

                if (extension != string.Empty)
                    name = name.Replace(extension, string.Empty);

                if (extension == string.Empty)
                    SelectedType = ShortcutType.Folder;
                else if (extension == ".exe")
                    SelectedType = ShortcutType.Application;
                else
                    SelectedType = ShortcutType.File;

                Name = name;
            }
        }

        private bool Validate()
        {
            return ValidatePath() == string.Empty && ValidateName() == string.Empty;
        }

        private string ValidatePath()
        {
            if (File.Exists(ShortcutPath) || Directory.Exists(ShortcutPath))
                return string.Empty;
            return "Incorrect path";
        }

        private string ValidateName()
        {
            if (!string.IsNullOrWhiteSpace(name))
                return string.Empty;
            return "Enter shortcut name";
        }

        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
