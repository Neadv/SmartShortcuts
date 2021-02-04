using SmartShortcuts.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShortcuts.ViewModel
{
    public class AddGroupViewModel : INotifyPropertyChanged
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

        public Action CloseAction { get; set; }
        public RelayCommand AddCommand { get; set; }

        private string name;

        public AddGroupViewModel(IList<Group> groups)
        {
            AddCommand = new RelayCommand((obj) =>
            {
                if (groups != null)
                    groups.Add(new Group(name));
                CloseAction?.Invoke();
            },
            (obj) => name != null && name != "");
        }

        private void OnPropertyChanged([CallerMemberShip] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
