namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoreManager : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoreManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreManagers", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.StoreManagers", new[] { "ApplicationUserId" });
            DropTable("dbo.StoreManagers");
        }
    }
}
