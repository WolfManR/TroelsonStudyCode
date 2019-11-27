using AutoLotDAL_EF.Models;
using AutoLotDAL_EF.Repos;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFVersion.Cmds;

namespace WPFVersion.ViewModels
{
    public class MainWindowViewModel
    {
        private ICommand _changeColorCommand = null;
        private ICommand _addCarCommand = null;
        private RelayCommand<Inventory> _deleteCarCommand = null;

        public IList<Inventory> Cars { get; }
        public ICommand ChangeColorCmd => _changeColorCommand ?? (_changeColorCommand = new ChangeColorCommand());
        public ICommand AddCarCmd => _addCarCommand ?? (_addCarCommand = new AddCarCommand());
        public RelayCommand<Inventory> DeleteCarCmd => _deleteCarCommand ?? (_deleteCarCommand = new RelayCommand<Inventory>(DeleteCar, CanDeleteCar));

        public MainWindowViewModel()
        {
            Cars = new ObservableCollection<Inventory>(new InventoryRepo().GetAll());
        }        

        private bool CanDeleteCar(Inventory car) => car != null;
        private void DeleteCar(Inventory car)
        {
            Cars.Remove(car);
        }
    }
}
