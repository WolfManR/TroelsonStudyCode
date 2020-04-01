namespace AutoLotDAL_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CustId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CarId", "dbo.Inventory");
            RenameColumn(table: "dbo.Orders", name: "CustId", newName: "CustomerId");
            RenameIndex(table: "dbo.Orders", name: "IX_CustId", newName: "IX_CustomerId");
            DropPrimaryKey("dbo.CreditRisk");
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Inventory");

            DropColumn("dbo.CreditRisk", "CustID");
            DropColumn("dbo.Customers", "CustID");
            DropColumn("dbo.Orders", "OrderId");
            DropColumn("dbo.Inventory", "CarId");

            AddColumn("dbo.CreditRisk", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CreditRisk", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Customers", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Orders", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Inventory", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Inventory", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddPrimaryKey("dbo.CreditRisk", "Id");
            AddPrimaryKey("dbo.Customers", "Id");
            AddPrimaryKey("dbo.Orders", "Id");
            AddPrimaryKey("dbo.Inventory", "Id");
            CreateIndex("dbo.CreditRisk", new[] { "LastName", "FirstName" }, unique: true, name: "IDX_CreditRisk_Name");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "CarId", "dbo.Inventory", "Id");

            //DropColumn("dbo.CreditRisk", "CustID");
            //DropColumn("dbo.Customers", "CustID");
            //DropColumn("dbo.Orders", "OrderId");
            //DropColumn("dbo.Inventory", "CarId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CarId", "dbo.Inventory");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropPrimaryKey("dbo.Inventory");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.CreditRisk");
            DropColumn("dbo.Inventory", "Timestamp");
            DropColumn("dbo.Inventory", "Id");
            DropColumn("dbo.Orders", "Timestamp");
            DropColumn("dbo.Orders", "Id");
            DropColumn("dbo.Customers", "Timestamp");
            DropColumn("dbo.Customers", "Id");
            DropColumn("dbo.CreditRisk", "Timestamp");
            DropColumn("dbo.CreditRisk", "Id");

            AddColumn("dbo.Inventory", "CarId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Customers", "CustID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CreditRisk", "CustID", c => c.Int(nullable: false, identity: true));
            //DropForeignKey("dbo.Orders", "CarId", "dbo.Inventory");
            //DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CreditRisk", "IDX_CreditRisk_Name");
            //DropPrimaryKey("dbo.Inventory");
            //DropPrimaryKey("dbo.Orders");
            //DropPrimaryKey("dbo.Customers");
            //DropPrimaryKey("dbo.CreditRisk");
            //DropColumn("dbo.Inventory", "Timestamp");
            ////DropColumn("dbo.Inventory", "Id");
            //DropColumn("dbo.Orders", "Timestamp");
            ////DropColumn("dbo.Orders", "Id");
            //DropColumn("dbo.Customers", "Timestamp");
            ////DropColumn("dbo.Customers", "Id");
            //DropColumn("dbo.CreditRisk", "Timestamp");
            //DropColumn("dbo.CreditRisk", "Id");
            AddPrimaryKey("dbo.Inventory", "CarId");
            AddPrimaryKey("dbo.Orders", "OrderId");
            AddPrimaryKey("dbo.Customers", "CustID");
            AddPrimaryKey("dbo.CreditRisk", "CustID");
            RenameIndex(table: "dbo.Orders", name: "IX_CustomerId", newName: "IX_CustId");
            RenameColumn(table: "dbo.Orders", name: "CustomerId", newName: "CustId");
            AddForeignKey("dbo.Orders", "CarId", "dbo.Inventory", "CarId");
            AddForeignKey("dbo.Orders", "CustId", "dbo.Customers", "CustID", cascadeDelete: true);
        }
    }
}
