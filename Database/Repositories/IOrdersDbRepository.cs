using CompanyName.Application.Dal.Orders.Models;

namespace CompanyName.Application.Dal.Orders.Repositories
{
    public interface IOrdersDbRepository
    {
        OrderDal Create(OrderDal order);
        IEnumerable<OrderDal> Get();
        OrderDal Get(int id);
        void Update(OrderDal model);

        void Delete(int id);
    }
}