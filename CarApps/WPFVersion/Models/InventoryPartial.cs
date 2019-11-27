using System.ComponentModel;

namespace WPFVersion.Models
{
    public partial class Inventory:IDataErrorInfo
    {
        private string _error;
        public string Error => _error;
        
        public string this[string columnName]
        {
            get 
            {
                //Original implementation
                switch (columnName)
                {
                    case nameof(CarId):
                        break;
                    case nameof(Make):
                        if (Make == "ModelT") return "Too Old";
                        return CheckMakeAndColor();
                    case nameof(Color):
                        return CheckMakeAndColor();
                    case nameof(PetName):
                        break;
                }
                return string.Empty; 
            }
        }

        internal string CheckMakeAndColor()
        {
            if (Make == "Chevy" && Color == "Pink") return $"{Make}'s don't come in {Color}";
            return string.Empty;
        }
        //internal bool CheckMakeAndColor()
        //{
        //    if (Make != "Chevy" || Color != "Pink") return false;
        //    AddError(nameof(Make), $"{Make}'s don't come in {Color}");
        //    AddError(nameof(Color), $"{Make}'s don't come in {Color}");
        //    return true;
        //}
    }
}
