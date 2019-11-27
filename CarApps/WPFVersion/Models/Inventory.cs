using System.ComponentModel;

namespace WPFVersion.Models
{
    public partial class Inventory:INotifyPropertyChanged
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Color { get; set; }
        public string PetName { get; set; }

        // Important to work with Nuget package PropertyChanged.Fody
        public bool IsChanged { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
