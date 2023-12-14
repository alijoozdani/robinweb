using Microsoft.EntityFrameworkCore;
using RobinWeb.DataLayer.Entities;
using System.Data;

namespace RobinWeb.DataLayer.Context
{
    public class RobinWebDBContext : DbContext
    {
        public RobinWebDBContext(DbContextOptions<RobinWebDBContext> options) : base(options)
        {
        }

        public DbSet<User> UsersTbl { get; set; }
        public DbSet<Slider> SlidersTbl { get; set; }
        public DbSet<Product> ProductsTbl { get; set; }
        public DbSet<Blog> BlogsTbl { get; set; }
        public DbSet<ContactUsForm> ContactUsFormsTbl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<Blog>().HasQueryFilter(b => !b.IsDelete);

            modelBuilder.Entity<Blog>().HasQueryFilter(b => !b.IsDelete);

            modelBuilder.Entity<Blog>().HasQueryFilter(b => !b.IsDelete);

            modelBuilder.Entity<Blog>().HasQueryFilter(c => !c.IsDelete);
           
            base.OnModelCreating(modelBuilder);
        }

    }
}
