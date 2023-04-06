using CompanyName.Application.WebApi.OrdersApi.Models.Orders;

namespace CompanyName.Application.WebApi.ProductApi.Models.Orders.Responses
{
    public class GetOrderResponse : OrderDtoBase
    {
        public int Id { get; set; }
    }
}
