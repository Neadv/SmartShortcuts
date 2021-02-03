using System.ComponentModel;

namespace SmartShortcuts.Model
{
    interface IAction : INotifyPropertyChanged
    {
        string Path { get; set; }
        void Execute();
    }
}
