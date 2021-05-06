namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationsEntityUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "NotificationStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "NotificationCategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "NotificationCategory");
            DropColumn("dbo.Notifications", "NotificationStatus");
        }
    }
}
