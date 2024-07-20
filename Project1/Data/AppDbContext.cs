using Microsoft.EntityFrameworkCore;
using TipTracker.Models;

namespace TipTracker.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tip> Tips { get; set; }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=TipTrackerDb;User Id=sa;Password=NotPasswork123!;TrustServerCertificate=true;");
        }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tip>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");
        }
}
}


