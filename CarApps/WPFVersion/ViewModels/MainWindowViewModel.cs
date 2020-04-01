using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFVersion.Cmds;
using WPFVersion.Models;

namespace WPFVersion.ViewModels
{
    public class MainWindowViewModel
    {
        private ICommand _changeColorCommand = null;
        private ICommand _addCarCommand = null;
        private RelayCommand<Inventory> _deleteCarCommand = null;

        public IList<Inventory> Cars { get; } = new ObservableCollection<Inventory>();
        public ICommand ChangeColorCmd => _changeColorCommand ?? (_changeColorCommand = new ChangeColorCommand());
        public ICommand AddCarCmd => _addCarCommand ?? (_addCarCommand = new AddCarCommand());
        public RelayCommand<Inventory> DeleteCarCmd => _deleteCarCommand ?? (_deleteCarCommand = new RelayCommand<Inventory>(DeleteCar, CanDeleteCar));

        public MainWindowViewModel()
        {
            Cars.Add( new Inventory { CarId = 1, Color = "Blue", Make = "Chevy", PetName = "Kit", IsChanged = false });
            Cars.Add( new Inventory { CarId = 2, Color = "Red", Make = "Ford", PetName = "Red Rider", IsChanged = false });
        }        

        private bool CanDeleteCar(Inventory car) => car != null;
        private void DeleteCar(Inventory car)
        {
            Cars.Remove(car);
        }
    }
}
