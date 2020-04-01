using AutoLotDAL.Models;
using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace AutoLotDAL.EF
{
    public partial class AutoLotEntities : DbContext
    {
        //static readonly DatabaseLogger DatabaseLogger = new DatabaseLogger("sqllog.txt");
        public AutoLotEntities() : base("name=AutoLotConnection")
        {
            //DbInterception.Add(new ConsoleWriterInterceptor());

            //DatabaseLogger.StartLogging();
            //DbInterception.Add(DatabaseLogger);

            //var context = (this as IObjectContextAdapter).ObjectContext;
            //context.ObjectMaterialized += OnObjectMaterialized;
            //context.SavingChanges += OnSavingChanges;
        }

        protected override void Dispose(bool disposing)
        {
            //DbInterception.Remove(DatabaseLogger);
            //DatabaseLogger.StopLogging();
            //base.Dispose(disposing);
        }

        private void OnSavingChanges(object sender, EventArgs eventArgs)
        {
            //Sender is of type ObjectContext.  Can get current and original values, and 
            //   cancel /modify the save operation as desired.
            var context = sender as ObjectContext;
            if (context == null) return;
            foreach (ObjectStateEntry item in context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added))
            {
                //Do something important here
                if ((item.Entity as Inventory) != null)
                {
                    var entity = (Inventory)item.Entity;
                    if (entity.Color == "Red") item.RejectPropertyChanges(nameof(entity.Color));
                }
            }
        }
        private void OnObjectMaterialized(object sender, ObjectMaterializedEventArgs e) { }

        public virtual DbSet<CreditRisk> CreditRisk { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Inventory> Cars { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Inventory)
                .WillCascadeOnDelete(false);
        }
    }
}
