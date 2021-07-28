using AutoMapper;
using news.Repositories;
using news.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace news.Services
{
    public class CategoryService : IService<Category>
    {
        private readonly IRepository<Repositories.Models.Category> rep;
        private readonly IMapper map;

        public CategoryService(IRepository<Repositories.Models.Category> repository, IMapper mapper)
        {
            rep = repository;
            map = mapper;
        }

        public bool Create(Category item)
        {
            var a = map.Map<Category,
                Repositories.Models.Category>(item);
            return rep.Create(a);
        }

        public bool Delete(int Id) => rep.Delete(Id);

        public Category Get(int Id)
        {
            var a = map.Map<Repositories.Models.Category,
                Category>(rep.Get(Id));
            return a;
        }

        public IEnumerable<Category> GetAll()
        {
            var a = map.Map<IEnumerable<Repositories.Models.Category>,
                IEnumerable<Category>>(rep.GetAll());
            return a;
        }

        public bool Update(Category item)
        {
            var a = map.Map<Category,
                Repositories.Models.Category>(item);
            return rep.Update(a);
        }
    }
}
