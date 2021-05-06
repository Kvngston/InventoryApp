namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUnitToDrug : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drugs", "TotalUnitPerDrugs", c => c.Int(nullable: false));
            AddColumn("dbo.Drugs", "PricePerUnit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drugs", "PricePerUnit");
            DropColumn("dbo.Drugs", "TotalUnitPerDrugs");
        }
    }
}
