namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drugs", "Report_Id", c => c.Int());
            CreateIndex("dbo.Drugs", "Report_Id");
            AddForeignKey("dbo.Drugs", "Report_Id", "dbo.Reports", "Id");
            DropColumn("dbo.Reports", "SuppliersReport");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "SuppliersReport", c => c.String());
            DropForeignKey("dbo.Drugs", "Report_Id", "dbo.Reports");
            DropIndex("dbo.Drugs", new[] { "Report_Id" });
            DropColumn("dbo.Drugs", "Report_Id");
        }
    }
}
