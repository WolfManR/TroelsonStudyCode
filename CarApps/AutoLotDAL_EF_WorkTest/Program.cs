using AutoLotDAL_EF.EF;
using AutoLotDAL_EF.Models;
using System;
using System.Data.Entity;

namespace AutoLotDAL_EF_WorkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with ADO.NET EF Code First *****\n");    
            
            Database.SetInitializer(new MyDataInitializer());
            using (var context = new AutoLotEntities())
            {
                foreach (Inventory c in context.Inventory) Console.WriteLine(c);
            }
            Console.ReadLine();
        }
    }
}
