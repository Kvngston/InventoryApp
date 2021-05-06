namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPrescribedDrugsToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrescribedDrugs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrugName = c.String(nullable: false),
                        TotalDosage = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrescriptionDuration = c.Int(nullable: false),
                        MorningDosage = c.Int(nullable: false),
                        AfternoonDosage = c.Int(nullable: false),
                        EveningDosage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PrescribedDrugs");
        }
    }
}
