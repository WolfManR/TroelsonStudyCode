using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPFVersion.Models;

namespace WPFVersion
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly IList<Inventory> _cars = new List<Inventory>();
        public MainWindow()
        {
            InitializeComponent();
            _cars.Add(new Inventory { CarId = 1, Color = "Blue", Make = "Chevy", PetName = "Kit" });
            _cars.Add(new Inventory { CarId = 2, Color = "Red", Make = "Ford", PetName = "Red Rider" });
            cboCars.ItemsSource = _cars;
        }

        private void btnChangeColor_Click(object sender, RoutedEventArgs e)
        {
            _cars.First(x => x.CarId == ((Inventory)cboCars.SelectedItem)?.CarId).Color = "Pink";
        }

        private void btnAddCar_Click(object sender, RoutedEventArgs e)
        {
            var maxCount = _cars?.Max(x => x.CarId) ?? 0;
            _cars?.Add(new Inventory { CarId = ++maxCount, Color = "Yellow", Make = "VM", PetName = "Birdie" });
        }
    }
}
