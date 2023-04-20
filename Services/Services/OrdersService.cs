using CompanyName.Application.Dal.Orders.Repositories;
using CompanyName.Application.Services.ProductService.Models;
using AutoMapper;
using CompanyName.Application.Dal.Orders.Models;

namespace CompanyName.Application.Services.ProductService.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersDbRepository repository;
        private readonly IMapper mapper;

        public Guid Guid { get; private set; }

        public OrdersService(IOrdersDbRepository orderRepository, IMapper autoMapper)
        {
            repository = orderRepository;
            mapper = autoMapper;
            Guid = Guid.NewGuid();
        }

        public Order Create(Order order) 
        {
            var orderDal = mapper.Map<Order, OrderDal>(order);
            var resultDal = repository.Create(orderDal);

            return mapper.Map<OrderDal, Order>(resultDal);
        }

        public async Task<IEnumerable<Order>> GetAsync()
        {
            var ordersDal = await repository.GetAsync();
            var result = mapper.Map<IEnumerable<OrderDal>, IEnumerable<Order>>(ordersDal);
            return result;
        }

        public async Task<Order> GetAsync(int id) 
        {
            var orderDal = await repository.GetAsync(id);
            return mapper.Map<OrderDal, Order>(orderDal);
        }
        
        public void Update(Order order) 
        {
            var orderDal = mapper.Map<Order, OrderDal>(order);
            repository.Update(orderDal);
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(id);
        }
    }
}
