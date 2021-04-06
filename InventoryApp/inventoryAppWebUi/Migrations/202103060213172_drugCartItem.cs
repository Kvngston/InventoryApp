namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drugCartItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DrugCartItems", "DrugCartId", "dbo.DrugCarts");
            DropIndex("dbo.DrugCartItems", new[] { "DrugCartId" });
            DropPrimaryKey("dbo.DrugCarts");
            AlterColumn("dbo.DrugCartItems", "DrugCartId", c => c.Int(nullable: false));
            AlterColumn("dbo.Drugs", "DrugName", c => c.String(nullable: false));
            AlterColumn("dbo.DrugCarts", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.DrugCarts", "Id");
            CreateIndex("dbo.DrugCartItems", "DrugCartId");
            AddForeignKey("dbo.DrugCartItems", "DrugCartId", "dbo.DrugCarts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DrugCartItems", "DrugCartId", "dbo.DrugCarts");
            DropIndex("dbo.DrugCartItems", new[] { "DrugCartId" });
            DropPrimaryKey("dbo.DrugCarts");
            AlterColumn("dbo.DrugCarts", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Drugs", "DrugName", c => c.String());
            AlterColumn("dbo.DrugCartItems", "DrugCartId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.DrugCarts", "Id");
            CreateIndex("dbo.DrugCartItems", "DrugCartId");
            AddForeignKey("dbo.DrugCartItems", "DrugCartId", "dbo.DrugCarts", "Id", cascadeDelete: true);
        }
    }
}
