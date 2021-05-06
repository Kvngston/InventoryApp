using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace inventoryAppWebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationRoles : DbMigration
    {
        public override void Up()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var pharmacist = new IdentityRole("Pharmacist");
            var storeManager = new IdentityRole("StoreManager");
            roleManager.Create(pharmacist);
            roleManager.Create(storeManager);

        }
        
        public override void Down()
        {
        }
    }
}
