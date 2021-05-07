namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrescribeTotalToCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DrugCartItems", "PrescribedAmount", c => c.Int(nullable: false));
            DropColumn("dbo.DrugCartItems", "PrescribedQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DrugCartItems", "PrescribedQuantity", c => c.Int(nullable: false));
            DropColumn("dbo.DrugCartItems", "PrescribedAmount");
        }
    }
}
