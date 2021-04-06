namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "GeneratedReferenceNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "GeneratedReferenceNumber");
        }
    }
}
