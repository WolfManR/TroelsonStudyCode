using AutoMapper;
using System.Collections.Generic;
using AutoLotDAL_EF.Models.Models;
using AutoLotDAL_EF.Repos;

/// <summary>
/// Сводное описание для AutoLotService
/// </summary>
public class AutoLotService:IAutoLotService
{
    readonly IMapper mapper;
    public AutoLotService()
    {
        mapper = (new MapperConfiguration(cfg => cfg.CreateMap<Inventory, InventoryRecord>())).CreateMapper();
    }

    public void InsertCar(string make, string color, string petname)
    {
        var repo = new InventoryRepo();
        repo.Add(new Inventory { Color = color, Make = make, PetName = petname });
        repo.Dispose();
    }
    public void InsertCar(InventoryRecord car)
    {
        var repo = new InventoryRepo();
        repo.Add(new Inventory { Color = car.Color, Make = car.Make, PetName = car.PetName });
        repo.Dispose();
    }
    public List<InventoryRecord> GetInventory()
    {
        var repo = new InventoryRepo();
        var records = repo.GetAll();
        var results = mapper.Map<List<InventoryRecord>>(records);
        repo.Dispose();
        return results;
    }
}