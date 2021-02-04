using SmartShortcuts.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SmartShortcuts.ViewModel
{
    public class AddGroupViewModel
    {
        public string Name { get; set; }

        public Action CloseAction { get; set; }
        public RelayCommand AddCommand { get; }
        public RelayCommand CancelCommand { get; }

        public AddGroupViewModel(IList<Group> groups)
        {
            AddCommand = new RelayCommand((obj) =>
            {
                if (groups != null)
                    groups.Add(new Group(Name));
                CloseAction?.Invoke();
            },
            (obj) => Name != null && Name != "");
            CancelCommand = new RelayCommand((obj) => CloseAction?.Invoke(), null);
        }

    }
}
