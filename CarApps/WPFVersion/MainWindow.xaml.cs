using System.Collections.Generic;
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
    }
}
