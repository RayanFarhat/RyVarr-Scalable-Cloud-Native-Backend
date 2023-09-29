using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BackendServer.DTOs;
using Microsoft.AspNetCore.Identity;

namespace BackendServer.DB;
public class RyvarrDb : IdentityDbContext
{
    public DbSet<AccountData> AccountData { get; set; }
    public RyvarrDb(DbContextOptions<RyvarrDb> options) : base(options)
    {
        AccountData = Set<AccountData>();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().ToTable(name: "Roles");
        builder.Entity<IdentityUser>().ToTable(name: "Users");
        builder.Entity<IdentityUserRole<string>>().ToTable(name: "UserRoles");
        // adding Indexing
        builder.Entity<AccountData>().ToTable("AcountData")
            .HasIndex(p => p.Id)
            .IsUnique();
    }

    // public async Task Add(AccountData user)
    // {
    //     AccountData u = new User(id: 0, username: user.username, email: user.email, password: user.password);
    //     await this.users.AddAsync(u);
    //     await this.SaveChangesAsync();
    // }
    // public async Task<AccountData> Get(string id)
    // {
    //     return await this.accountData.FindAsync(id);
    // }
    // public async Task<AccountData> GetByUsername(string username)
    // {
    //     return await this.accountData.FindAsync(username);
    // }
    public async Task<IEnumerable<AccountData>> GetAll()
    {
        return await this.AccountData.ToListAsync();
    }
    public async Task Update(AccountData user)
    {
        this.AccountData.Update(user);
        await this.SaveChangesAsync();
    }
    public async Task Delete(string id)
    {
        var user = await this.AccountData.FindAsync(id);
        if (user == null)
            return;

        this.AccountData.Remove(user);
        await this.SaveChangesAsync();
    }


}