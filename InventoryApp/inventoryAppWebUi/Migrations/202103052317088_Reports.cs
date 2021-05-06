namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrugSales = c.String(),
                        SuppliersReport = c.String(),
                        TimeFrame = c.Int(nullable: false),
                        TotalRevenueForReport = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "Report_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Report_Id");
            AddForeignKey("dbo.Orders", "Report_Id", "dbo.Reports", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Report_Id", "dbo.Reports");
            DropIndex("dbo.Orders", new[] { "Report_Id" });
            DropColumn("dbo.Orders", "Report_Id");
            DropTable("dbo.Reports");
        }
    }
}
