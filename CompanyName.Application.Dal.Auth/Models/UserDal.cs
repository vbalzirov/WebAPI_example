using System.ComponentModel.DataAnnotations;

namespace CompanyName.Application.Dal.Auth.Models
{
    public class UserDal
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;        

        public int Role { get; set; }
    }
}
