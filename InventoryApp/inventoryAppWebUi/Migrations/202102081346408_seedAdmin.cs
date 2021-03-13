namespace inventoryAppWebUi.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdmin : DbMigration
    {
        public override void Up()
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var user = new IdentityUser()
            {
                UserName = "Admin@admin.com",
                Email = "Admin@admin.com",
                EmailConfirmed = true,
                // TwoFactorEnabled = true,
            };

            var result = userManager.Create(user, "Admin1234_");

            {
                if (result.Succeeded)
                {
                    var admin = new IdentityRole("Admin");
                    var roleResult = roleManager.Create(admin);

                    {
                        if (roleResult.Succeeded)
                        {
                            userManager.AddToRole(user.Id, "Admin");
                        }
                    }
                }
            }

        }

        public override void Down()
        {
        }
    }
}
