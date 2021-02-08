using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartShortcuts.Model
{
    public enum ActionType
    {
        Run,
        RunWithParameters,
        OpenIn,
        Script
    }
    public class ShortcutAction
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

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                OnPropertyChanged();
            }
        }

        public string Args
        {
            get
            {
                return args;
            }
            set
            {
                args = value;
                OnPropertyChanged();
            }
        }

        public ActionType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        private string args;
        private string path;
        private string name;
        private ActionType type;

        [Newtonsoft.Json.JsonConstructor]
        public ShortcutAction(string path = "", string args = "", ActionType type = ActionType.Run)
        {
            Path = path;
            Args = args;
            Type = type;
            Name = Type.ToString();
        }

        public ShortcutAction(ShortcutAction action)
        {
            Path = action.Path;
            Args = action.Args;
            Type = action.Type;
            Name = action.Name;
        }

        public void Execute()
        {
            if (path == null || path == "")
                throw new WarningException("empty path");
            Process.Start(Path, Args);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
