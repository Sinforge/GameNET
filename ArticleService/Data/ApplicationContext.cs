using ArticleService.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }


    }
}
