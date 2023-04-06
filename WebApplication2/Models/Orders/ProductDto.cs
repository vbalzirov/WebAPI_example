namespace CompanyName.Application.WebApi.OrdersApi.Models.Orders
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal ProductQuantity { get; set; }
    }
}
