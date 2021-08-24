using AutoMapper;
using news.Repositories;
using news.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace news.Services
{
    public class CategoryService : IService<Category>
    {
        private readonly IRepository<Repositories.Models.Category> _rep;
        private readonly IMapper _map;

        public CategoryService(IRepository<Repositories.Models.Category> repository, IMapper mapper)
        {
            _rep = repository;
            _map = mapper;
        }

        public bool Create(Category item)
        {
            var a = _map.Map<Category,
                Repositories.Models.Category>(item);
            return _rep.Create(a);
        }

        public bool Delete(int Id) => _rep.Delete(Id);

        public Category Get(int Id)
        {
            var a = _map.Map<Repositories.Models.Category,
                Category>(_rep.Get(Id));
            return a;
        }

        public IEnumerable<Category> GetAll()
        {
            var a = _map.Map<IEnumerable<Repositories.Models.Category>,
                IEnumerable<Category>>(_rep.GetAll());
            return a;
        }

        public bool Update(Category item)
        {
            var a = _map.Map<Category,
                Repositories.Models.Category>(item);
            return _rep.Update(a);
        }
    }
}
