using System.ComponentModel;

namespace SmartShortcuts.Model
{
    interface IAction : INotifyPropertyChanged
    {
        void Execute();
    }
}
