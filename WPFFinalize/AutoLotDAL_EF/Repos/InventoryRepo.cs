using AutoLotDAL_EF.Models;
using System.Collections.Generic;
using System.Linq;

namespace AutoLotDAL_EF.Repos
{
    public class InventoryRepo : BaseRepo<Inventory>
    {
        public override List<Inventory> GetAll()
            => Context.Cars.OrderBy(x => x.PetName).ToList();
    }
}
