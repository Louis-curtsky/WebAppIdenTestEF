using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using WebAppIdenTestEF.Identity;
using WebAppIdenTestEF.Models;

[assembly: OwinStartupAttribute(typeof(WebAppIdenTestEF.Startup))]
namespace WebAppIdenTestEF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            this.CreateRolesAndUsers();
        }

        private async void CreateRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var appDbContext = new ApplicationDbContext();
            var appUserStore = new AppUserStore(appDbContext);
            var userManager = new ApplicationUserManager(appUserStore);

            if (!roleManager.RoleExists("Admin"))
            {
                //i´ll assume database is seeded becouse i found roles
                IdentityRole role = new IdentityRole("Admin");
                IdentityResult result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception("Faild to create Role: " + result);
                }
                //seed in the following into the Db Admin
                if (userManager.FindByName("Admin")==null)
                {
                    string GenId = Guid.NewGuid().ToString();
                    ApplicationUser appUser = new ApplicationUser
                    {
                        Id = GenId,
                        UserName = "admin@gmail.se",
                        Email = "admin@gmail.se"
                    };
                    var userResult = userManager.Create(appUser, "Asdf!234");
                    if (!userResult.Succeeded)
                    {
                        throw new Exception("Faild to create User: " + userResult);
                    }

                        userManager.AddToRoleAsync(appUser.Id, role.Name).Wait();
                }
                appDbContext.SaveChanges();
            } // End of create Admin
            if (!roleManager.RoleExists("Manager"))
            //------ Seed into database -----------------------------------------------------------
            {
                IdentityRole role = new IdentityRole("Manager");
                IdentityResult result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception("Faild to create Role: " + result);
                }

                if (userManager.FindByName("Manager") == null)
                {
                    string GenId = Guid.NewGuid().ToString();
                    ApplicationUser appUser = new ApplicationUser
                    {
                        Id = GenId,
                        UserName = "manager@uas.se",
                        Email = "manager@uas.se"
                    };
                    IdentityResult userResult = await userManager.CreateAsync(appUser, "Asdf!234");
                    if (!userResult.Succeeded)
                    {
                        throw new Exception("Faild to create User: " + userResult);
                    }
                    IdentityResult resultAssign = userManager.AddToRoleAsync(appUser.Id, role.Name).Result;

                    if (!resultAssign.Succeeded)
                    {
                        throw new Exception($"Faild to grant {role.Name} role to Admin");
                    }
                }
            appDbContext.SaveChanges();
            } // End looing for Manager Roles
            if (!roleManager.RoleExists("Pilot"))
            //------ Seed into database -----------------------------------------------------------
            {
                IdentityRole role = new IdentityRole("Pilot");
                IdentityResult result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception("Faild to create Role: " + result);
                }

                if (userManager.FindByName("Pilot") == null)
                {
                    string GenId = Guid.NewGuid().ToString();
                    ApplicationUser appUser = new ApplicationUser
                    {
                        Id = GenId,
                        UserName = "pilot@uas.se",
                        Email = "pilot@uas.se"
                    };
                    IdentityResult userResult = await userManager.CreateAsync(appUser, "Asdf!234");
                    if (!userResult.Succeeded)
                    {
                        throw new Exception("Failed to create User: " + userResult);
                    }
                    IdentityResult resultAssign = userManager.AddToRoleAsync(appUser.Id, role.Name).Result;

                    if (!resultAssign.Succeeded)
                    {
                        throw new Exception($"Faild to grant {role.Name} role to Admin");
                    }
                }
            appDbContext.SaveChanges();
            } // End looing for User Roles
        }
    }
}
