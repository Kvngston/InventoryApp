namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationsUpdate : DbMigration
    {
        public override void Up()
        {
            // status error
            AddColumn("dbo.Suppliers", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "Status");
        }
    }
}
