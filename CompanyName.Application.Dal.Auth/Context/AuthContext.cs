using CompanyName.Application.Core.Configurations;
using CompanyName.Application.Dal.Auth.Configurations;
using CompanyName.Application.Dal.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.Application.Dal.Orders.Contexts
{
    public class AuthContext : IdentityDbContext
    {
        private readonly IAuthRepositorySettings settings;
        
        public DbSet<UserDal> Users { get; private set; }

        public AuthContext(IAuthRepositorySettings repositorySettings) : base()
        {
            settings = repositorySettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (settings.IsInMemory)
            {
                builder.UseInMemoryDatabase(settings.DatabaseName);
            }
            else
            {
                builder.UseSqlServer(settings.ConnectionString);
            }
        }
    }
}
