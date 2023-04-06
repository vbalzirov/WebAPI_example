using CompanyName.Application.Dal.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.Application.Dal.Orders.Contexts
{
    public class OrderContext : DbContext
    {
        public DbSet<OrderDal> Orders { get; private set; }

        public DbSet<ProductDal> Products { get; private set; }

        public DbSet<OrderProductDal> OrderProducts { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase("MarketDb");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Описание связи многие-ко-многим
            // Один Order
            // Связан со многими OrderProducts
            // по ключу OrderId
            builder.Entity<OrderProductDal>()
                .HasOne(op => op.Order)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(op => op.OrderId);
        }
    }
}
