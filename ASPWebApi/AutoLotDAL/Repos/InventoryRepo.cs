using AutoLotDAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace AutoLotDAL.Repos
{
    public class InventoryRepo : BaseRepo<Inventory>
    {
        public override List<Inventory> GetAll()
            => Context.Cars.OrderBy(x => x.PetName).ToList();
    }
}
