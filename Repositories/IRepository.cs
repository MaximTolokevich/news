using news.Repositories.Models;
using System.Collections.Generic;
using System.Linq;

namespace news.Repositories
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> GetAll();
        public T Get(int Id);
        public bool Create(T item);
        public bool Update(T item);
        public bool Delete(int Id);
        
    }
    public interface IPasswordManager
    {
        public bool ChangePassword(int id, string oldPass, string newPass);
    }
}
