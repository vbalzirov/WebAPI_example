using CompanyName.Application.Dal.Orders.Contexts;
using CompanyName.Application.Dal.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.Application.Dal.Orders.Repositories
{
    public class OrdersDbRepository : IOrdersDbRepository
    {
        private readonly OrderContext context;

        public OrdersDbRepository(OrderContext orderContext)
        {
            context = orderContext;
            FillData();
        }

        public OrderDal Create(OrderDal order)
        {
            context.Orders.Add(order);

            context.SaveChanges();

            return order;
        }

        public async Task<IEnumerable<OrderDal>> GetAsync()
        {
            return await context.Orders
                .Include(o => o.OrderProducts)
                .ToListAsync();
        }

        public async Task<OrderDal> GetAsync(int id)
        {
            return await context.Orders
                .Include(o => o.OrderProducts)
                .SingleAsync(t => t.Id == id);
        }

        public async Task Update(OrderDal model)
        {
            var existingModel = await context.Orders
                .SingleAsync(t => t.Id == model.Id);

            existingModel.Number = model.Number;
            existingModel.IssueDate = model.IssueDate;
            //existingModel.OrderProducts = model.OrderProducts;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = context.Orders
                   .Single(t => t.Id == id);

            context.Remove(order);
            await context.SaveChangesAsync();
        }

        private void FillData()
        {
            var product1 = new ProductDal
            {
                Name = "Apple"
            };

            var product2 = new ProductDal
            {
                Name = "Milk"
            };

            var product3 = new ProductDal
            {
                Name = "T-Shirt"
            };

            context.Products.AddRange(product1, product2, product3);

            var order1 = new OrderDal
            {
                Number = "000001-1",
                IssueDate = DateTimeOffset.UtcNow
            };

            var order2 = new OrderDal
            {
                Number = "000001-2",
                IssueDate = DateTimeOffset.UtcNow
            };

            context.Orders.AddRange(order1, order2);

            context.OrderProducts.AddRange(
                new OrderProductDal
                {
                    Order = order1,
                    Product = product1,
                    ProductQuantity = 1.5m,
                },
                new OrderProductDal
                {
                    Order = order1,
                    Product = product2,
                    ProductQuantity = 1,
                },
                new OrderProductDal
                {
                    Order = order2,
                    Product = product1,
                    ProductQuantity = 3.5m,
                },
                new OrderProductDal
                {
                    Order = order2,
                    Product = product2,
                    ProductQuantity = 5,
                },
                new OrderProductDal
                {
                    Order = order2,
                    Product = product3,
                    ProductQuantity = 1,
                });

            context.SaveChanges();
        }
    }
}
