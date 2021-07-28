using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using news.Controllers.Models;
using news.Repositories.Models;
using news.Repositories.Data;
using System.Collections.Generic;

namespace news
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
