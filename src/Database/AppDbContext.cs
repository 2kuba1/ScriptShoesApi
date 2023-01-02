using Microsoft.EntityFrameworkCore;
using ScriptShoesAPI.Database.Entities;
using ScriptShoesCQRS.Database.Entities;

namespace ScriptShoesAPI.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> config) : base(config) { }

    public DbSet<Shoes> Shoes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<EmailCodes> EmailCodes { get; set; }
    public DbSet<Images> Images { get; set; }
    public DbSet<MainImages> MainImages { get; set; }
    public DbSet<Favorites> Favorites { get; set; }
    public DbSet<ShoeSizes> ShoeSizes { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
    public DbSet<ReviewsLikes> ReviewsLikes { get; set; }
    public DbSet<Cart> Cart { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Roles>()
            .HasData(new Roles()
            {
                Id = 1,
                Name = "User"
            }, new Roles()
            {
                Id = 2,
                Name = "Admin"
            });
    }
}