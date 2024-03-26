using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MapitSiteAdmin.Areas.Identity.Data;
using MapitSiteAdmin.Seeds;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Authorization;
using MapitSiteAdmin.Permission;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("mapitDbContextConnection") ?? throw new InvalidOperationException("Connection string 'mapitDbContextConnection' not found.");

builder.Services.AddDbContext<mapitDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<MapitUser> (options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<mapitDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();

//add autorisation policy based
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();


var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("app");
        try
        {
            var userManager = services.GetRequiredService<UserManager<MapitUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await DefaultRoles.SeedAsync(userManager, roleManager);
            await DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
            await DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
            logger.LogInformation("Finished Seeding Default Data");
            logger.LogInformation("Application Starting");
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "An error occurred seeding the DB");
        }
    }


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
