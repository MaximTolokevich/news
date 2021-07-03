
using news.Interfaces;
using news.Models;
using news.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news.Service
{
    public class AuthorService : IService<Author>
    {
        IRepository<Author> rep;
        public AuthorService(IRepository<Author> repository)
        {
            rep = repository;
        }
        bool IService<Author>.Create(Author item)
        {
            return rep.Create(item);
        }

        bool IService<Author>.Delete(int Id)
        {
            return rep.Delete(Id);
        }

        Author IService<Author>.Get(int Id)
        {
            return rep.Get(Id);
        }

        IQueryable<Author> IService<Author>.GetAll()
        {
            return (IQueryable<Author>)rep.GetAll();
        }

        bool IService<Author>.Update(Author item)
        {
            return rep.Update(item);
        }
    }
}
