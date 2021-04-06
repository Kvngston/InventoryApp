namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DrugCartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        DrugId = c.Int(nullable: false),
                        DrugCartId = c.Int(nullable: false, false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drugs", t => t.DrugId, cascadeDelete: false)
                .ForeignKey("dbo.DrugCarts", t => t.DrugCartId, cascadeDelete: false)
                .Index(t => t.DrugId)
                .Index(t => t.DrugCartId);
            
            CreateTable(
                "dbo.DrugCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, true),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        AddressLine1 = c.String(nullable: false, maxLength: 100),
                        AddressLine2 = c.String(),
                        ZipCode = c.String(nullable: false, maxLength: 10),
                        City = c.String(nullable: false, maxLength: 50),
                        State = c.String(maxLength: 10),
                        Country = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 50),
                        OrderTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderPlaced = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        DrugId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Drugs", t => t.DrugId, cascadeDelete: false)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: false)
                .Index(t => t.OrderId)
                .Index(t => t.DrugId);
            
            CreateTable(
                    "dbo.Notifications",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationDetails = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        NotificationType = c.Int(nullable: false),
                        Title = c.String(false)
                    })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "DrugId", "dbo.Drugs");
            DropForeignKey("dbo.DrugCartItems", "DrugCartId", "dbo.DrugCarts");
            DropForeignKey("dbo.DrugCarts", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DrugCartItems", "DrugId", "dbo.Drugs");
            DropIndex("dbo.OrderDetails", new[] { "DrugId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.DrugCarts", new[] { "ApplicationUserId" });
            DropIndex("dbo.DrugCartItems", new[] { "DrugCartId" });
            DropIndex("dbo.DrugCartItems", new[] { "DrugId" });
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.DrugCarts");
            DropTable("dbo.DrugCartItems");
        }
    }
}
