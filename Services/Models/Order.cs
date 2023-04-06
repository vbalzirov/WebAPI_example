namespace CompanyName.Application.Services.ProductService.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public DateTimeOffset IssueDate { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
