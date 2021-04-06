namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSupplierIdInDrug : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Drugs", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.Drugs", new[] { "SupplierId" });
            DropColumn("dbo.Drugs", "SupplierId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Drugs", "SupplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.Drugs", "SupplierId");
            AddForeignKey("dbo.Drugs", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
        }
    }
}
