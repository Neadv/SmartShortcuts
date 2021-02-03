﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShortcuts.Model
{
    class Group : INotifyPropertyChanged
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

        private void OnPropertyChanged([CallerMemberShip]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
