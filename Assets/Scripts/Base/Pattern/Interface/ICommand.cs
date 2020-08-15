using System;
namespace Base.Pattern.Interface
{
    public interface ICommand
    {
        bool CanExecute(object parameter = null);
        void Execute(object parameter = null);
    }
}