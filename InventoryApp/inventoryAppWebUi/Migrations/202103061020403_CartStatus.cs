namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DrugCarts", "CartStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DrugCarts", "CartStatus");
        }
    }
}
