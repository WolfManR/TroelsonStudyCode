using System.Windows.Input;
using WPFVersion.Models;

namespace WPFVersion.Cmds
{
    class ChangeColorCommand:CommandBase,ICommand
    {
        public override bool CanExecute(object parameter) => (parameter as Inventory) != null;
        public override void Execute(object parameter)
        {
            ((Inventory)parameter).Color = "Pink";
        }
    }
}
