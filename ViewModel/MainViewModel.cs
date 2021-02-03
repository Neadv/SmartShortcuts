using SmartShortcuts.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShortcuts.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Group> Groups { get; set; }

        public MainViewModel()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group
                {
                    Name = "test",
                    Shortcuts = new ObservableCollection<Shortcut>
                    {
                        new Shortcut("test.exe"),
                    }
                }
            };
        }

        private void OnPropertyChanged([CallerMemberShip] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
