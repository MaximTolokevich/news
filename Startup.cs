using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using news.Extensions.Mapping;
using news.Repositories;
using news.Repositories.Data;
using news.Repositories.Models;
using news.Services;

namespace news
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
                    mc.AddProfile(new MappingProfile())
                );
            mappingConfig.AssertConfigurationIsValid();
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllersWithViews();
            services.AddDbContext<NewsContext>(options => options.EnableSensitiveDataLogging()
            .EnableServiceProviderCaching(false)
            .UseMySql(Configuration.GetConnectionString("news"), new MySqlServerVersion(new System.Version(8, 0, 24))),
            ServiceLifetime.Transient
            );
            services.AddScoped<IRepository<Author>, AuthorRepository>();
            services.AddScoped<IService<Services.Models.Author>, AuthorService>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IService<Services.Models.Category>, CategoryService>();
            services.AddScoped<IRepository<News>, NewsRepository>();
            services.AddScoped<IService<Services.Models.News>, NewsService>();

            services.AddDefaultIdentity<IdentityUser>(opt => opt.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<NewsContext>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => //CookieAuthenticationOptions
        {
            options.LoginPath = new PathString("/Account/Login");
        });
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseAuthentication();
                app.UseAuthorization();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=DefaultPage}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
