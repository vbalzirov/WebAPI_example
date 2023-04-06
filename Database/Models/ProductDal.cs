using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyName.Application.Dal.Orders.Models
{
    [Table("Products")]
    public class ProductDal
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
