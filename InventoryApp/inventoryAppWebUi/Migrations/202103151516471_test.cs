namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drugs", "SupplierTag", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drugs", "SupplierTag");
        }
    }
}
