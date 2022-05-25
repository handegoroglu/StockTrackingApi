using Microsoft.EntityFrameworkCore;
using StockTrackingApi.Models;

namespace StockTrackingApi
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=ProductsDB.db;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(x =>
            {
                x.HasIndex(y => y.Barcode).IsUnique();
            });

        }
    }
}
