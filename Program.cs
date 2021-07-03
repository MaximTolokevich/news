using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using news.Models;
using news.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (NewsContext context = new NewsContext())
            {
                News first = new News { newsContent = "New1", DateNews = new System.DateTime(2015, 7, 20) };
                News second = new News { newsContent = "New2", DateNews = new System.DateTime(2011, 6, 21) };
                News third = new News { newsContent = "New3", DateNews = new System.DateTime(2013, 5, 22) };

                Author tom = new Author { FirstName = "Tom", LastName = "Ford", FullName = "Tom Ford" , Password="123",Email="asd"};
                Author nick = new Author { FirstName = "Nick", LastName = "Qwe", FullName = "Nick Qwe" , Password = "456" ,Email="fgh"};
                Author jack = new Author { FirstName = "Jack", LastName = "Rty", FullName = "Jack Rty" , Password = "789" ,Email="jkl"};



                Category Policy = new Category { CategoryName = "Policy" };
                Category Health = new Category { CategoryName = "Health" };
                Category Sport = new Category { CategoryName = "Sport" };

                context.News.AddRange(first, second, third);


                first.NewsAuthors.Add(tom);
                second.NewsAuthors.Add(nick);
                third.NewsAuthors.Add(jack);

                first.Category = Policy;
                second.Category = Sport;
                third.Category = Health;

                context.SaveChanges();

            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
