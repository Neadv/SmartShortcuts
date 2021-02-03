using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartShortcuts.Model
{
    class RunAction : IAction
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        private string args = "";
        private string path = "";

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
