
using AutoMapper;
using news.Repositories;
using news.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace news.Services
{
    public class AuthorService : IService<Author>
    {
        private readonly IMapper map;
        private readonly IRepository<Repositories.Models.Author> rep;

        public AuthorService(IRepository<Repositories.Models.Author> repository,IMapper mapper)
        {
            rep = repository;
            map = mapper;
        }

        public bool Create(Author item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }
            var a = map.Map<Author,
                Repositories.Models.Author>
                (item);
            return rep.Create(a);
        }

        public bool Delete(int Id) => rep.Delete(Id);

        public Author Get(int Id)
        {
            var a = map.Map<Repositories.Models.Author,
                Author>
                (rep.Get(Id));
            return a;
        }

        public IEnumerable<Author> GetAll()
        {
            var a = map.Map<IEnumerable<Repositories.Models.Author>,
                IEnumerable<Author>>
                (rep.GetAll());
            return a;
        }

        public bool Update(Author item)
        {
            var a = map.Map<Author,
                Repositories.Models.Author>
                (item);
            return rep.Update(a);
        }
    }
}
