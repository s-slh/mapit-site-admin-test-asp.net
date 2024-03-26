using MapitSiteAdmin.Areas.Identity.Data;
using MapitSiteAdmin.Constants.Users;
using Microsoft.AspNetCore.Identity;

namespace MapitSiteAdmin.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<MapitUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }
}
