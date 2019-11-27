using System.ComponentModel.DataAnnotations;

namespace AutoLotDAL_EF.Models
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
