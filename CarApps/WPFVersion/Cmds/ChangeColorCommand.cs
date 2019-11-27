using System;
using System.Windows.Input;
using WPFVersion.Models;

namespace WPFVersion.Cmds
{
    class ChangeColorCommand:ICommand
    {
        public bool CanExecute(object parameter) => (parameter as Inventory) != null;
        
        public void Execute(object parameter)
        {
            ((Inventory)parameter).Color = "Pink";
        }
        
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
