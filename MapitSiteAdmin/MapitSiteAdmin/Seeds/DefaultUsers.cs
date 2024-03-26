using MapitSiteAdmin.Areas.Identity.Data;
using MapitSiteAdmin.Constants.Users;
using MapitSiteAdmin.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MapitSiteAdmin.Seeds
{
    public static class DefaultUsers
    {
        //Basic user:
        //Creates a User with Basic Role.
        //You can see that we are setting the email and username of this particular user in the code.
        //Once the user is created/seeded, we add the user to the Basic Role.
        public static async Task SeedBasicUserAsync(UserManager<MapitUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser1 = new MapitUser
            {
                UserName = "si.salah.safia@gmail.com",
                Email = "si.salah.safia@gmail.com",
                EmailConfirmed = true
            };
            var defaultUser2 = new MapitUser
            {
                UserName = "kawtar.marref@gmail.com",
                Email = "kawtar.marref@gmail.com",
                EmailConfirmed = true
            };
            var defaultUser3 = new MapitUser
            {
                UserName = "abdelfatah.yousfi@gmail.com",
                Email = "abdelfatah.yousfi@gmail.com",
                EmailConfirmed = true
            };

            var DefaultUsers = new List<MapitUser>();
            DefaultUsers.Add(defaultUser3);
            DefaultUsers.Add(defaultUser2);
            DefaultUsers.Add(defaultUser1);

            foreach (var defaultUser in DefaultUsers)
            {
                if (userManager.Users.All(u => u.Id != defaultUser.Id))
                {
                    var user = await userManager.FindByEmailAsync(defaultUser.Email);
                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultUser, "Ssi.salah.safia@gmail.com1");
                        await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    }
                }
            }

        }
        //Admin
        //Similarly, we create another user and add it to Basic, Admin, and SuperAdmin Roles.
        //Basically, this user is granted all roles.
        public static async Task SeedSuperAdminAsync(UserManager<MapitUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new MapitUser
            {
                UserName = "safia.sisalah@axinnov-dz.com",
                Email = "safia.sisalah@axinnov-dz.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Ssafia.sisalah@axinnov-dz.com1");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
                //Here we add claims to the SuperAdmin user.
                //The idea is that this super admin should have all permissions that exist in our system.
                //Remember defining Product Permissions?
                //We are going to build this seed in such a way that the SuperAdmin gets all of the product permissions.
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }
        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "Products");
        }
        //public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        //{
        //    var allClaims = await roleManager.GetClaimsAsync(role);
        //    var allPermissions = Permissions.GeneratePermissionsForModule(module);
        //    foreach (var permission in allPermissions)
        //    {
        //        if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
        //        {
        //            await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
        //        }
        //    }
        //}

    }
}
