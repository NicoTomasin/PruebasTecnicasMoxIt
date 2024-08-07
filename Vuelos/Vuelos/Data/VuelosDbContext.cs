using Microsoft.EntityFrameworkCore;
using Vuelos.Models;

namespace Vuelos.Data
{
    public class VuelosDbContext : DbContext
    {
        public VuelosDbContext(DbContextOptions<VuelosDbContext> options)
            : base(options)
        {
        }
        public DbSet<VuelosModel> Vuelos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VuelosModel>().HasKey(x => x.Id);
        }
    }
}