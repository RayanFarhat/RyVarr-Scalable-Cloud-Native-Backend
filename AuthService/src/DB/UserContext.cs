using Microsoft.EntityFrameworkCore;
using AuthService.DTOs;

namespace AuthService.DB;


public class UserContext : DbContext
{
    public DbSet<User> users { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        users = Set<User>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=ryvarrdb;Username=ryan;Password=ryan");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
        modelBuilder.Entity<User>().ToTable("users");
    }

}