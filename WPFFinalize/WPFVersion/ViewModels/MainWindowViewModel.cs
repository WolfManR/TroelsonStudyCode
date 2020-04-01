using AutoLotDAL_EF.Models;
using AutoLotDAL_EF.Repos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using WPFVersion.Cmds;

namespace WPFVersion.ViewModels
{
    public class MainWindowViewModel
    {
        private ICommand _changeColorCommand = null;
        private ICommand _addCarCommand = null;
        private RelayCommand<Inventory> _deleteCarCommand = null;

        InventoryRepo inventory = new InventoryRepo();
        public IList<Inventory> Cars { get; }
        public ICommand ChangeColorCmd => _changeColorCommand ?? (_changeColorCommand = new ChangeColorCommand());
        public ICommand AddCarCmd => _addCarCommand ?? (_addCarCommand = new AddCarCommand());
        public RelayCommand<Inventory> DeleteCarCmd => _deleteCarCommand ?? (_deleteCarCommand = new RelayCommand<Inventory>(DeleteCar, CanDeleteCar));

        public MainWindowViewModel()
        {
            var inventories = new ObservableCollection<Inventory>(inventory.GetAll());
            inventories.CollectionChanged += Cars_CollectionChanged;
            Cars = inventories;
            
        }

        private void Cars_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var action = e.Action;
            switch (action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Inventory item in e.NewItems)
                    {
                        inventory.Add(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Inventory item in e.OldItems)
                    {
                        inventory.Delete(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }

        private bool CanDeleteCar(Inventory car) => car != null;
        private void DeleteCar(Inventory car)
        {
            Cars.Remove(car);
        }
    }
}
