using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace WPFVersion.Models
{
    public partial class Inventory:IDataErrorInfo,INotifyDataErrorInfo
    {
        private string _error;
        public string Error => _error;


        public readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public bool HasErrors => _errors.Count != 0;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) return _errors.Values;
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }
        protected void AddError(string propertyName, string error)
        {
            AddErrors(propertyName, new List<string> { error });
        }
        
        protected void AddErrors(string propertyName, IList<string> errors)
        {
            if (errors == null || errors.Count == 0) return;

            var changed = false;
            if (!_errors.ContainsKey(propertyName))
            {
                _errors.Add(propertyName, new List<string>());
                changed = true;
            }
            foreach (var err in errors)
            {
                if (_errors[propertyName].Contains(err)) continue;
                _errors[propertyName].Add(err);
                changed = true;
            }
            if (changed) OnErrorsChanged(propertyName);
        }
        protected void ClearErrors(string propertyName = "")
        {
            if (String.IsNullOrEmpty(propertyName)) _errors.Clear();
            else _errors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }


        public string this[string columnName]
        {
            get 
            {
                ////Original implementation
                //switch (columnName)
                //{
                //    case nameof(CarId):
                //        break;
                //    case nameof(Make):
                //        if (Make == "ModelT") return "Too Old";
                //        return CheckMakeAndColor();
                //    case nameof(Color):
                //        return CheckMakeAndColor();
                //    case nameof(PetName):
                //        break;
                //}
                //return string.Empty;

                //Updated implementation

                bool hasError = false;
                switch (columnName)
                {
                    case nameof(CarId):
                        AddErrors(nameof(CarId), GetErrorsFromAnnotations(nameof(CarId), CarId));
                        break;
                    case nameof(Make):
                        hasError = CheckMakeAndColor();
                        if (Make == "ModelT")
                        {
                            AddError(nameof(Make), "Too Old");
                            hasError = true;
                        }
                        if (!hasError)
                        {
                            //This is not perfect logic, just illustrative of the pattern
                            ClearErrors(nameof(Make));
                            ClearErrors(nameof(Color));
                        }
                        //AddErrors(nameof(Make), GetErrorsFromAnnotations(nameof(Make), Make));
                        break;
                    case nameof(Color):
                        hasError = CheckMakeAndColor();
                        if (!hasError)
                        {
                            ClearErrors(nameof(Make));
                            ClearErrors(nameof(Color));
                        }
                        //AddErrors(nameof(Color), GetErrorsFromAnnotations(nameof(Color), Color));
                        break;
                    case nameof(PetName):
                        //AddErrors(nameof(PetName), GetErrorsFromAnnotations(nameof(PetName), PetName));
                        break;
                }
                return string.Empty;
            }
        }

        private IList<string> GetErrorsFromAnnotations(string v, int carId)
        {
            throw new NotImplementedException();
        }

        //internal string CheckMakeAndColor()
        //{
        //    if (Make == "Chevy" && Color == "Pink") return $"{Make}'s don't come in {Color}";
        //    return string.Empty;
        //}
        internal bool CheckMakeAndColor()
        {
            if (Make != "Chevy" || Color != "Pink") return false;
            AddError(nameof(Make), $"{Make}'s don't come in {Color}");
            AddError(nameof(Color), $"{Make}'s don't come in {Color}");
            return true;
        }
    }
}
