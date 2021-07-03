using Microsoft.EntityFrameworkCore;
using news.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news.Repository
{
    public class NewsContext:DbContext

    {
        public DbSet<News> News { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public NewsContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=admin;database=News;",
                new MySqlServerVersion(new Version(8, 0, 24))
            );
        }
    }
}
