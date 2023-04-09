using CompanyName.Application.Dal.Auth.Models;
using CompanyName.Application.Dal.Orders.Contexts;

namespace CompanyName.Application.Dal.Auth.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthContext context;

        public AuthRepository(AuthContext authContext)
        {
            context = authContext;
        }

        public int AddUser(UserDal user)
        {
            context.Users.Add(user);
            context.SaveChanges();

            return user.Id;
        }
    }
}
