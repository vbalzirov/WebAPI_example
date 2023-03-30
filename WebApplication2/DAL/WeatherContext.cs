using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL.Models;

namespace WebApplication2.DAL
{
    public class WeatherContext : DbContext
    {
        public DbSet<WeatherForecastDal> Forcasts { get; set; }
        
        public DbSet<ForcastDetailesDal> ForcastDetailes { get; set; }
        
        public DbSet<AuthorDal> Authors { get; set; }
        
        public DbSet<BookDal> Books { get; set; }

        public DbSet<BookAuthors> BookAuthors { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //builder.UseSqlServer("sqlConnectionString");
            builder.UseInMemoryDatabase("WeatherDb");
        }
    }
}
