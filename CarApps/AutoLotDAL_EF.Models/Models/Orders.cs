using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLotDAL_EF.Models.Models
{
    public partial class Orders:EntityBase
    {
        public int CustomerId { get; set; }
        public int CarId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customers Customers { get; set; }

        [ForeignKey(nameof(CarId))]
        public virtual Inventory Inventory { get; set; }
    }
}
