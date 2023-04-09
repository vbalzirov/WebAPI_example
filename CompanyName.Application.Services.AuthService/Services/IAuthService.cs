using CompanyName.Application.Services.AuthService.Models;

namespace CompanyName.Application.Services.AuthService.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterUser(UserRegister userregister);

        Task<AuthResult> LoginUser(UserLogin userLogin);
    }
}