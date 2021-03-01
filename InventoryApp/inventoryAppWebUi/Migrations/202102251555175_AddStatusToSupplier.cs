namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToSupplier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Suppliers", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "Status");
        }
    }
}
