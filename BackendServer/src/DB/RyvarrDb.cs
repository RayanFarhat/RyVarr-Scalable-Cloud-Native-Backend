using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BackendServer.DTOs;
using Microsoft.AspNetCore.Identity;
using BackendServer.Authentication;

namespace BackendServer.DB;
public class RyvarrDb : IdentityDbContext
{
    public DbSet<AccountData> AccountData { get; set; }
    public DbSet<ContactData> ContactData { get; set; }

    public RyvarrDb(DbContextOptions<RyvarrDb> options) : base(options)
    {
        AccountData = Set<AccountData>();
        ContactData = Set<ContactData>();
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

        builder.Entity<ContactData>().ToTable("ContactData").HasIndex(p => p.Id).IsUnique(); ;
    }

    public async Task AddContactData(ContactModel data)
    {
        Guid id = Guid.NewGuid();
        ContactData u = new(Id: id.ToString(), Name: data.Name, Email: data.Email, Company: data.Company, Message: data.Message);
        await this.ContactData.AddAsync(u);
        await this.SaveChangesAsync();
    }
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