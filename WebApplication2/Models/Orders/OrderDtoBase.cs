namespace CompanyName.Application.WebApi.OrdersApi.Models.Orders
{
    public abstract class OrderDtoBase
    {
        public string Number { get; set; } = null!;

        public DateTimeOffset IssueDate { get; set; }

        public ICollection<ProductDto> OrderProducts { get; set; } = new List<ProductDto>();
    }
}
