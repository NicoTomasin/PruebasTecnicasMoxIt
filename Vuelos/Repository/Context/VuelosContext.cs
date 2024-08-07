using Microsoft.EntityFrameworkCore;
using Vuelos.Models;

namespace Repository.Context
{
    public class VuelosContext : DbContext
    {
        public VuelosContext(DbContextOptions<VuelosContext> options) : base(options) { }
        public DbSet<VuelosModel> Vuelo { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VuelosModel>().HasKey(k => k.Id);
        }
    }
}
