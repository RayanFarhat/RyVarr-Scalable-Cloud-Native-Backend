using Microsoft.EntityFrameworkCore;
using AuthService.DTOs;

namespace AuthService.DB;


public class RyvarrDb : DbContext
{
    // every DbSet is for a table in ryvarrdb
    public DbSet<User> users { get; set; }

    public RyvarrDb(DbContextOptions<RyvarrDb> options) : base(options)
    {
        users = Set<User>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //db here is pointed to the ip that docker assign to postgre
        optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=ryvarrdb;Username=ryan;Password=ryan");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");

        modelBuilder.UseSerialColumns();
        modelBuilder.UseIdentityColumns();
        // modelBuilder.Entity<User>().Property(u => u.id).UseIdentityAlwaysColumn();
    }

}