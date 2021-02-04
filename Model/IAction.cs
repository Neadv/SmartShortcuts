using System.ComponentModel;

namespace SmartShortcuts.Model
{
    public interface IAction : INotifyPropertyChanged
    {
        void Execute();
    }
}
