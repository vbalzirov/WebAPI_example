using CompanyName.Application.Dal.Orders.Models;

namespace CompanyName.Application.Dal.Orders.Repositories
{
    public interface IOrdersDbRepository
    {
        OrderDal Create(OrderDal order);
        Task<IEnumerable<OrderDal>> GetAsync();
        Task<OrderDal> GetAsync(int id);
        Task Update(OrderDal model);
        Task DeleteAsync(int id);
    }
}