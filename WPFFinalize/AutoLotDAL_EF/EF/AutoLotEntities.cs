using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using AutoLotDAL_EF.Models;
using AutoLotDAL_EF.Models.Base;
using System.Data.Entity.Infrastructure;

namespace AutoLotDAL_EF.EF
{
    public partial class AutoLotEntities : DbContext
    {
        public AutoLotEntities() : base("name=AutoLotConnection")
        {
            var context = (this as IObjectContextAdapter).ObjectContext;
            context.ObjectMaterialized += OnObjectMaterialized;
        }
        
        protected override void Dispose(bool disposing)
        {
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
        private void OnObjectMaterialized(object sender, ObjectMaterializedEventArgs e) 
        {
            var model = (e.Entity as EntityBase);
            if(model != null) model.IsChanged = false;
        }

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
