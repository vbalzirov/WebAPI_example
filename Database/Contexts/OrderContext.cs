using CompanyName.Application.Core.Configurations;
using CompanyName.Application.Dal.Orders.Configuratioin;
using CompanyName.Application.Dal.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.Application.Dal.Orders.Contexts
{
    public class OrderContext : DbContext
    {
        private readonly IOrderRepositorySettings settings;

        public DbSet<OrderDal> Orders { get; private set; }

        public DbSet<ProductDal> Products { get; private set; }

        public DbSet<OrderProductDal> OrderProducts { get; private set; }

        public OrderContext(IOrderRepositorySettings orderRepositorySettings) : base()
        {
            settings = orderRepositorySettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (settings.IsInMemory)
            {
                builder.UseInMemoryDatabase(settings.DatabaseName);
            }
            else
            {
                builder.UseSqlServer(settings.ConnectionString);
            }
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
