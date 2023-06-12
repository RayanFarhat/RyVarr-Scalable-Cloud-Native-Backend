using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using AuthService.DTOs;

namespace AuthService.DB;


public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        Users = Set<User>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=ryvarrdb;Username=ryan;Password=ryan");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
    }

    public void EnsureUsersTableCreated()
    {
        string createTableSql = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id int PRIMARY KEY,
                    Username varchar(255),
                    Email varchar(255),
                    Password varchar(255)
                )";

        Database.ExecuteSqlRaw(createTableSql);
    }
}