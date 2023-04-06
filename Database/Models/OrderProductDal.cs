using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyName.Application.Dal.Orders.Models
{
    public class OrderProductDal
    {
        public int Id { get; set; }

        public decimal ProductQuantity { get; set; }

        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual OrderDal Order { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual ProductDal Product { get; set; }
    }
}
