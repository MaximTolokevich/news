
using AutoMapper;
using news.Repositories;
using news.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace news.Services
{
    public class NewsService : IService<News>
    {
        private readonly IMapper map;
        private readonly IRepository<Repositories.Models.News> rep;
        public NewsService(IRepository<Repositories.Models.News> repository, IMapper mapper)
        {
            map = mapper;
            rep = repository; 
        }
        public bool Create(News item)
        {
           var a = map.Map<News,
               Repositories.Models.News>
               (item);
           return  rep.Create(a);
        }

        public bool Delete(int Id)
        {
            return rep.Delete(Id);
        }

        public News Get(int Id)
        {
            var a = map.Map< Repositories.Models.News,
                News > 
                (rep.Get(Id));
            return a;
        }

        public IEnumerable<News> GetAll()
        {
            var a = map.Map<IEnumerable<Repositories.Models.News>,
                IEnumerable<News>>
                (rep.GetAll());
            return a;
        }

        public bool Update(News item)
        {
            var a = map.Map<News,
                Repositories.Models.News>
                (item);
            return rep.Update(a);
        }
    }
}
