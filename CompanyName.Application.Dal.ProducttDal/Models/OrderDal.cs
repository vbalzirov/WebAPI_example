using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyName.Application.Dal.Orders.Models
{
    [Table("Orders")]
    public class OrderDal
    {
        [Key]
        public int Id { get; set; }

        public string Number { get; set; } = null!;

        public DateTimeOffset IssueDate { get; set; }

        public virtual ICollection<OrderProductDal> OrderProducts { get; set; } = new List<OrderProductDal>();
    }
}
