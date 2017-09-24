using Microsoft.EntityFrameworkCore;
using TutorNinja.Models;

namespace TutorNinja.Data
{
    public class AdContext : DbContext
    {
        public AdContext(DbContextOptions<AdContext> options) : base(options)
        {
        }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>().ToTable("Ad");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}
