using AutoLotDAL_EF.Models;

namespace WPFVersion.Cmds
{
    public class ChangeColorCommand:CommandBase
    {
        public override bool CanExecute(object parameter) => (parameter as Inventory) != null;
        public override void Execute(object parameter)
        {
            ((Inventory)parameter).Color = "Pink";
        }
    }
}
