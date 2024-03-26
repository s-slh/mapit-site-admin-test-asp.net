using MapitSiteAdmin.Areas.Identity.Data;
using MapitSiteAdmin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MapitSiteAdmin.Areas.Identity.Data;

public class mapitDbContext : IdentityDbContext<MapitUser>
{

    //ajouter les tables:
    public DbSet<Project> Projects { get; set; }
    public DbSet<Collaboration> ProjectUser { get; set; }
    public DbSet<Diagram> Diagrams { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<GroupUser> GroupUser { get; set; }
    public DbSet<Group> Group { get; set; }
    public DbSet<Category> Category { get; set; }
    
    public mapitDbContext(DbContextOptions<mapitDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
