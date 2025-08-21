using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Coupen> Coupen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Coupen>().HasData(
               new Coupen() { Id=1, ProductName="iPhone X", Description="Iphone Description", Amount=120  },
               new Coupen() { Id = 2, ProductName = "Samsung s25", Description = "samsung Description", Amount = 10 }
               );
        }
    }
}
