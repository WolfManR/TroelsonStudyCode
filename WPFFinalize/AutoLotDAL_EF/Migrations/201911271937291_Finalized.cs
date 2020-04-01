namespace AutoLotDAL_EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Finalized : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventory", "IsChanged", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "IsChanged", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "IsChanged", c => c.Boolean(nullable: false));
            AddColumn("dbo.CreditRisk", "IsChanged", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreditRisk", "IsChanged");
            DropColumn("dbo.Customers", "IsChanged");
            DropColumn("dbo.Orders", "IsChanged");
            DropColumn("dbo.Inventory", "IsChanged");
        }
    }
}
