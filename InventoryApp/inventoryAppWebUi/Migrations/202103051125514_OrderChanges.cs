namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "DrugId", "dbo.Drugs");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.DrugCartItems", "DrugCartId", "dbo.DrugCarts");
            DropIndex("dbo.DrugCartItems", new[] { "DrugCartId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "DrugId" });
            DropPrimaryKey("dbo.DrugCarts");
            AddColumn("dbo.DrugCartItems", "Order_OrderId", c => c.Int());
            AddColumn("dbo.Orders", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DrugCartItems", "DrugCartId", c => c.Int(nullable: false));
            AlterColumn("dbo.Drugs", "DrugName", c => c.String(nullable: false));
            AlterColumn("dbo.DrugCarts", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Orders", "FirstName", c => c.String());
            AlterColumn("dbo.Orders", "LastName", c => c.String());
            AlterColumn("dbo.Orders", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Orders", "Email", c => c.String());
            AddPrimaryKey("dbo.DrugCarts", "Id");
            CreateIndex("dbo.DrugCartItems", "DrugCartId");
            CreateIndex("dbo.DrugCartItems", "Order_OrderId");
            AddForeignKey("dbo.DrugCartItems", "Order_OrderId", "dbo.Orders", "OrderId");
            AddForeignKey("dbo.DrugCartItems", "DrugCartId", "dbo.DrugCarts", "Id", cascadeDelete: true);
            DropColumn("dbo.Orders", "AddressLine1");
            DropColumn("dbo.Orders", "AddressLine2");
            DropColumn("dbo.Orders", "ZipCode");
            DropColumn("dbo.Orders", "City");
            DropColumn("dbo.Orders", "State");
            DropColumn("dbo.Orders", "Country");
            DropColumn("dbo.Orders", "OrderTotal");
            DropColumn("dbo.Orders", "OrderPlaced");
            DropTable("dbo.OrderDetails");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.OrderDetailId);
            
            AddColumn("dbo.Orders", "OrderPlaced", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "OrderTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "Country", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Orders", "State", c => c.String(maxLength: 10));
            AddColumn("dbo.Orders", "City", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Orders", "ZipCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.Orders", "AddressLine2", c => c.String());
            AddColumn("dbo.Orders", "AddressLine1", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.DrugCartItems", "DrugCartId", "dbo.DrugCarts");
            DropForeignKey("dbo.DrugCartItems", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.DrugCartItems", new[] { "Order_OrderId" });
            DropIndex("dbo.DrugCartItems", new[] { "DrugCartId" });
            DropPrimaryKey("dbo.DrugCarts");
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Orders", "PhoneNumber", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Orders", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Orders", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.DrugCarts", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Drugs", "DrugName", c => c.String());
            AlterColumn("dbo.DrugCartItems", "DrugCartId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Orders", "CreatedAt");
            DropColumn("dbo.Orders", "Price");
            DropColumn("dbo.DrugCartItems", "Order_OrderId");
            AddPrimaryKey("dbo.DrugCarts", "Id");
            CreateIndex("dbo.OrderDetails", "DrugId");
            CreateIndex("dbo.OrderDetails", "OrderId");
            CreateIndex("dbo.DrugCartItems", "DrugCartId");
            AddForeignKey("dbo.DrugCartItems", "DrugCartId", "dbo.DrugCarts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "DrugId", "dbo.Drugs", "Id", cascadeDelete: true);
        }
    }
}
