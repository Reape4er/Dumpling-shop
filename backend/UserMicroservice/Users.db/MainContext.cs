using Microsoft.EntityFrameworkCore;
using Users.db.Entities;

namespace Users.db
{
    public class MainContext : DbContext
    {
        public MainContext() { }
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        public DbSet<DbUser> Users {  get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbUser>()
                .HasIndex(user=>user.Email)
                .IsUnique();

            modelBuilder.Entity<DbUser>()
                .HasQueryFilter(u => u.Deleted == null);
        }
    }
}
