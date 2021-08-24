
using AutoMapper;
using news.Repositories;
using news.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace news.Services
{
    public class NewsService : IService<News>
    {
        private readonly IMapper _map;
        private readonly IRepository<Repositories.Models.News> _rep;
        public NewsService(IRepository<Repositories.Models.News> repository, IMapper mapper)
        {
            _map = mapper;
            _rep = repository; 
        }
        public bool Create(News item)
        {
           var a = _map.Map<News,
               Repositories.Models.News>
               (item);
           return _rep.Create(a);
        }

        public bool Delete(int Id)
        {
            return _rep.Delete(Id);
        }

        public News Get(int Id)
        {
            var a = _map.Map< Repositories.Models.News,
                News > 
                (_rep.Get(Id));
            return a;
        }

        public IEnumerable<News> GetAll()
        {
            var a = _map.Map<IEnumerable<Repositories.Models.News>,
                IEnumerable<News>>
                (_rep.GetAll());
            return a;
        }

        public bool Update(News item)
        {
            var a = _map.Map<News,
                Repositories.Models.News>
                (item);
            return _rep.Update(a);
        }
    }
}
