using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BackendServer.DTOs;
using Microsoft.AspNetCore.Identity;

namespace BackendServer.Authentication;
public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<AcountData> acountData { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        acountData = Set<AcountData>();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().ToTable(name: "Roles");
        builder.Entity<IdentityUser>().ToTable(name: "Users");
        builder.Entity<IdentityUserRole<string>>().ToTable(name: "UserRoles");
        // adding Indexing
        builder.Entity<AcountData>().ToTable("AcountData")
            .HasIndex(p => p.Id)
            .IsUnique();
    }
}