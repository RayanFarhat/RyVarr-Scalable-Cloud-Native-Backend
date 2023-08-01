// using Microsoft.EntityFrameworkCore;
// using AuthService.DTOs;

// namespace AuthService.DB;


// public class RyvarrDb : DbContext
// {
//     // every DbSet is for a table in ryvarrdb
//     public DbSet<User> users { get; set; }

//     public RyvarrDb(DbContextOptions<RyvarrDb> options) : base(options)
//     {
//         users = Set<User>();
//     }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         //db here is pointed to the ip that docker assign to postgre
//         optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=ryvarrdb;Username=ryan;Password=ryan");
//     }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);

//         modelBuilder.Entity<User>().ToTable("users");
//         // modelBuilder.Entity<User>().HasKey(u => new { u.id, u.username });
//         // modelBuilder.UseSerialColumns();
//         // modelBuilder.UseIdentityColumns();
//         // modelBuilder.Entity<User>().Property(u => u.id).UseIdentityAlwaysColumn();
//     }

//     public async Task Add(UserForRegister user)
//     {
//         User u = new User(id: 0, username: user.username, email: user.email, password: user.password);
//         await this.users.AddAsync(u);
//         await this.SaveChangesAsync();
//     }
//     public async Task<User> Get(string id)
//     {
//         return await this.users.FindAsync(id);
//     }
//     public async Task<User> GetByUsername(string username)
//     {
//         return await this.users.FindAsync(username);
//     }
//     public async Task<IEnumerable<User>> GetAll()
//     {
//         return await this.users.ToListAsync();
//     }
//     public async Task Update(User user)
//     {
//         this.users.Update(user);
//         await this.SaveChangesAsync();
//     }
//     public async Task Delete(string id)
//     {
//         var user = await this.users.FindAsync(id);
//         if (user == null)
//             return;

//         this.users.Remove(user);
//         await this.SaveChangesAsync();
//     }
// }