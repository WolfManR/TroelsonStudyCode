using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WPFVersion.Models
{
    public partial class Inventory:INotifyPropertyChanged
    {
        [Required]
        public int CarId { get; set; }
        [Required,StringLength(50)]
        public string Make { get; set; }
        [Required, StringLength(50)]
        public string Color { get; set; }
        [Required, StringLength(50)]
        public string PetName { get; set; }

        // Important to work with Nuget package PropertyChanged.Fody
        public bool IsChanged { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != nameof(IsChanged)) IsChanged = true;
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}
