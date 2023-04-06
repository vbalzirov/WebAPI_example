using CompanyName.Application.Services.ProductService.Models;

namespace CompanyName.Application.Services.ProductService.Services
{
    public interface IOrdersService
    {
        Guid Guid { get; }

        Order Create(Order order);
        
        IEnumerable<Order> Get();
        
        Order Get(int id);
        
        void Update(Order model);

        void Delete(int id);
    }
}
