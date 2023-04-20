using CompanyName.Application.Services.ProductService.Models;

namespace CompanyName.Application.Services.ProductService.Services
{
    public interface IOrdersService
    {
        Guid Guid { get; }

        Order Create(Order order);

        Task<IEnumerable<Order>> GetAsync();

        Task<Order> GetAsync(int id);

        void Update(Order model);

        Task DeleteAsync(int id);
    }
}
