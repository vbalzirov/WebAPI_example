using System.ComponentModel.DataAnnotations;

namespace CompanyName.Application.WebApi.OrdersApi.Models.Auth.Requests
{
    public class UserRegisterRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
