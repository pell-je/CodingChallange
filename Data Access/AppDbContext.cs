using ApiNamespace.Controllers;
using JobTargetCodingChallange.Domain.Item;
using JobTargetCodingChallange.Entity;
using JobTargetCodingChallange.Entity.Sale;
using Microsoft.EntityFrameworkCore;

namespace JobTargetCodingChallange
{
    public class AppDbContext : DbContext
    {

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.ShippingInfo);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

    }
}
