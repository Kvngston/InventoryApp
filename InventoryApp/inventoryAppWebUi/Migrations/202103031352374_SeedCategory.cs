namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Data.Entity;
    using inventoryAppDomain.Entities;
    using inventoryAppDomain.IdentityEntities;

    public partial class SeedCategory : DbMigration
    {
        public override void Up()
        {
            ApplicationDbContext context = new ApplicationDbContext();

           var drugCategory = new List<DrugCategory>
            {
                new DrugCategory() { CategoryName = "Syrup" },
                new DrugCategory() { CategoryName = "Tablet" },
                new DrugCategory() { CategoryName = "Injectable" },
                new DrugCategory() { CategoryName = "Others" }
            };


            context.DrugCategories.AddRange(drugCategory);
            context.SaveChanges();
        }
        
        public override void Down()
        {
        }
    }
}
