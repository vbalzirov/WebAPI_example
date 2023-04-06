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

        public OrdersService(IOrdersDbRepository orderRepository, IMapper autoMapper)
        {
            repository = orderRepository;
            mapper = autoMapper;
        }

        public Order Create(Order order) 
        {
            var orderDal = mapper.Map<Order, OrderDal>(order);
            var resultDal = repository.Create(orderDal);

            return mapper.Map<OrderDal, Order>(resultDal);
        }

        public IEnumerable<Order> Get()
        {
            var ordersDal = repository.Get();
            var result = mapper.Map<IEnumerable<OrderDal>, IEnumerable<Order>>(ordersDal);
            return result;
        }

        public Order Get(int id) 
        {
            var orderDal = repository.Get(id);
            return mapper.Map<OrderDal, Order>(orderDal);
        }
        
        public void Update(Order order) 
        {
            var orderDal = mapper.Map<Order, OrderDal>(order);
            repository.Update(orderDal);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
