using SmartShortcuts.Model;
using SmartShortcuts.Services;
using System;
using System.ComponentModel;
using System.IO;

namespace SmartShortcuts.ViewModel
{
    public class ShortcutActionViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public string Name
        {
            get => Action.Name;
            set
            {
                Action.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Path
        {
            get => Action.Path;
            set
            {
                Action.Path = value;
                OnPropertyChanged(nameof(Path));
            }
        }

        public string Args
        {
            get => Action.Args;
            set
            {
                Action.Args = value;
                OnPropertyChanged(nameof(Args));
            }
        }

        public ActionType SelectedType
        {
            get => Action.Type;
            set
            {
                switch (value)
                {
                    case ActionType.Run:
                        PathDisplayName = "Path: ";
                        ArgsDisplayName = "Args: ";
                        Args = "";
                        break;
                    case ActionType.RunWithParameters:
                        PathDisplayName = "Path: ";
                        ArgsDisplayName = "Args: ";
                        break;
                    case ActionType.OpenIn:
                        PathDisplayName = "Path1: ";
                        ArgsDisplayName = "Path2: ";
                        break;
                    case ActionType.Script:
                        Path = "cmd";
                        PathDisplayName = "Path: ";
                        ArgsDisplayName = "Script: ";
                        break;
                }

                if (Name.Trim() == SelectedType.ToString())
                    Name = value.ToString();

                Action.Type = value;

                OnPropertyChanged(string.Empty);
            }
        }

        public bool FirstArg => SelectedType != ActionType.Script; 
        public bool SecondArg => SelectedType != ActionType.Run;

        public string PathDisplayName { get; private set; }
        public string ArgsDisplayName { get; private set; }

        public RelayCommand OkCommand { get; }
        public RelayCommand SelectPathCommand { get; }
        public RelayCommand SelectArgsCommand { get; }
        public RelayCommand SelectFArgsCommand { get; }

        public Action CloseAction { get; set; }

        public ShortcutAction Action { get; private set; }
        public bool Result { get; private set; } = false;

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Name):
                        error = ValidateName();
                        break;
                    case nameof(Path):
                        error = ValidatePath();
                        break;
                    case nameof(Args):
                        error = ValidateArgs();
                        break;
                }
                return error;
            }
        }

        private bool Validate()
        {
            if (ValidateName() == string.Empty && ValidatePath() == string.Empty && ValidateArgs() == string.Empty)
                return true;
            return false;
        }

        public ShortcutActionViewModel(ShortcutAction shortcutAction = null)
        {
            Action = shortcutAction == null ? new ShortcutAction() : new ShortcutAction(shortcutAction);
            SelectedType = Action.Type;

            OkCommand = new RelayCommand((obj) =>
            {
                Result = true;
                CloseAction();
            }, (obj) => Validate());

            SelectPathCommand = new RelayCommand((obj) => Path = DialogService.ShowFileDialog(), (obj) => FirstArg);
            SelectFArgsCommand = new RelayCommand((obj) => Args = DialogService.ShowFolderDialog(), (obj) => SelectedType == ActionType.RunWithParameters);
            SelectArgsCommand = new RelayCommand((obj) => 
            {
                if (SelectedType != ActionType.Script)
                {
                    Args = DialogService.ShowFileDialog();
                }
                else
                {
                    using(StreamReader sr = new StreamReader(DialogService.ShowFileDialog()))
                    {
                        Args = sr.ReadToEnd();
                    }
                }
            }, (obj) => SecondArg);
        }

        private string ValidateArgs()
        {
            if ((Args == null || Args == "") && SecondArg)
                return "Enter arguments";
            return string.Empty;
        }

        private string ValidatePath()
        {
            if (!File.Exists(Path) && FirstArg)
                return "IncorrectPath";
            return string.Empty;
        }

        private string ValidateName()
        {
            if (Name == null || Name == "")
                return "Enter name";
            return string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
