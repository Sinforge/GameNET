using GameHub.Models;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }


    }
}
