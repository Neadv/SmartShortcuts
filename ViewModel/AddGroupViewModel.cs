using SmartShortcuts.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        public RelayCommand AddCommand { get; }
        public RelayCommand CancelCommand { get; }

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
            CancelCommand = new RelayCommand((obj) => CloseAction?.Invoke(), null);
        }

        private void OnPropertyChanged([CallerMemberShip] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
