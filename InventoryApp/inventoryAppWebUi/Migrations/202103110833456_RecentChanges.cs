namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecentChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DrugCategories", "CategoryName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DrugCategories", "CategoryName", c => c.String());
        }
    }
}
