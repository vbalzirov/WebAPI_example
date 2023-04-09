using CompanyName.Application.Dal.Auth.Models;

namespace CompanyName.Application.Dal.Auth.Repository
{
    public interface IAuthRepository
    {
        int AddUser(UserDal user);
    }
}