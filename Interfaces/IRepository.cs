using System.Collections.Generic;

namespace news.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int Id);
        bool Create(T item);
        bool Update(T item);
        bool Delete(int Id);
    }
    public interface IChangePassword
    {
        bool ChangePass(int id, string oldPass, string newPass);
    }
}
