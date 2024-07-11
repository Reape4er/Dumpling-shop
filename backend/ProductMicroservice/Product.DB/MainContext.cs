using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Product.DB.Entities;
using Microsoft.EntityFrameworkCore;


namespace Product.DB
{
    public class MainContext : DbContext
    {
        public MainContext() { }
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        public DbSet<DbProduct> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbProduct>()
                .HasQueryFilter(u => u.Deleted == null);
        }
    }
}
