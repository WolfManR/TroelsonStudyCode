using System.ComponentModel;
using System.Runtime.CompilerServices;
/// <summary>
/// Manual work with INotifyPropertyChanged
/// </summary>
namespace WPFVersion.Models.NotMoreNeeded
{
    public class InventoryManual : INotifyPropertyChanged
    {
        private int carId;
        private string color;
        private string make;
        private string petName;
        private bool isChanged;

        public int CarId
        {
            get => carId;
            set
            {
                if (carId == value) return;
                carId = value;
                OnPropertyChanged();
            }
        }
        public string Make
        {
            get => make;
            set
            {
                if (make == value) return;
                make = value;
                OnPropertyChanged();
            }
        }
        public string Color
        {
            get => color;
            set
            {
                if (color == value) return;
                color = value;
                OnPropertyChanged(nameof(Color));  //Attribute CallerMemberName not needed with "nameof()" method
            }
        }
        public string PetName
        {
            get => petName;
            set
            {
                if (petName == value) return;
                petName = value;
                OnPropertyChanged();
            }
        }
        public bool IsChanged
        {
            get => isChanged; 
            set
            {
                if (isChanged == value) return;
                isChanged = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != nameof(IsChanged)) IsChanged = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
