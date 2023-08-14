using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BackendServer.DTOs;

namespace BackendServer.Authentication;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<AcountData> acountData { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        acountData = Set<AcountData>();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ApplicationUser>(entity =>
       {
           entity.ToTable(name: "Users");
           entity.Property(e => e.Id).HasColumnName("UserId");

       });
        builder.Entity<AcountData>().ToTable("AcountData");
    }
}