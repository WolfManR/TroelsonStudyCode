using AutoLotDAL_EF.EF;
using AutoLotDAL_EF.Models;
using AutoLotDAL_EF.Repos;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AutoLotDAL_EF_WorkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with ADO.NET EF Code First *****\n");    
            
            //Database.SetInitializer(new MyDataInitializer());
            using (var context = new AutoLotEntities())
            {
                foreach (Inventory c in context.Cars) Console.WriteLine(c);
            }

            Console.WriteLine("***** Using a Repository *****\n");
            using (var repo = new InventoryRepo())
            {
                foreach (Inventory c in repo.GetAll()) Console.WriteLine(c);
            }
            TestConcurrency();

            Console.ReadLine();
        }

        private static void AddNewRecord(Inventory car)
        {
            using (var repo = new InventoryRepo())
            {
                repo.Add(car);
            }
        }        

        private static void UpdateRecord(int carId)
        {
            using (var repo = new InventoryRepo())
            {
                // Grab the car, change it, save! 
                var carToUpdate = repo.GetOne(carId);
                if (carToUpdate == null) return;
                carToUpdate.Color = "Blue";
                repo.Save(carToUpdate);
            }
        }
        
        private static void RemoveRecordByCar(Inventory carToDelete)
        {
            using (var repo = new InventoryRepo())
            {
                repo.Delete(carToDelete);
            }
        }
        
        private static void RemoveRecordById(int carId, byte[] timeStamp)
        {
            using (var repo = new InventoryRepo())
            {
                repo.Delete(carId, timeStamp);
            }
        }
        
        private static void TestConcurrency()
        {
            var repo1 = new InventoryRepo();
            //Use a second repo to make sure using a different context
            var repo2 = new InventoryRepo();
            var car1 = repo1.GetOne(1);
            var car2 = repo2.GetOne(1);
            car1.PetName = "NewName";
            repo1.Save(car1);
            car2.PetName = "OtherName";
            try
            {
                repo2.Save(car2);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var currentValues = entry.CurrentValues;
                var originalValues = entry.OriginalValues;
                var dbValues = entry.GetDatabaseValues();
                Console.WriteLine(" ******** Concurrency ************");
                Console.WriteLine("Type\tPetName");
                Console.WriteLine($"Current:\t{currentValues[nameof(Inventory.PetName)]}");
                Console.WriteLine($"Orig:\t{originalValues[nameof(Inventory.PetName)]}");
                Console.WriteLine($"db:\t{dbValues[nameof(Inventory.PetName)]}");
            }
        }
    }
}
