namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
           
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SupplierBrands",
                c => new
                    {
                        Supplier_Id = c.Int(nullable: false),
                        Brand_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Supplier_Id, t.Brand_Id });
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Drugs", "BrandId", c => c.Int(nullable: false));
            AlterColumn("dbo.Suppliers", "TagNumber", c => c.String());
            AlterColumn("dbo.Suppliers", "Website", c => c.String());
            AlterColumn("dbo.Suppliers", "Email", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierName", c => c.String());
            DropColumn("dbo.Suppliers", "Status");
            CreateIndex("dbo.SupplierBrands", "Brand_Id");
            CreateIndex("dbo.SupplierBrands", "Supplier_Id");
            CreateIndex("dbo.Drugs", "BrandId");
            AddForeignKey("dbo.Drugs", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SupplierBrands", "Brand_Id", "dbo.Brands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SupplierBrands", "Supplier_Id", "dbo.Suppliers", "Id", cascadeDelete: true);
        }
    }
}
