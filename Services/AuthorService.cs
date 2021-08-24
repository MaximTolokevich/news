
using AutoMapper;
using news.Repositories;
using news.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace news.Services
{
    public class AuthorService : IService<Author>
    {
        private readonly IMapper _map;
        private readonly IRepository<Repositories.Models.Author> _rep;

        public AuthorService(IRepository<Repositories.Models.Author> repository,IMapper mapper)
        {
            _rep = repository;
            _map = mapper;
        }

        public bool Create(Author item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }
            var a = _map.Map<Author,
                Repositories.Models.Author>
                (item);
            return _rep.Create(a);
        }

        public bool Delete(int Id) => _rep.Delete(Id);

        public Author Get(int Id)
        {
            var a = _map.Map<Repositories.Models.Author,
                Author>
                (_rep.Get(Id));
            return a;
        }

        public IEnumerable<Author> GetAll()
        {
            var a = _map.Map<IEnumerable<Repositories.Models.Author>,
                IEnumerable<Author>>
                (_rep.GetAll());
            return a;
        }

        public bool Update(Author item)
        {
            var a = _map.Map<Author,
                Repositories.Models.Author>
                (item);
            return _rep.Update(a);
        }
    }
}
