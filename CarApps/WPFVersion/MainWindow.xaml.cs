using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPFVersion.Cmds;
using WPFVersion.Models;

namespace WPFVersion
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICommand _changeColorCommand = null;
        private ICommand _addCarCommand = null;
        public ICommand ChangeColorCmd => _changeColorCommand ?? (_changeColorCommand = new ChangeColorCommand());
        public ICommand AddCarCmd => _addCarCommand ?? (_addCarCommand = new AddCarCommand());

        readonly IList<Inventory> _cars = new ObservableCollection<Inventory>();
        public MainWindow()
        {
            InitializeComponent();
            _cars.Add(new Inventory { CarId = 1, Color = "Blue", Make = "Chevy", PetName = "Kit", IsChanged = false });
            _cars.Add(new Inventory { CarId = 2, Color = "Red", Make = "Ford", PetName = "Red Rider", IsChanged = false });
            cboCars.ItemsSource = _cars;
        }
    }
}
