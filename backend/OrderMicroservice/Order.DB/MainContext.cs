using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.DB.Entities; // Добавлено пространство имен для DbOrder и DbOrderItems
using Microsoft.EntityFrameworkCore;

namespace Order.DB
{
    public class MainContext : DbContext
    {
        public MainContext() { }
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }

        public DbSet<DbOrder> Orders { get; set; } // Добавлен DbSet для заказов
        public DbSet<DbOrderItems> OrderItems { get; set; } // Добавлен DbSet для элементов заказа

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DbOrder>()
                .HasQueryFilter(o => o.Deleted == null);

            modelBuilder.Entity<DbOrderItems>()
                .HasQueryFilter(oi => oi.Deleted == null);

            // Определение связи между заказами и элементами заказа
            modelBuilder.Entity<DbOrder>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.DbOrder)
                .HasForeignKey(oi => oi.OrderID);
        }
    }
}
