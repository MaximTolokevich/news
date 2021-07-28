using Microsoft.EntityFrameworkCore;
using news.Repositories.Models;
using System;

namespace news.Repositories.Data
{
    public class NewsContext:DbContext

    {
        public DbSet<News> News { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public NewsContext(DbContextOptions<NewsContext> options):base(options)
        {
            
        }
    }
}
