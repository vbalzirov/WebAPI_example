using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.DAL.Models
{
    public class WeatherForecastDal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal TemperatureC { get; set; }

        public string Summary { get; set; }

        public bool IsDeleted { get; set; }

        // One To Many
        public ICollection<ForcastDetailesDal> ForcastDetailes { get; set; } = new List<ForcastDetailesDal>();    
    }

    public class ForcastDetailesDal
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
    }

    public class BookDal
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class AuthorDal
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }
    }

    // Many-To-Many Example
    public class BookAuthors
    {
        [Key]
        public int Id { get; set; }

        public AuthorDal Author { get; set; }

        public BookDal Book { get; set; }
    }
}
