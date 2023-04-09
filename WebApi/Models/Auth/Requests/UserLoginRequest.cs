using System.ComponentModel.DataAnnotations;

namespace CompanyName.Application.WebApi.OrdersApi.Models.Auth.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
